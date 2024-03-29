using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject launcher;
    [SerializeField] Transform launcher_start;
    [SerializeField] Transform player_start;
    [SerializeField] GameObject level_complete_panel;
    [SerializeField] Text time_label;
    [SerializeField] Text highscore;
    [SerializeField] Text level_name;
    [SerializeField] Text comment_label;
    [SerializeField] Text time_difference;

    private string[] far_off_best;
    private string[] close_to_best;
    private string[] beat_best;
    public int CurrentLevel;
    private PlayerMovement pm;
    private float time_completed;
    private PowerUpManager pum;
    private CoinProgress cp;
    private CoinManager cm;
    public float gem_win_threshold;

    private GameObject CanvasAtEnd;
    private GameObject BuyLevelPanel;
    private GameObject NotEnoughGemsPanel;

    private GameObject swing_button;

    private int adCounter = 0;

    void Start()
    {
        swing_button = GameObject.Find("Grapple");
        CanvasAtEnd = GameObject.Find("CanvasAtEnd");
        CanvasAtEnd.SetActive(false);

        time_completed = 0;

        far_off_best = new string[]{ "lmao not even close", "did you take a break?", "wow... let's not talk about that", "at least try please" };
        close_to_best = new string[] {"mildly close I guess", "okay maybe you're getting there", "you can definitely be faster", "if you watch an ad you'll beat your time"};
        beat_best = new string[] { "nice job!!", "I'm proud... I don't say that often", "I'll give you $10 if you do it again", "congrats I guess..." };

        pm = player.GetComponent("PlayerMovement") as PlayerMovement;

        pum = gameObject.GetComponent("PowerUpManager") as PowerUpManager;

        level_complete_panel.SetActive(false);
        cm = gameObject.GetComponent("CoinManager") as CoinManager;
    }

    void FixedUpdate()
    {
        if (pm.start_time != 0)
            time_label.text = (Time.time - pm.start_time).ToString("0.00");
        else if (time_completed != 0)
            return;
        else
            time_label.text = "0.00";
    }

    public void level_complete()
    {
        //Disable swing button
        swing_button.SetActive(false);
        //Freeze player 
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        player.GetComponent<Rigidbody2D>().gravityScale = 0f;

        //Open up the menu after winning
        level_complete_panel.SetActive(true);

        //Disable boost bar
        try { GameObject.Find("Boost bar").SetActive(false); }
        catch { print("why"); }

        level_name.text = "Level " + CurrentLevel.ToString();
        Debug.Log("Level Complete!");

        //Get level time
        time_completed = Time.time - pm.start_time;
        pm.start_time = 0f;

        string level_key = "Level" + CurrentLevel.ToString() + "_Time";
        float bestrun = PlayerPrefs.GetFloat(level_key, 1000f);

        if (bestrun > time_completed)
        {
            PlayerPrefs.SetFloat(level_key, time_completed);
            highscore.text = time_completed.ToString("0.00");
            GameObject.Find("StartBestTime").GetComponent<Text>().text = time_completed.ToString("0.00");
            comment_label.text = beat_best[Random.Range(0, beat_best.Length)];

            if (bestrun != 1000f)
                time_difference.text = (time_completed - bestrun).ToString("0.00");
            else
                time_difference.text = time_completed.ToString("0.00");

            time_difference.color = new Color(0, 1, 0, 1);

            //Set to update leaderboard
            PlayerPrefs.SetInt("Level" + CurrentLevel.ToString() + "Update", 1);
        }
        else
        {
            highscore.text = bestrun.ToString("0.00");

            if (time_completed > (bestrun + 10))
                comment_label.text = far_off_best[Random.Range(0, far_off_best.Length)];
            else
                comment_label.text = close_to_best[Random.Range(0, close_to_best.Length)];

            time_difference.text = "+" + (time_completed - bestrun).ToString("0.00");
            time_difference.color = new Color(1, 0, 0, 1);
        }

        //Add gems
        cp = SaveSystem.LoadCoinProgress();

        if (!cp.levelCoinProg[CurrentLevel - 1][0])
        {
            cp.levelCoinProg[CurrentLevel - 1][0] = true;
            int c = PlayerPrefs.GetInt("Coins", 0);
            c++;
            PlayerPrefs.SetInt("Coins", c);
        }

        if(time_completed < gem_win_threshold && !cp.levelCoinProg[CurrentLevel - 1][1])
        {
            cp.levelCoinProg[CurrentLevel - 1][1] = true;
            int c = PlayerPrefs.GetInt("Coins", 0);
            c++;
            PlayerPrefs.SetInt("Coins", c);
        }

        SaveSystem.SaveCoinProgress(cp);
        cm.UpdateGemUI();
    }

    public void level_failed()
    {
        swing_button.SetActive(true);

        adCounter = PlayerPrefs.GetInt("Ad Counter", 0);

        if (++adCounter >= 25)
        {
            //Run ad
            InterstitialController ic = GameObject.Find("InterstitialManager").GetComponent("InterstitialController") as InterstitialController;
            ic.ShowAd();
            adCounter = 0;
        }
        PlayerPrefs.SetInt("Ad Counter", adCounter);

        reset_level();
    }

    public void reset_level()
    {
        time_label.text = "0.00";

        launcher.transform.position = launcher_start.transform.position;
        player.transform.position = player_start.position;

        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        player.GetComponent<Rigidbody2D>().gravityScale = .8f;

        pm.reset();

        pum.reset_power_ups();

        level_complete_panel.SetActive(false);
    }

    public void back_button_pressed()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void next_level()
    {

        //First check if next level is unlocked
        LevelProgress lp = SaveSystem.LoadLevelUnlockProg();

        if(lp.levelUnlockProg[CurrentLevel])
        {
            string next_level_name = "Level0" + (CurrentLevel + 1).ToString();
            if(CurrentLevel > 9)
            {
                next_level_name = "Level" + (CurrentLevel + 1).ToString();
            }
            SceneManager.LoadScene(next_level_name);
        }
        else
        {
            //If it's not unlocked then offer buy option
            set_buy_level(CurrentLevel + 1);
        }
        
    }

    public void set_buy_level(int level)
    {
        level_complete_panel.SetActive(false);

        CanvasAtEnd.SetActive(true);

        if(BuyLevelPanel == null)
            BuyLevelPanel = GameObject.Find("BuyLevel");

        BuyLevelPanel.SetActive(true);

        BuyLevelPanel.transform.position = level_complete_panel.transform.position;

        string level_name = "Level " + level.ToString();
        GameObject.Find("BuyLevelName").GetComponent<Text>().text = level_name;

        int coins = PlayerPrefs.GetInt("Coins", 0);
        GameObject.Find("BuyLevelNumCoins").GetComponent<Text>().text = coins.ToString();
    }

    public void set_level_complete_panel()
    {
        level_complete_panel.SetActive(true);
    }

}
