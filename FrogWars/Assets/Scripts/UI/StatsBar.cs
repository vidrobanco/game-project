using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsBar : MonoBehaviour
{
    public Component gameObjStats;

    public Transform statBar;

    // Update is called once per frame
    void Update()
    {
        float barScale;

        statBar.localScale = new Vector3(barScale, 1f, 1f);
    }
}
