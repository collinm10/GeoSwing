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

    public Customizer()
    {

    }

    public void SetPlayerSkinOwned(int index, bool boo)
    {
        player_skin_owned[index] = boo;
    }

    public void SetRopeSkinOwned(int index, bool boo)
    {
        rope_skin_owned[index] = boo;
    }

    public void SetObstacleSkinOwned(int index, bool boo)
    {
        obstacle_skin_owned[index] = boo;
    }
}
