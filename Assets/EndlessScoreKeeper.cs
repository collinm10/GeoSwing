using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndlessScoreKeeper : MonoBehaviour
{

    public int score_for_endless = 0;

    private EndlessUIController UIController;

    private MovePusher mp;

    // Start is called before the first frame update
    void Start()
    {
        UIController = gameObject.GetComponent("EndlessUIController") as EndlessUIController;
        mp = GameObject.Find("Pusher").GetComponent("MovePusher") as MovePusher;
    }

    // Update is called once per frame
    public void UpdateScore()
    {
        score_for_endless++;
        UIController.UpdateScoreUI(score_for_endless);
        
        if(score_for_endless % 10 == 0 && mp.speed != 35)
        {
            mp.speed += 5;
        }

    }

    //Check or set high score
    public void HighscoreCheck()
    {
        GameObject.Find("PrevScore").GetComponent<Text>().text = score_for_endless.ToString();
        //This runs when the player dies or resets 
        if (score_for_endless > PlayerPrefs.GetInt("EndlessHighscore", 0))
        {
            PlayerPrefs.SetInt("EndlessHighscore", score_for_endless);
            UIController.SetHighscoreUI(score_for_endless);
        }

        score_for_endless = 0;

        UIController.UpdateScoreUI(score_for_endless);
    }
}
