using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class LilypadManager : MonoBehaviour
{
    float secsToWait = 4f;
    float secsToWaitIncrease = 2f;

    List<List<GameObject>> lilypadArrs = new List<List<GameObject>>();

    // Start is called before the first frame update
    void Start()
    {
        // Gets all the lilypad parent objects
        List<GameObject> lpArrGameObjs = GameObject.FindGameObjectsWithTag("LilyPadParent").ToList();

        lpArrGameObjs.Sort((a, b) => Int32.Parse(a.name).CompareTo(Int32.Parse(b.name))); 

        foreach (GameObject lpArrParent in lpArrGameObjs)
        {
            // A list storing the lilypads in each parent
            List<GameObject> lpArrChildren = new List<GameObject>();

            for (int i = 0; i < lpArrParent.transform.childCount; i++)
            {
                // Adds a lilypad to the list
                lpArrChildren.Add(lpArrParent.transform.GetChild(i).gameObject);
            }

            lilypadArrs.Add(lpArrChildren);
        }

        foreach(var lPadList in lilypadArrs)
        {
            foreach(var lPad in lPadList)
                print(lPad.transform.parent.gameObject.name);
        }

        StartCoroutine(SendOffLilyPads());
    }

    IEnumerator SendOffLilyPads()
    {
        foreach (var lilypads in lilypadArrs)
        {
            foreach (var lilypad in lilypads)
            {
                lilypad.GetComponent<Lilypad>().StartMoving();
            }

            yield return new WaitForSeconds(secsToWait);
            secsToWait += secsToWaitIncrease;
        }
    }
    
}
