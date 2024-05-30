using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pickup_flashlight : MonoBehaviour
{
    public GameObject Gun;
    public Transform weaponParent;
    public float dropForce = 5f;
    public float pickupDistance = 2f;
    public float inputX;
    public float inputY;
    public float inputZ;
    public float challengeThreshold = 0.7f;
    public Text successText;
    public Text pickupPromptText;

    private bool isPickedUp = false;
    public int challenge = 0; // Initialize the challenge count
    private GameObject bottoms;

    void Start()
    {
        Gun.GetComponent<Rigidbody>().isKinematic = true;
        if (successText != null)
        {
            successText.gameObject.SetActive(false);
        }
        if (pickupPromptText != null)
        {
            pickupPromptText.gameObject.SetActive(false);
        }
        bottoms = GameObject.Find("Flashlight");
        if (bottoms == null)
        {
            Debug.LogError("Flashlight object not found in the scene.");
        }
    }

    void Update()
    {
        if (isPickedUp && Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
        }
        Debug.Log("Challenge Count: " + challenge); // Move this line to see the updated count
    }

    void Drop()
    {
        Gun.transform.parent = null;
        Vector3 dropDirection = transform.forward;
        Rigidbody gunRigidbody = Gun.GetComponent<Rigidbody>();
        gunRigidbody.isKinematic = false;
        gunRigidbody.velocity = dropDirection * dropForce;

        Gun.GetComponent<MeshCollider>().enabled = true;
        isPickedUp = false;

        if (bottoms != null)
        {
            Vector3 bottomsPosition = bottoms.transform.position;
            Vector3 challengePosition = new Vector3(inputX, inputY, inputZ);
            float distanceToChallenge = Vector3.Distance(Gun.transform.position, challengePosition);
            float allowedDistance = challengeThreshold * pickupDistance;

            if (distanceToChallenge <= allowedDistance)
            {
                IncrementChallenge(); // Call the method to increment challenge count
                StartCoroutine(DisplaySuccessTextWithDelay(2f));
            }
        }
    }

    void Equip()
    {
        Gun.transform.position = weaponParent.transform.position;
        Gun.transform.rotation = weaponParent.transform.rotation;
        Gun.GetComponent<Rigidbody>().isKinematic = true;
        Gun.GetComponent<MeshCollider>().enabled = false;
        Gun.transform.SetParent(weaponParent);
        isPickedUp = true;
        if (pickupPromptText != null)
        {
            pickupPromptText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isPickedUp && other.gameObject.CompareTag("Player"))
        {
            float distance = Vector3.Distance(transform.position, other.transform.position);
            if (distance <= pickupDistance)
            {
                if (pickupPromptText != null)
                {
                    pickupPromptText.gameObject.SetActive(true);
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Equip();
                }
            }
            else
            {
                if (pickupPromptText != null)
                {
                    pickupPromptText.gameObject.SetActive(false);
                }
            }
        }
    }

    IEnumerator DisplaySuccessTextWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (successText != null)
        {
            successText.gameObject.SetActive(true);
            yield return new WaitForSeconds(3f);
            successText.gameObject.SetActive(false);
        }
    }

    // Method to increment the challenge count
    public void IncrementChallenge()
    {
        challenge++;
        Debug.Log("Challenge Count: " + challenge);
    }
}
