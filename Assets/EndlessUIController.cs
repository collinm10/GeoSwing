using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndlessUIController : MonoBehaviour
{
    private GameObject score_label;
    private GameObject player;
    private GameObject player_start;


    //Text objects
    private Text highscore_text;

    // Start is called before the first frame update
    void Start()
    {
        score_label = GameObject.Find("CurrentScore");
        player = GameObject.Find("plaer");
        player_start = GameObject.Find("plaer start");

        highscore_text = GameObject.Find("Highscore").GetComponent<Text>();

        SetHighscoreUI(PlayerPrefs.GetInt("EndlessHighscore", 0));
    }

    // Update is called once per frame
    public void UpdateScoreUI(int score)
    {
        score_label.GetComponent<Text>().text = score.ToString();
    }

    public void SetHighscoreUI(int highscore)
    {
        highscore_text.text = highscore.ToString();
    }
}
