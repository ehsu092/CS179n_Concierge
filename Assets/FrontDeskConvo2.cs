using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSystem : MonoBehaviour
{
    bool player_dectection = false;

    // Update is called once per frame
    void Update()
    {

        if (player_dectection && Input.GetKeyDown(KeyCode.E))
        {
            print("it worked somehow?!?!");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "PlayerBody")
        {
            player_dectection = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        player_dectection = false;
    }

}