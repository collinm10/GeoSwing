using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CustomizeManager : MonoBehaviour
{
    public GameObject main_menu_plaer;
    public GameObject main_menu_obstacle;

    //Base gameobjects for store skins
    public GameObject base_player_skin;
    public GameObject base_obstacle_skin;

    public Transform skin_location;

    //Number of each type of skin
    private int num_player_skins;
    private int num_obstacle_skins;

    [Header("Scroll Buttons")]
    public GameObject button1;
    public GameObject button2;

    private GameObject[] active_demos;
    private int highlighted_index;

    //Obstacle or Player skins?
    private int type;

    //UI Elements
    private GameObject BuyOrEquip;
    public Text BuyOrEquipText;
    public GameObject BuyPanel;
    public Text NumPlayerCoins;
    public Text CostOfSkin;

    private Customizer customizer;

    private UIController ui;

    void Awake()
    {
        /*
         REMEMBER AFTER ADDING SKINS
         a) Change num skins here
         b) Change num skins in customizer
         c) Make sure skins have appropriate numbering
        */
        //Initialize some variables
        num_obstacle_skins = 4;
        num_player_skins = 24;

        ui = gameObject.GetComponent("UIController") as UIController;

        customizer = SaveSystem.LoadCustomizer();

        if (customizer == null)
        {
            customizer = new Customizer();
            SaveSystem.SaveCustomizer(customizer);
        }

        //Check if the player owned list still match the amount of skins in the folder
        if (num_player_skins >= customizer.GetSizeOfPlayerOwned())
            customizer.IncrementPlayerSkinOwnedSize(num_player_skins - customizer.GetSizeOfPlayerOwned());

        main_menu_plaer.GetComponent<MainMenuAnimation>().update_skins(customizer.GetActiveSkin(2), 2);
        main_menu_obstacle.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ForObstacles/ObstacleSkin" + customizer.GetActiveSkin(0));

        BuyOrEquip = GameObject.Find("BuyOrEquip");
    }

    public void customization_clicked(int type1)
    {
        int numCoins = PlayerPrefs.GetInt("Coins", 0);
        NumPlayerCoins.text = numCoins.ToString();

        button1.SetActive(true);
        button2.SetActive(true);

        switch (type1)
        {
            case 0:
                active_demos = new GameObject[num_obstacle_skins];
                for (int i = 0; i < num_obstacle_skins; i++)
                {
                    highlighted_index = 0;

                    Vector3 pos;
                    if (i < 2)
                        pos = new Vector3(skin_location.position.x + i * 3.5f, skin_location.position.y, skin_location.position.z);
                    else
                        pos = new Vector3(skin_location.position.x + i * 30f, skin_location.position.y, skin_location.position.z);

                    active_demos[i] = Instantiate(base_obstacle_skin, pos, skin_location.rotation);
                    active_demos[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ForObstacles/ObstacleSkin" + (i).ToString());


                    if (active_demos[i].transform.position != skin_location.position)
                    {
                        dehighlight(i);
                    }
                }
                button1.SetActive(false);
                type = 0;
                break;
            case 1:
                type = 1;
                break;
            case 2:
                active_demos = new GameObject[num_player_skins];
                for (int i = 0; i < num_player_skins; i++)
                {
                    highlighted_index = 0;

                    Vector3 pos;
                    if (i < 2)
                        pos = new Vector3(skin_location.position.x + i * 3.5f, skin_location.position.y, skin_location.position.z);
                    else
                        pos = new Vector3(skin_location.position.x + i * 30f, skin_location.position.y, skin_location.position.z);

                    active_demos[i] = Instantiate(base_player_skin, pos, skin_location.rotation);
                    active_demos[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ForPlayer/PlayerSkin" + (i).ToString());

                    if (active_demos[i].transform.position != skin_location.position)
                    {
                        dehighlight(i);
                    }
                }
                button1.SetActive(false);
                type = 2;
                break;
        }
        change_button();
    }

    public void scroll_right()
    {
        button1.SetActive(true);

        if (highlighted_index != 0)
        {
            active_demos[highlighted_index - 1].transform.position = new Vector3(skin_location.position.x - 20f, skin_location.position.y, skin_location.position.z);
        }

        active_demos[highlighted_index].transform.position = new Vector3(skin_location.position.x - 3.5f, skin_location.position.y, skin_location.position.z);
        dehighlight(highlighted_index);

        active_demos[highlighted_index + 1].transform.position = skin_location.position;
        highlight(highlighted_index + 1);

        if (highlighted_index < active_demos.Length - 2)
            active_demos[highlighted_index + 2].transform.position = new Vector3(skin_location.position.x + 3.5f, skin_location.position.y, skin_location.position.z);

        highlighted_index++;

        if (highlighted_index == active_demos.Length - 1)
        {
            button2.SetActive(false);
            button1.SetActive(true);
        }

        change_button();
    }

    public void scroll_left()
    {
        button2.SetActive(true);

        active_demos[highlighted_index].transform.position = new Vector3(skin_location.position.x + 3.5f, skin_location.position.y, skin_location.position.z);
        dehighlight(highlighted_index);

        if (highlighted_index < active_demos.Length - 1)
            active_demos[highlighted_index + 1].transform.position = new Vector3(skin_location.position.x + 30f, skin_location.position.y, skin_location.position.z);

        active_demos[highlighted_index - 1].transform.position = skin_location.position;
        highlight(highlighted_index - 1);

        if (highlighted_index > 1)
        {
            active_demos[highlighted_index - 2].transform.position = new Vector3(skin_location.position.x - 3.5f, skin_location.position.y, skin_location.position.z);
        }

        highlighted_index--;

        if (highlighted_index == 0)
        {
            button1.SetActive(false);
            button2.SetActive(true);
        }

        change_button();
    }

    private void dehighlight(int i)
    {
        active_demos[i].transform.localScale *= .7f;
        Color curr = active_demos[i].GetComponent<SpriteRenderer>().color;
        active_demos[i].GetComponent<SpriteRenderer>().color = new Color(curr.r, curr.g, curr.b, .5f);
    }

    private void highlight(int i)
    {
        active_demos[i].transform.localScale *= 1.429f;
        Color curr = active_demos[i].GetComponent<SpriteRenderer>().color;
        active_demos[i].GetComponent<SpriteRenderer>().color = new Color(curr.r, curr.g, curr.b, 1f);
    }

    public void back_pressed()
    {
        foreach (GameObject go in active_demos)
        {
            Destroy(go);
        }
    }

    public void add_coins(int i)
    {
        int coins = PlayerPrefs.GetInt("coins", 0);
        coins = coins + i;
        PlayerPrefs.SetInt("coins", coins);
    }

    public void remove_coins(int i)
    {
        int coins = PlayerPrefs.GetInt("coins", 0);
        coins = coins - i;
        PlayerPrefs.SetInt("coins", coins);
    }

    private void change_button()
    {
        if(customizer.GetSkinOwned(highlighted_index, type) && customizer.GetActiveSkin(type) != highlighted_index)
        {
            BuyPanel.SetActive(false);
            BuyOrEquipText.text = "Equip";
        }
        else if(customizer.GetSkinOwned(highlighted_index, type) && customizer.GetActiveSkin(type) == highlighted_index)
        {
            BuyPanel.SetActive(false);
            BuyOrEquipText.text = "Equipped";
        }
        else
        {
            BuyOrEquipText.text = "";
            BuyPanel.SetActive(true);
            if (type == 2)
                CostOfSkin.text = customizer.player_cost[highlighted_index].ToString();
            else if (type == 0)
                CostOfSkin.text = customizer.obstacle_cost[highlighted_index].ToString();
        }
    }

    public void BuyOrEquipPressed()
    {
        if(customizer.GetSkinOwned(highlighted_index, type))
        {
            Debug.Log("Equipped");
            customizer.SetActiveSkin(highlighted_index, type);

            main_menu_plaer.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ForPlayer/PlayerSkin" + customizer.GetActiveSkin(2));
            main_menu_obstacle.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ForObstacles/ObstacleSkin" + customizer.GetActiveSkin(0));
            change_button();
        }
        else
        {
            int num_pc = PlayerPrefs.GetInt("Coins", 0);

            int cost = 0;
            if (type == 2)
                cost = customizer.player_cost[highlighted_index];
            else if (type == 0)
                cost = customizer.obstacle_cost[highlighted_index];

            if(num_pc >= cost)
            {
                customizer.SetSkinOwned(highlighted_index, type);
                num_pc -= cost;
                PlayerPrefs.SetInt("Coins", num_pc);
                change_button();
                GameObject.Find("NumPlayerCoinsLabel").GetComponent<Text>().text = PlayerPrefs.GetInt("Coins", 0).ToString();
            }
            else
            {
                ui.enable_not_enough_gems();
            }
        }

        SaveSystem.SaveCustomizer(customizer);
    }

    public void reset_customizer()
    {
        PlayerPrefs.SetInt("Coins", 0);
    }
}
