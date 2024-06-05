using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTrigger2 : MonoBehaviour
{
    public GameObject teleporter1;
    public GameObject teleporter2;

    void Start()
    {
        if (teleporter1 != null)
        {
            teleporter1.SetActive(false);
        }
        if (teleporter2 != null)
        {
            teleporter2.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateTeleporters();
        }
    }

    void ActivateTeleporters()
    {
        if (teleporter1 != null)
        {
            teleporter1.SetActive(true);
        }
        if (teleporter2 != null)
        {
            teleporter2.SetActive(true);
        }
    }
}
