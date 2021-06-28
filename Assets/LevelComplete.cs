using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject launcher;
    [SerializeField] Transform launcher_start;
    [SerializeField] Transform player_start;

    private PlayerMovement pm;

    void Start()
    {
        pm = player.GetComponent("PlayerMovement") as PlayerMovement;
    }
    public void level_complete()
    {
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        player.GetComponent<Rigidbody2D>().gravityScale = 0f;
        Debug.Log("Level Complete!");
    }

    public void level_failed()
    {
        Debug.Log("You suck!");
        reset_level();
    }

    public void reset_level()
    {
        launcher.transform.position = launcher_start.transform.position;
        player.transform.position = player_start.position;
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        pm.reset();
    }
}
