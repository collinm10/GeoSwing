using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using TMPro;

public class LeaderboardController : MonoBehaviour
{
    UIController ui;
    int num_levels;
    bool ready_for_next = true;
    public TextMeshProUGUI level_text;
    public GameObject LeaderboardPanel;
    public GameObject DisplayNamePanel;
    public Text NameText;

    // Start is called before the first frame update
    void Awake()
    {
        ui = GameObject.Find("GameManager").GetComponent<UIController>();
        num_levels = GameObject.Find("GameManager").GetComponent<UIController>().scene_names.Length;
        Login();
    }

    public void OnLeaderboardClick(int level)
    {
        ui.disable_level_info_panel();
        LeaderboardPanel.SetActive(true);

        //Update leaderboard UI
        level_text.text = "Level " + level.ToString();
        GetLeaderboard(level);
    }

    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }

    void OnSuccess(LoginResult result)
    {
        Debug.Log("Successful Login/Account creation!");

        string name = null;
        if(result.InfoResultPayload.PlayerProfile != null)
            name = result.InfoResultPayload.PlayerProfile.DisplayName;

        if(name == null)
        {
            DisplayNamePanel.SetActive(true);
            ui.disable_main_canvas();
        }

        if(PlayerPrefs.GetInt("hasLeaderboardUpdated", 0) == 0)
        {
            StartCoroutine(UpdateLeaderboard());
        }

        //Check player prefs for each level
        for(int i = 0; i < 20; i++)
        {
            if(PlayerPrefs.GetInt("Level" + (i+1).ToString() + "Update", 0) == 1)
            {
                string level_key = "Level" + (i + 1).ToString() + "_Time";
                float bestrun = PlayerPrefs.GetFloat(level_key, 1000f);
                string hold = bestrun.ToString("0.00");
                float bestrun1 = float.Parse(hold);
                SendLeaderboard(bestrun1, i + 1);

                PlayerPrefs.SetInt("Level" + (i+1).ToString() + "Update", 0);
            }
        }
    }

    void OnError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }

    public void SendLeaderboard(float time, int level)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "Level " + level.ToString() + " Time",
                    Value = (int)(Mathf.Round(time * 100f))
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Leaderboard Updated!");
    }

    public void GetLeaderboard(int level)
    {
        /*var request = new GetLeaderboardRequest
        {
            StatisticName = "Level " + level.ToString() + " Time",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);*/

        var request = new GetLeaderboardAroundPlayerRequest
        {
            StatisticName = "Level " + level.ToString() + " Time",
            PlayFabId = "B608539395D97582",
            MaxResultsCount = 11
        };
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnLeaderboardGet, OnError);
    }

    void OnLeaderboardGet(GetLeaderboardAroundPlayerResult result)
    {
        Debug.Log("Leaderboard has been got");

        //Do something
        int count = 1;
        bool skip = true;
        var items = result.Leaderboard;
        items.Reverse();
        foreach (var item in items)
        {
            if (skip)
            {
                skip = false;
                continue;
            }

            //If player hasn't completed level
            if(item.StatValue/100 != 1000)
            {
                GameObject.Find("Name" + count.ToString()).GetComponent<Text>().text = item.DisplayName;
                GameObject.Find("Time" + count.ToString()).GetComponent<Text>().text = (((float)item.StatValue / 100f)).ToString("0.00");
            }
            else
            {
                GameObject.Find("Name" + count.ToString()).GetComponent<Text>().text = "-";
                GameObject.Find("Time" + count.ToString()).GetComponent<Text>().text = "-";
            }
            count++;
        }

        for(int i = count; i < 11; i++)
        {
            GameObject.Find("Name" + i.ToString()).GetComponent<Text>().text = "------";
            GameObject.Find("Time" + i.ToString()).GetComponent<Text>().text = "------";
        }
    }

    IEnumerator UpdateLeaderboard()
    {
        //Send all best runs to playfab
        for (int i = 0; i < num_levels; i++)
        {
            string level_key = "Level" + (i + 1).ToString() + "_Time";
            float bestrun = PlayerPrefs.GetFloat(level_key, 1000f);

            string hold = bestrun.ToString("0.00");
            float bestrun1 = float.Parse(hold);

            if(bestrun != 1000)
                SendLeaderboard(bestrun, i + 1);

            yield return new WaitForSeconds(.2f);
        }

        //Set hasLeaderboardUpdated so it doesn't run again
        PlayerPrefs.SetInt("hasLeaderboardUpdated", 1);
    }

    public void EnterUsername()
    {
        if(NameText.text != null)
        {
            var request = new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = NameText.text,
            };
            PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
        }
    }

    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        ui.open_main_menu();
        DisplayNamePanel.SetActive(false);
    }

    int hold_curr_leaderboard = 1;

    public void CreateDummy() 
    {
        SendLeaderboard(0, hold_curr_leaderboard);
        hold_curr_leaderboard++;
        Debug.Log(hold_curr_leaderboard);
    }
}
