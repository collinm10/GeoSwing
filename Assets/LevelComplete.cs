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
    [SerializeField] GameObject level_complete_panel_without_pp;
    [SerializeField] Text time_label;
    [SerializeField] Text highscore;
    [SerializeField] Text level_name;

    public int CurrentLevel;
    private PlayerMovement pm;

    void Start()
    {
        pm = player.GetComponent("PlayerMovement") as PlayerMovement;
        level_complete_panel.SetActive(false);
        level_complete_panel_without_pp.SetActive(false);
    }
    public void level_complete()
    {
        Debug.Log(Time.time - pm.start_time);
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        player.GetComponent<Rigidbody2D>().gravityScale = 0f;

        level_complete_panel.SetActive(true);
        level_complete_panel_without_pp.SetActive(true);

        level_name.text = "Level " + CurrentLevel.ToString();
        Debug.Log("Level Complete!");

        //Get level time
        float time_completed = Time.time - pm.start_time;
        string level_key = "Level" + CurrentLevel.ToString() + "_Time";
        float bestrun = PlayerPrefs.GetFloat(level_key, 1000f);

        if (bestrun > time_completed)
        {
            PlayerPrefs.SetFloat(level_key, time_completed);
            highscore.text = time_completed.ToString("0.00");
        }
        else
        {
            highscore.text = bestrun.ToString("0.00");
        }

        time_label.text = time_completed.ToString(".00");
    }

    public void level_failed()
    {
        Debug.Log("You suck!");
        reset_level();
    }

    public void reset_level()
    {
        launcher.transform.position = launcher_start.transform.position;
        player.transform.position = player_start.position;
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        player.GetComponent<Rigidbody2D>().gravityScale = 1;
        pm.reset();
        level_complete_panel.SetActive(false);
        level_complete_panel_without_pp.SetActive(false);

    }

    public void back_button_pressed()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void next_level()
    {
        string next_level_name = "Level0" + (CurrentLevel + 1).ToString();
        SceneManager.LoadScene(next_level_name);
    }
}
