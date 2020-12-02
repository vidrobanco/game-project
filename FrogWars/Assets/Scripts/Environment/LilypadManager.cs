using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilypadManager : MonoBehaviour
{
    float secsToWait = 4f;
    float secsToWaitIncrease = 2f;

    List<List<GameObject>> lilypadArrs = new List<List<GameObject>>();

    // Start is called before the first frame update
    void Start()
    {
        // Gets all the lilypad parent objects
        var lpArrGameObjs = GameObject.FindGameObjectsWithTag("LilyPadParent"); 

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

        StartCoroutine(SendOffLilyPads());
    }

    IEnumerator SendOffLilyPads()
    {
        foreach (var lilypads in lilypadArrs)
        {
            foreach (var lilypad in lilypads)
            {
                print(lilypad.name);
                lilypad.GetComponent<Lilypad>().StartMoving();
            }

            yield return new WaitForSeconds(secsToWait);
            secsToWait += secsToWaitIncrease;
        }
    }
    
}
