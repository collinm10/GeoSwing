using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCollider : MonoBehaviour
{
    private BackgroundChunkGenerator bg_generator;

    // Start is called before the first frame update
    void Start()
    {
        bg_generator = GameObject.Find("GameManager").GetComponent("BackgroundChunkGenerator") as BackgroundChunkGenerator;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (gameObject.name)
        {
            case "BGUpCollider":
                bg_generator.GenerateBackgroundChunk(0, gameObject.transform.parent.gameObject);
                break;
            case "BGRightCollider":
                bg_generator.GenerateBackgroundChunk(1, gameObject.transform.parent.gameObject);
                break;
            case "BGDownCollider":
                bg_generator.GenerateBackgroundChunk(2, gameObject.transform.parent.gameObject);
                break;
            case "BGLeftCollider":
                bg_generator.GenerateBackgroundChunk(3, gameObject.transform.parent.gameObject);
                break;
        }

        Destroy(this.gameObject);
    }
}
