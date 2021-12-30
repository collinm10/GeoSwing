using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBoost : MonoBehaviour
{
    public PlayerMovement pm;

    // Update is called once per frame
    void Update()
    {
        pm.AddBoost();
    }
}
