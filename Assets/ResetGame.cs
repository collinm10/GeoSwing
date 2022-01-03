using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject launcher;
    [SerializeField] Transform launcher_start;
    [SerializeField] Transform player_start;

    private PlayerMovement pm;
    private HazardGeneration hg;
    private ObstacleGeneration og;

    //Need to store this in order to reactivate during reset
    private GameObject GenerateNext_Trigger_Holder;

    void Start()
    {

        pm = player.GetComponent("PlayerMovement") as PlayerMovement;
        hg = this.gameObject.GetComponent("HazardGeneration") as HazardGeneration;
        og = this.gameObject.GetComponent("ObstacleGeneration") as ObstacleGeneration;

        GenerateNext_Trigger_Holder = GameObject.Find("GenerateNext_Trigger");
    }

    public void reset_level()
    {
        //What do I need to do when reseting a level?
        /*
            1. Need to reset player
            2. Need to reset launcher
            3. Need to delete all hazards except for the first 2 sets
            4. Need to delete all backgrounds except the first couple sets
            5. Need to record a high score
         */

        //Resets launcher and player
        launcher.transform.position = launcher_start.transform.position;
        player.transform.position = player_start.position;

        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        player.GetComponent<Rigidbody2D>().gravityScale = 1;

        pm.reset();

        //Resets hazards
        hg.reset();

        //Resets Obstacles
        og.reset();

        //Reset backgrounds
        GameObject[] gms = GameObject.FindGameObjectsWithTag("BackgroundTag");

        try
        {
            Destroy(gms[1]);
        }
        catch
        {
            Debug.Log("No other background");
        }

        GenerateNext_Trigger_Holder.SetActive(true);

        gameObject.SendMessage("HighscoreCheck");

        //Run ad?
        int adCounter = PlayerPrefs.GetInt("Ad Counter", 0);

        if (++adCounter >= 25)
        {
            //Run ad
            InterstitialController ic = GameObject.Find("InterstitialManager").GetComponent("InterstitialController") as InterstitialController;
            ic.ShowAd();
            adCounter = 0;
        }

        PlayerPrefs.SetInt("Ad Counter", adCounter);

        GameObject[] gems = GameObject.FindGameObjectsWithTag("Gem");
        foreach(GameObject gem in gems){
            Destroy(gem);
        }
    }

    public void back_button_pressed()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void reset_infinite()
    {
        //Resets launcher and player
        launcher.transform.position = launcher_start.transform.position;
        player.transform.position = player_start.position;

        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        player.GetComponent<Rigidbody2D>().gravityScale = 1;

        pm.reset();
    }

}


