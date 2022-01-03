using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BuyLevelController : MonoBehaviour
{
    private LevelComplete gm;
    private GameObject BuyLevelPanel;
    private GameObject NotEnoughGemsPanel;
    private GameObject IAPPanel;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent("LevelComplete") as LevelComplete;
        BuyLevelPanel = GameObject.Find("BuyLevel");
        NotEnoughGemsPanel = GameObject.Find("NotEnoughGems");
        IAPPanel = GameObject.Find("IAP Panel");

        IAPPanel.SetActive(false);
        NotEnoughGemsPanel.SetActive(false);
    }

    public void Buy_Level()
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        if (coins >= 3)
        {
            coins -= 3;
            PlayerPrefs.SetInt("Coins", coins);
            LevelProgress lp = SaveSystem.LoadLevelUnlockProg();
            lp.levelUnlockProg[gm.CurrentLevel] = true;
            SaveSystem.SaveLevelProgress(lp);
            BuyLevelPanel.SetActive(false);
            SceneManager.LoadScene("Level0" + (gm.CurrentLevel + 1).ToString());
        }
        else
        {
            NotEnoughGemsPanel.transform.position = BuyLevelPanel.transform.position;
            NotEnoughGemsPanel.SetActive(true);
            BuyLevelPanel.SetActive(false);
            GameObject.Find("NotEnoughGemsNumCoins").GetComponent<Text>().text = coins.ToString();
        }
    }
    
    public void ExitNotEnoughGems()
    {
        BuyLevelPanel.SetActive(true);
        NotEnoughGemsPanel.SetActive(false);
    }

    public void OnClickNah()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenIAP()
    {
        IAPPanel.SetActive(true);
        NotEnoughGemsPanel.SetActive(false);
        IAPPanel.transform.position = BuyLevelPanel.transform.position;
    }

    public void CloseIAP()
    {
        IAPPanel.SetActive(false);
    }
}
