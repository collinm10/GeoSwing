using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private string[] scene_names;
    [SerializeField] private GameObject MainMenuCanvas;
    [SerializeField] private GameObject LevelCanvas;
    [SerializeField] private GameObject level_button_prefab;
    [SerializeField] private GameObject customize_obstacle_canvas;
    [SerializeField] private GameObject customize_rope_canvas;
    [SerializeField] private GameObject customize_player_canvas;
    [SerializeField] private GameObject customize_canvas;
    [SerializeField] private GameObject level_info_panel;
    [SerializeField] private GameObject BuyLevelPanel;
    [SerializeField] private GameObject NotEnoughGemsPanel;
    [SerializeField] private GameObject IAPPanel;

    private GameObject[] level_buttons;
    public Sprite CoinPrefab;
    public Sprite CoinOutlinePrefab;
    private GameObject BestRunTime;
    private GameObject LevelName;
    private GameObject TimeToBeat;
    private int currLevel;

    private float[] level_times_to_beat = {3.5f, 6f, 13f, 10f, 14f, 18f, 23f, 19f, 30f, 27f};

    void Awake()
    {
        LevelProgress lp = SaveSystem.LoadLevelUnlockProg();
        if(lp == null)
        {
            lp = new LevelProgress(10);
            SaveSystem.SaveLevelProgress(lp);
        }

        level_buttons = new GameObject[scene_names.Length];
    }

    void Start()
    {
        customize_level_buttons();
        SetLevelUnlocks();
        LevelCanvas.SetActive(false);

    }

    public void load_level()
    {
        SceneManager.LoadScene(scene_names[currLevel-1]);
    }

    public void go_to_customize()
    {
        open_customize_player();
    }

    public void load_endless()
    {
        SceneManager.LoadScene("Endless");
    }
    public void open_levels_screen()
    {
        disable_customize_obstacle();
        disable_customize_player();
        disable_customize_rope();
        enable_level_canvas();
        disable_level_info_panel();
        disable_main_canvas();
        disable_buy_level();
        disable_iap_panel();
        disable_not_enough_gems();
    }

    public void open_main_menu()
    {
        disable_customize_obstacle();
        disable_customize_player();
        disable_customize_rope();
        disable_level_canvas();
        disable_level_info_panel();
        enable_main_canvas();
        disable_buy_level();
        disable_iap_panel();
        disable_not_enough_gems();
    }

    public void open_customize_obstacle()
    {
        enable_customize_obstacle();
        disable_customize_player();
        disable_customize_rope();
        disable_level_canvas();
        disable_main_canvas();
        disable_buy_level();
    }

    public void open_customize_rope()
    {
        disable_customize_obstacle();
        disable_customize_player();
        enable_customize_rope();
        disable_level_canvas();
        disable_main_canvas();
        disable_buy_level();
    }

    public void open_customize_player()
    {
        disable_customize_obstacle();
        enable_customize_player();
        disable_customize_rope();
        disable_level_canvas();
        disable_main_canvas();
        disable_buy_level();
    }

    public void open_level_info_panel(int level)
    {
        //Check if level is unlocked already
        LevelProgress lp1 = SaveSystem.LoadLevelUnlockProg();

        if (lp1.levelUnlockProg[level - 1])
        {
            enable_level_info_panel();
            disable_customize_obstacle();
            disable_customize_player();
            disable_customize_rope();
            disable_level_canvas();
            disable_main_canvas();
            set_level_info(level);
        }
        else
        {
            set_buy_level(level);
        }
    }

    private void enable_main_canvas()
    {
        MainMenuCanvas.SetActive(true);
        customize_canvas.SetActive(false);
    }

    public void disable_buy_level()
    {
        if (BuyLevelPanel == null)
            BuyLevelPanel = GameObject.Find("BuyLevel");
        BuyLevelPanel.SetActive(false);
    }

    public void enable_not_enough_gems()
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        NotEnoughGemsPanel.SetActive(true);
        GameObject.Find("NotEnoughGemsNumCoins").GetComponent<Text>().text = coins.ToString();
    }

    public void disable_not_enough_gems()
    {
        if (NotEnoughGemsPanel == null)
            NotEnoughGemsPanel = GameObject.Find("NotEnoughGems");
        NotEnoughGemsPanel.SetActive(false);
        try { GameObject.Find("NumPlayerCoinsLabel").GetComponent<Text>().text = PlayerPrefs.GetInt("Coins", 0).ToString(); }
        catch { }
    }
    private void disable_main_canvas()
    {
        MainMenuCanvas.SetActive(false);
    }

    private void enable_level_info_panel()
    {
        level_info_panel.SetActive(true);
    }

    private void disable_level_info_panel()
    {
        level_info_panel.SetActive(false);
    }

    private void enable_level_canvas()
    {
        customize_canvas.SetActive(false);
        LevelCanvas.SetActive(true);
    }

    private void disable_level_canvas()
    {
        LevelCanvas.SetActive(false);
    }

    //OBSTACLE
    private void enable_customize_obstacle()
    {
        customize_canvas.SetActive(true);
        customize_obstacle_canvas.SetActive(true);
    }

    private void disable_customize_obstacle()
    {
        customize_obstacle_canvas.SetActive(false);
    }

    //ROPE
    private void enable_customize_rope()
    {
        customize_canvas.SetActive(true);
        customize_rope_canvas.SetActive(true);
    }

    private void disable_customize_rope()
    {
        customize_rope_canvas.SetActive(false);
    }

    //PLAYER
    private void enable_customize_player()
    {
        customize_canvas.SetActive(true);
        customize_player_canvas.SetActive(true);
    }

    private void disable_customize_player()
    {
        customize_player_canvas.SetActive(false);
    }

    private void set_level_info(int level)
    {
        currLevel = level;
        string level_key = "Level" + level.ToString() + "_Time";
        float bestrun = PlayerPrefs.GetFloat(level_key, 0f);

        if(BestRunTime == null)
        {
            BestRunTime = GameObject.Find("BestRunTime");
        }

        if(LevelName == null)
        {
            LevelName = GameObject.Find("LevelName");
        }

        if(TimeToBeat == null)
        {
            TimeToBeat = GameObject.Find("TimeToBeat");
        }

        LevelName.GetComponent<Text>().text = "Level " + level.ToString();
        BestRunTime.GetComponent<Text>().text = bestrun.ToString("0.00");
        TimeToBeat.GetComponent<Text>().text = level_times_to_beat[level - 1].ToString("0.00");

        CoinProgress cp = SaveSystem.LoadCoinProgress();

        for(int i = 0; i < 3; i++)
        {
            if (cp.levelCoinProg[level - 1][i])
            {
                string hold = "UICoin" + i.ToString();
                GameObject.Find(hold).GetComponent<Image>().sprite = CoinPrefab;
            }
            else
            {
                string hold = "UICoin" + i.ToString();
                GameObject.Find(hold).GetComponent<Image>().sprite = CoinOutlinePrefab;
            }
        }
    }

    private void customize_level_buttons()
    {
        CoinProgress cp = SaveSystem.LoadCoinProgress();
        if(cp == null)
        {
            cp = new CoinProgress();
            SaveSystem.SaveCoinProgress(cp);
        }

        for(int i = 0; i < scene_names.Length; i++)
        {
            int jj = 0;
            for(int j = 0; j < 3; j++)
            {
                if (cp.levelCoinProg[i][j])
                {
                    string hold = "Gem" + (i + 1).ToString() + (jj + 1).ToString();
                    GameObject.Find(hold).GetComponent<Image>().sprite = CoinPrefab;
                    jj++;
                }
            }

        }

    }

    public void SetLevelUnlocks()
    {
        LevelProgress lp = SaveSystem.LoadLevelUnlockProg();

        if (lp == null)
        {
            lp = new LevelProgress(scene_names.Length);
            SaveSystem.SaveLevelProgress(lp);
        }

        for(int i = 0; i < scene_names.Length; i++)
        {
            //Check if level is unlocked
            if (!lp.levelUnlockProg[i])
            {
                //Disable gems if not unlocked
                string level_name_in_menu = "LevelButton (" + i.ToString() + ")";
                for (int j = 0; j < 3; j++)
                {
                    string hold = "Gem" + (i+1).ToString() + (j+1).ToString();
                    try
                    {
                        GameObject.Find(hold).SetActive(false);
                    }
                    catch { }

                }
            }
            else if(i > 2)
            {
                //Disable the lock icon
                string hold = "UnlockIcon" + (i + 1).ToString();
                try
                {
                    GameObject.Find(hold).SetActive(false);
                }
                catch {}

                //Re-enable gems
                for (int j = 0; j < 3; j++)
                {
                    string hold1 = "Gem" + (i + 1).ToString() + (j + 1).ToString();
                    try
                    {
                        GameObject.Find(hold1).SetActive(true);
                    }
                    catch {}
                }
            }
        }

    }

    public void set_buy_level(int level)
    {
        BuyLevelPanel.SetActive(true);
        string level_name = "Level " + level.ToString();
        GameObject.Find("BuyLevelName").GetComponent<Text>().text = level_name;

        string buy_level_label = "Buy level for       ?";
        GameObject.Find("BuyLevelLabel").GetComponent<Text>().text = buy_level_label;

        int coins = PlayerPrefs.GetInt("Coins", 0);
        GameObject.Find("BuyLevelNumCoins").GetComponent<Text>().text = coins.ToString();

        currLevel = level;
    }

    public void Buy_Level()
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        if(coins >= 3)
        {
            coins -= 3;
            PlayerPrefs.SetInt("Coins", coins);
            LevelProgress lp = SaveSystem.LoadLevelUnlockProg();
            lp.levelUnlockProg[currLevel - 1] = true;
            SaveSystem.SaveLevelProgress(lp);
            SetLevelUnlocks();
            BuyLevelPanel.SetActive(false);

            //Set Gems active on new level
            GameObject.Find("LevelButton" + currLevel.ToString()).transform.GetChild(1).gameObject.SetActive(true);
            GameObject.Find("LevelButton" + currLevel.ToString()).transform.GetChild(2).gameObject.SetActive(true);
            GameObject.Find("LevelButton" + currLevel.ToString()).transform.GetChild(3).gameObject.SetActive(true);
        }
        else
        {
            NotEnoughGemsPanel.SetActive(true);
            GameObject.Find("NotEnoughGemsNumCoins").GetComponent<Text>().text = coins.ToString();
        }
    }

    public void enable_iap_panel()
    {
        IAPPanel.SetActive(true);
    }

    public void disable_iap_panel()
    {
        if (IAPPanel == null)
            IAPPanel = GameObject.Find("IAP Panel");
        IAPPanel.SetActive(false);
    }
}