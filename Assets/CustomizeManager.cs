using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizeManager : MonoBehaviour
{
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
    public Button button1;
    public Button button2;

    //skin arrays
    private Skin[] player_skins;
    private Skin[] obstacle_skins;
    private Skin[] rope_skins;

    private GameObject[] active_demos;
    private int highlighted_index;

    void Awake()
    {
        //Initialize skin arrays
        player_skins = new Skin[num_player_skins];
        obstacle_skins = new Skin[num_obstacle_skins];
        rope_skins = new Skin[num_rope_skins];

        //Create player skins
        for(int i = 0; i < num_player_skins; i++)
        {
            player_skins[i] = new Skin(player_sprite_names[i], player_material_names[i], 2);
        }

        //Create obstacle skins
        for (int i = 0; i < num_obstacle_skins; i++)
        {
            obstacle_skins[i] = new Skin(obstacle_sprite_names[i], obstacle_material_names[i], 0);
        }

        //Create rope skins
        for (int i = 0; i < num_rope_skins; i++)
        {
            rope_skins[i] = new Skin(rope_sprite_names[i], rope_material_names[i], 1);
        }
    }

    public void customization_clicked(int type)
    {
        switch (type)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                active_demos = new GameObject[num_player_skins];
                for (int i = 0; i < num_player_skins; i++)
                {
                    highlighted_index = 0;

                    Vector3 pos;
                    if(i < 2)
                        pos = new Vector3(skin_location.position.x + i * 3.5f, skin_location.position.y, skin_location.position.z);
                    else
                        pos = new Vector3(skin_location.position.x + i * 30f, skin_location.position.y, skin_location.position.z);

                    active_demos[i] = Instantiate(player_skin_demos[i], pos, skin_location.rotation);


                    if(active_demos[i].transform.position != skin_location.position)
                    {
                        dehighlight(i);
                    }
                }
                button1.interactable = false;
                break;
        }
    }

    public void scroll_right()
    {
        button1.interactable = true;

        if (highlighted_index != 0)
        {
            active_demos[highlighted_index - 1].transform.position = new Vector3(skin_location.position.x - 20f, skin_location.position.y, skin_location.position.z);
        }

        active_demos[highlighted_index].transform.position = new Vector3(skin_location.position.x - 3.5f, skin_location.position.y, skin_location.position.z);
        dehighlight(highlighted_index);

        active_demos[highlighted_index + 1].transform.position = skin_location.position;
        highlight(highlighted_index + 1);

        if(highlighted_index < active_demos.Length - 2)
            active_demos[highlighted_index + 2].transform.position = new Vector3(skin_location.position.x + 3.5f, skin_location.position.y, skin_location.position.z);

        highlighted_index++;

        print(highlighted_index);
        print(active_demos.Length - 1);

        if(highlighted_index == active_demos.Length - 1)
        {
            button2.interactable = false;
        }
    }

    public void scroll_left()
    {
        button2.interactable = true;

        active_demos[highlighted_index].transform.position = new Vector3(skin_location.position.x + 3.5f, skin_location.position.y, skin_location.position.z);
        dehighlight(highlighted_index);

        if(highlighted_index  < active_demos.Length - 1)
            active_demos[highlighted_index + 1].transform.position = new Vector3(skin_location.position.x + 30f, skin_location.position.y, skin_location.position.z);

        active_demos[highlighted_index - 1].transform.position = skin_location.position;
        highlight(highlighted_index - 1);

        if(highlighted_index > 1)
        {
            active_demos[highlighted_index - 2].transform.position = new Vector3(skin_location.position.x - 3.5f, skin_location.position.y, skin_location.position.z);
        }

        highlighted_index--;
        
        if(highlighted_index == 0)
        {
            button1.interactable = false;
        }
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
        foreach(GameObject go in active_demos)
        {
            Destroy(go);
        }
    }

}
