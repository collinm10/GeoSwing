using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUIManager : MonoBehaviour
{
    private int CurrentLevel;

    private float[] level_times_to_beat = { 3.5f, 6f, 13f, 10f, 14f, 18f, 23f, 19f, 30f, 27f, 14f, 10f, 25f, 13f, 25f, 0f, 0f, 0f, 0f, 0f };

    // Start is called before the first frame update
    void Start()
    {
        //Get Current Level
        LevelComplete lc = gameObject.GetComponent("LevelComplete") as LevelComplete;
        CurrentLevel = lc.CurrentLevel;

        //Get Best time
        string level_key = "Level" + CurrentLevel.ToString() + "_Time";
        float bestrun = PlayerPrefs.GetFloat(level_key, 0f);

        //Find text label
        GameObject.Find("StartBestTime").GetComponent<Text>().text = bestrun.ToString("0.00");
        GameObject.Find("StartGemTime").GetComponent<Text>().text = level_times_to_beat[CurrentLevel - 1].ToString("0.00");
    }

}
