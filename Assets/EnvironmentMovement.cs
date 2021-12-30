using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentMovement : MonoBehaviour
{
    private GameObject upper_hazard;
    private GameObject lower_hazard;
    private GameObject player;
    private GameObject player_start;

    //Background stuff
    private GameObject reset_background_pos;
    private bool background_reset_already = false;
    [SerializeField] private GameObject BackgroundPrefab;

    // Start is called before the first frame update
    void Start()
    {
        upper_hazard = GameObject.Find("UpperHazard");
        lower_hazard = GameObject.Find("LowerHazard");
        player = GameObject.Find("plaer");
    }

    // Update is called once per frame
    void Update()
    {
        upper_hazard.transform.position = new Vector2(player.transform.position.x, upper_hazard.transform.position.y);
        lower_hazard.transform.position = new Vector2(player.transform.position.x, lower_hazard.transform.position.y);
    }

}
