using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Customizer
{
    private bool[] player_skin_owned;
    private bool[] rope_skin_owned;
    private bool[] obstacle_skin_owned;

    public int active_player_skin_index;
    public int active_rope_skin_index;
    public int active_obstacle_skin_index;

    public Customizer(int num_obstacle_skins, int num_rope_skins, int num_player_skins)
    {
        //Player starts out owning index 0
        active_obstacle_skin_index = 0;
        active_player_skin_index = 0;
        active_rope_skin_index = 0;

        player_skin_owned = new bool[num_player_skins];
        obstacle_skin_owned = new bool[num_obstacle_skins];
        rope_skin_owned = new bool[num_rope_skins];

        if(player_skin_owned.Length > 0)
            player_skin_owned[0] = true;

        if(rope_skin_owned.Length > 0)
            rope_skin_owned[0] = true;

        if(obstacle_skin_owned.Length > 0)
            obstacle_skin_owned[0] = true;
    }

    public void SetActiveSkin(int index, int type)
    {
        switch (type)
        {
            case 0:
                active_obstacle_skin_index = index;
                break;
            case 1:
                active_rope_skin_index = index;
                break;
            case 2:
                active_player_skin_index = index;
                break;
        }
    }

    public void SetSkinOwned(int index, int type)
    {
        switch (type)
        {
            case 0:
                obstacle_skin_owned[index] = true;
                break;
            case 1:
                rope_skin_owned[index] = true;
                break;
            case 2:
                player_skin_owned[index] = true;
                break;
        }
    }

    public bool GetSkinOwned(int index, int type)
    {
        switch (type)
        {
            case 0:
                return obstacle_skin_owned[index];
            case 1:
                return rope_skin_owned[index];
            case 2:
                return player_skin_owned[index];
            default:
                return false;
        }
    }

    public int GetActiveSkin(int type)
    {
        switch (type)
        {
            case 0:
                return active_obstacle_skin_index;
            case 1:
                return active_rope_skin_index;
            case 2:
                return active_player_skin_index;
            default:
                return -1;
        }
    }
}
