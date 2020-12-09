using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFollower : MonoBehaviour
{
    public Transform itemToFollow;

    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = itemToFollow.position + offset;
    }
}
