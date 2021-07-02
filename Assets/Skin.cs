using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin
{
    public string sprite_name;
    public string material_name;

    //0 is obstacle, 1 is rope, 2 is player
    public int type;

    public Skin(string sn, string mn, int t)
    {
        this.sprite_name = sn;
        this.material_name = mn;
        this.type = t;
    }
}
