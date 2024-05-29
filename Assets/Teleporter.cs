// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Teleporter : MonoBehaviour
// {
//     public GameObject Player;
//     public GameObject TeleportTo;
//     public GameObject StartTeleporter;

//     private void OnTriggerEnter(Collider collision)
//     {
//         Debug.Log("OnTriggerEnter called with: " + collision.gameObject.name);

//         if (collision.gameObject.CompareTag("Teleporter"))
//         {
//             Debug.Log("Teleporter triggered");
//             Player.transform.position = TeleportTo.transform.position;
//         }

//         if (collision.gameObject.CompareTag("SecondTeleporter"))
//         {
//             Debug.Log("SecondTeleporter triggered");
//             Player.transform.position = StartTeleporter.transform.position;
//         }
//     }
// }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public int code;
    float disableTimer = 0;

    void Update()
    {
        if (disableTimer > 0)
        {
            disableTimer -= Time.deltaTime;
            Debug.Log("Timer running: " + disableTimer);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("OnTriggerEnter called with: " + collider.gameObject.name);

        if (collider.CompareTag("Player") && disableTimer == 0)
        {
            Debug.Log("Player entered teleporter with code: " + code);

            Teleporter[] teleporters = FindObjectsOfType<Teleporter>();
            Debug.Log("Found teleporters: " + teleporters.Length);

            foreach (Teleporter tp in teleporters)
            {
                Debug.Log("Checking teleporter with code: " + tp.code);

                if (tp.code == code && tp != this)
                {
                    Debug.Log("Teleporting to: " + tp.gameObject.name);

                    tp.disableTimer = 2;
                    Vector3 position = tp.gameObject.transform.position;
                    position.y += 2;
                    collider.gameObject.transform.position = position;

                    Debug.Log("Teleported player to: " + position);
                }
            }
        }
        else
        {
            Debug.Log("Conditions not met for teleportation.");
        }
    }
}
