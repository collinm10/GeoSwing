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
        level_buttons = new GameObject[scene_names.Length];
    }

    void Start()
    {
        customize_level_buttons();
    }

    public void load_level()
    {
        SceneManager.LoadScene(scene_names[currLevel-1]);
    }

    public void open_levels_screen()
    {
        disable_main_canvas();
        enable_level_canvas();
        disable_level_info_panel();
    }

    public void open_main_menu()
    {
        disable_customize_obstacle();
        disable_customize_player();
        disable_customize_rope();
        disable_level_canvas();
        disable_level_info_panel();
        enable_main_canvas();
    }

    public void open_customize_obstacle()
    {
        enable_customize_obstacle();
        disable_customize_player();
        disable_customize_rope();
        disable_level_canvas();
        disable_main_canvas();
    }

    public void open_customize_rope()
    {
        disable_customize_obstacle();
        disable_customize_player();
        enable_customize_rope();
        disable_level_canvas();
        disable_main_canvas();
    }

    public void open_customize_player()
    {
        disable_customize_obstacle();
        enable_customize_player();
        disable_customize_rope();
        disable_level_canvas();
        disable_main_canvas();
    }

    public void open_level_info_panel(int level)
    {
        disable_customize_obstacle();
        disable_customize_player();
        disable_customize_rope();
        disable_level_canvas();
        disable_main_canvas();
        enable_level_info_panel();
        set_level_info(level);
    }

    private void enable_main_canvas()
    {
        MainMenuCanvas.SetActive(true);
        customize_canvas.SetActive(false);
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

        LevelCanvas.SetActive(false);
    }
}