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

    private void disable_main_canvas()
    {
        MainMenuCanvas.SetActive(false);
    }

    private void enable_level_canvas()
    {
        LevelCanvas.SetActive(true);
    }
}
