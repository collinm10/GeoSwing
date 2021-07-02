using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostBar : MonoBehaviour
{
    public float boost;
    public float prev_boost;
    public Slider boost_slide;

    // Update is called once per frame
    void Update()
    {
        if(boost != prev_boost)
        {
            boost_slide.value = boost * .01f;
            prev_boost = boost;
        }
    }
}
