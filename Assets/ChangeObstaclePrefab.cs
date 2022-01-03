using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeObstaclePrefab : MonoBehaviour
{
    public GameObject ObstaclePrefab;

    // Start is called before the first frame update
    void Start()
    {
        Customizer customizer = SaveSystem.LoadCustomizer();
        ObstaclePrefab.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ForObstacles/ObstacleSkin" + customizer.GetActiveSkin(0));
    }

}
