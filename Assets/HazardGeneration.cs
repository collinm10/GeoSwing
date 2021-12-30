using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardGeneration : MonoBehaviour
{
    public int HazardArrayLength;
    public int HazardGenerationOffset;

    public bool generated_obstacle = true;

    [SerializeField] GameObject HazardPrefab;
    [SerializeField] GameObject ScoreCollider;
    private float gap_size;

    private GameObject player;
    public GameObject[] hazards;
    public int hazard_pointer;
    private int last_offset;

    private GameObject[] score_colliders;

    //Top and bottom hazards
    private Transform upper_hazard;
    private Transform lower_hazard;

    //Range where center of gap can spawn
    private float top_of_spawn_range;
    private float bottom_of_spawn_range;

    //Half of gap size for the calculation in update (Division is resource intensive and we don't want to do that every frame)
    private float half_gap_size;

    //Hold previous gap location
    private float previous_gap_loc = 10;

    public float next_gap_offset;

    // Start is called before the first frame update
    void Start()
    {
        hazards = new GameObject[HazardArrayLength];
        score_colliders = new GameObject[2];

        //Find player
        player = GameObject.Find("plaer");

        //Find upper and lower hazard
        upper_hazard = GameObject.Find("UpperHazard").transform;
        lower_hazard = GameObject.Find("LowerHazard").transform;

        reset();

        //Based on gap size we want to calculate the range in which the center of the gap can spawn
        gap_size = 20f;

        half_gap_size = gap_size / 2;

        top_of_spawn_range = upper_hazard.position.y - half_gap_size;
        bottom_of_spawn_range = lower_hazard.position.y + half_gap_size + 5f;

    }

    public void reset()
    {
        previous_gap_loc = 10;
        hazard_pointer = 0;

        for(int i = 0; i < 20; i++)
            Destroy(hazards[i]);
        
        for(int i = 0; i < 2; i++)
        {
            Destroy(score_colliders[i]);
        }

        //Auto generate two hazard sets off the rip
        hazards[hazard_pointer] = Instantiate(HazardPrefab, new Vector3(player.transform.position.x + 20, -30, 0), Quaternion.identity);
        hazard_pointer++;

        hazards[hazard_pointer] = Instantiate(HazardPrefab, new Vector3(player.transform.position.x + 20, 40, 0), Quaternion.identity);
        hazard_pointer++;

        hazards[hazard_pointer] = Instantiate(HazardPrefab, new Vector3(player.transform.position.x + 70, -30, 0), Quaternion.identity);
        hazard_pointer++;

        hazards[hazard_pointer] = Instantiate(HazardPrefab, new Vector3(player.transform.position.x + 70, 40, 0), Quaternion.identity);
        hazard_pointer++;

        generated_obstacle = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Check if latest ball position is past the latest hazard
        if (hazards[0] != null)
        {

            //If hazards[] is full then wrap around.
            if (hazard_pointer >= HazardArrayLength)
            {
                hazard_pointer = 0;
                last_offset = 17;
            }
            else
            {
                if (hazard_pointer >= 3)
                {
                    last_offset = -3;
                }
            }


            if (player.transform.position.x > hazards[hazard_pointer + last_offset].transform.position.x)
            {
                //If current index in hazards[] is already taken then destroy first
                if (hazards[hazard_pointer] != null)
                    Destroy(hazards[hazard_pointer]);

                if (hazards[hazard_pointer + 1] != null)
                    Destroy(hazards[hazard_pointer+1]);

                //Create random location for gap
                float pos = Random.Range(previous_gap_loc - next_gap_offset, previous_gap_loc + next_gap_offset);

                pos = Mathf.Clamp(pos, bottom_of_spawn_range, top_of_spawn_range);

                //If it clamps then provide some variation
                if(pos == bottom_of_spawn_range)
                {
                    float random_offset = Random.Range(0, 5);
                    pos += random_offset;
                }

                //Generate score collider at gap
                if(score_colliders[0] == null)
                    score_colliders[0] = Instantiate(ScoreCollider, new Vector3(player.transform.position.x + HazardGenerationOffset, pos, 5), Quaternion.identity);
                else
                    score_colliders[1] = Instantiate(ScoreCollider, new Vector3(player.transform.position.x + HazardGenerationOffset, pos, 5), Quaternion.identity);

                //Based on this position we want the top vertical hazard halfway in between the top of the gap and the top hazard
                //First calculate the distance from the top of gap to the top hazard
                float top_dist = upper_hazard.position.y - (pos + half_gap_size);
                hazards[hazard_pointer] = Instantiate(HazardPrefab, new Vector3(player.transform.position.x + HazardGenerationOffset, (pos + half_gap_size) + top_dist / 2, 5), Quaternion.identity);

                float bottom_dist = (pos - half_gap_size) - lower_hazard.position.y;
                hazards[hazard_pointer + 1] = Instantiate(HazardPrefab, new Vector3(player.transform.position.x + HazardGenerationOffset, (pos - half_gap_size) - bottom_dist / 2, 5), Quaternion.identity);

                //Now scale based on distances
                hazards[hazard_pointer].transform.localScale = (new Vector2(hazards[hazard_pointer].transform.localScale.x, top_dist));
                hazards[hazard_pointer + 1].transform.localScale = (new Vector2(hazards[hazard_pointer].transform.localScale.x, bottom_dist));

                hazard_pointer += 2;

                generated_obstacle = false;

                //Set previous gap loc
                previous_gap_loc = pos;
            }
        }
    }
}
