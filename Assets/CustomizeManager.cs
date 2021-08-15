using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizeManager : MonoBehaviour
{
    public GameObject main_menu_plaer;
    public GameObject main_menu_obstacle;

    public Transform skin_location;

    public int num_player_skins;
    public int num_obstacle_skins;
    public int num_rope_skins;

    [Header("SPRITE NAMES")]
    //Sprite names
    public string[] player_sprite_names;
    public string[] obstacle_sprite_names;
    public string[] rope_sprite_names;

    [Header("MATERIAL NAMES")]
    //Material names
    public string[] player_material_names;
    public string[] obstacle_material_names;
    public string[] rope_material_names;

    [Header("Demo Prefabs")]
    public GameObject[] player_skin_demos;
    public GameObject[] obstacle_skin_demos;
    public GameObject[] rope_skin_demos;

    [Header("Scroll Buttons")]
    public GameObject button1;
    public GameObject button2;

    //skin arrays
    private Skin[] player_skins;
    private Skin[] obstacle_skins;
    private Skin[] rope_skins;

    private GameObject[] active_demos;
    private int highlighted_index;

    private int type;

    private GameObject BuyOrEquip;
    public Text BuyOrEquipText;
    public GameObject BuyPanel;
    public Text NumPlayerCoins;
    private Customizer customizer;

    void Awake()
    {
        customizer = SaveSystem.LoadCustomizer();
        if (customizer == null)
        {
            customizer = new Customizer(obstacle_skin_demos.Length, rope_skin_demos.Length, player_skin_demos.Length);
            SaveSystem.SaveCustomizer(customizer);
        }

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

                    active_demos[i] = Instantiate(obstacle_skin_demos[i], pos, skin_location.rotation);


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

                    active_demos[i] = Instantiate(player_skin_demos[i], pos, skin_location.rotation);


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
        }
    }

    public void BuyOrEquipPressed()
    {
        if(customizer.GetSkinOwned(highlighted_index, type))
        {
            Debug.Log("Equipped");
            customizer.SetActiveSkin(highlighted_index, type);

            main_menu_plaer.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ForPlayer/PlayerSkin" + customizer.GetActiveSkin(2));
            main_menu_obstacle.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ForObstacles/ObstacleSkin" + customizer.GetActiveSkin(0));
            change_button();
        }
        else
        {
            int num_pc = PlayerPrefs.GetInt("Coins", 0);
            if(num_pc > 99)
            {
                customizer.SetSkinOwned(highlighted_index, type);
                num_pc -= 100;
                PlayerPrefs.SetInt("Coins", num_pc);
                change_button();
            }
            else
            {
                Debug.Log("Not enough money");
            }
        }

        SaveSystem.SaveCustomizer(customizer);
    }

    public void reset_customizer()
    {
        Customizer custom = new Customizer(num_obstacle_skins, num_rope_skins, num_player_skins);
        SaveSystem.SaveCustomizer(custom);
    }
}
