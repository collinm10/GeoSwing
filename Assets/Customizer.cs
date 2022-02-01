using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Customizer
{
    private List<bool> player_skin_owned;
    private List<bool> obstacle_skin_owned;

    public int[] player_cost;
    public int[] obstacle_cost;

    public int active_player_skin_index;
    public int active_obstacle_skin_index;

    public int num_player_skins = 24;
    public int num_obstacle_skins = 4;

    public Customizer()
    {
        //Define the costs for skins
        player_cost = new int[24] { 0, 3, 3, 9, 9, 9, 9, 9, 9, 9, 9, 9, 5, 12, 15, 15, 15, 15, 15, 15, 15, 20, 20, 30 };
        obstacle_cost = new int[4] { 0, 9, 9, 9 };

        //Player starts out owning and using index 0
        active_obstacle_skin_index = 0;
        active_player_skin_index = 0;

        //Initialize the owned lists
        player_skin_owned = new List<bool>();
        obstacle_skin_owned = new List<bool>();

        //Add all bools for each player skin in each folder
        for(int i = 0; i < num_player_skins; i++)
        {
            player_skin_owned.Add(false);
        }
        
        //Add all bools for each obstacle skin in each folder
        for (int i = 0; i < num_obstacle_skins; i++)
        {
            obstacle_skin_owned.Add(false);
        }


        if (player_skin_owned.Count > 0)
            player_skin_owned[0] = true;

        if(obstacle_skin_owned.Count > 0)
            obstacle_skin_owned[0] = true;
    }

    public void SetActiveSkin(int index, int type)
    {
        switch (type)
        {
            case 0:
                active_obstacle_skin_index = index;
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
            case 2:
                return active_player_skin_index;
            default:
                return -1;
        }
    }

    public void IncrementPlayerSkinOwnedSize(int x)
    {
        for(int i = 0; i < x; i++)
            player_skin_owned.Add(false);
    }

    public void IncrementObstacleSkinOwnedSize()
    {
        obstacle_skin_owned.Add(false);
    }

    public int GetSizeOfPlayerOwned()
    {
        return player_skin_owned.Count;
    }

    public int GetSizeOfObstacleOwned()
    {
        return obstacle_skin_owned.Count;
    }
}
