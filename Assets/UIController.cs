using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private GameObject[] level_buttons;

    void Awake()
    {
        level_buttons = new GameObject[scene_names.Length];
    }

    void Start()
    {
        int i = 0;
        foreach (string name in scene_names)
        {
            level_buttons[i] = Instantiate(level_button_prefab);
            i++;
        }
    }

    public void load_level(int index)
    {
        SceneManager.LoadScene(scene_names[index]);
    }

    public void open_levels_screen()
    {
        disable_main_canvas();
        enable_level_canvas();
    }

    public void open_main_menu()
    {
        disable_customize_obstacle();
        disable_customize_player();
        disable_customize_rope();
        disable_level_canvas();
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

    private void enable_main_canvas()
    {
        MainMenuCanvas.SetActive(true);
        customize_canvas.SetActive(false);
    }

    private void disable_main_canvas()
    {
        MainMenuCanvas.SetActive(false);
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
}
