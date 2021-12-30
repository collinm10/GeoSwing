using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Customizer
{
    private List<bool> player_skin_owned;
    private List<bool> obstacle_skin_owned;

    public int active_player_skin_index;
    public int active_obstacle_skin_index;

    public Customizer()
    {
        //Player starts out owning and using index 0
        active_obstacle_skin_index = 0;
        active_player_skin_index = 0;

        //Initialize the owned lists
        player_skin_owned = new List<bool>();
        obstacle_skin_owned = new List<bool>();

        //Add all bools for each player skin in each folder
        DirectoryInfo playerDir = new DirectoryInfo("Assets/Resources/ForPlayer");
        FileInfo[] playerInfo = playerDir.GetFiles("*.png");
        
        foreach(FileInfo fi in playerInfo)
        {
            player_skin_owned.Add(false);
        }
        
        //Add all bools for each obstacle skin in each folder
        DirectoryInfo obstDir = new DirectoryInfo("Assets/Resources/ForObstacles");
        FileInfo[] obstInfo = obstDir.GetFiles("*.png");
        
        foreach(FileInfo fi in obstInfo)
        {
            obstacle_skin_owned.Add(false);
        }

        
        if(player_skin_owned.Count > 0)
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

    public void IncrementPlayerSkinOwnedSize()
    {
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
