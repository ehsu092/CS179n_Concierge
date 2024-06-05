using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class ConversationStarter : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation;

    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.F))
        //{

        //    //if (Cursor.lockState != CursorLockMode.Confined)
        //    //{

        //        Cursor.lockState = CursorLockMode.None;
        //        Cursor.lockState = CursorLockMode.Confined;
        //        Cursor.visible = true;
        //    //}
        //}
        if (ConversationManager.Instance != null)
        {
            if (ConversationManager.Instance.IsConversationActive)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                    ConversationManager.Instance.SelectPreviousOption();

                else if (Input.GetKeyDown(KeyCode.DownArrow))
                    ConversationManager.Instance.SelectNextOption();

                else if (Input.GetKeyDown(KeyCode.Return))
                    ConversationManager.Instance.PressSelectedOption();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("FrontdeskChar"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                ConversationManager.Instance.StartConversation(myConversation);
                Update();
            }
        }
    }

}