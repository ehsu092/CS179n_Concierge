using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{
    public Transform teleportTarget;
    public GameObject Character;

    void OnTriggerEnter(Collider other)
    {
        Character.transform.position = teleportTarget.transform.position;
    }
}