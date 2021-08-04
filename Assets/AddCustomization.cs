using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCustomization : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Customizer customizer = SaveSystem.LoadCustomizer();
        Sprite sprite = Resources.Load<Sprite>("ForObstacles/ObstacleSkin" + customizer.GetActiveSkin(0));

        GameObject[] gos = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach(GameObject go in gos)
        {
            //Change sprite to active sprite
            go.GetComponent<SpriteRenderer>().sprite = sprite;
        }

        
    }

}
