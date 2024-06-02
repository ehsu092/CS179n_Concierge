using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoDisplay : MonoBehaviour
{
    public RawImage photoDisplayImage;

    public IEnumerator DisplayPhoto(Texture2D photo, float displayDuration)
    {
        // Set the photo texture to the RawImage component
        photoDisplayImage.texture = photo;

        // Display the RawImage component
        photoDisplayImage.gameObject.SetActive(true);

        // Wait for the specified display duration
        yield return new WaitForSeconds(displayDuration);

        // Hide the RawImage component after displaying for the specified duration
        photoDisplayImage.gameObject.SetActive(false);
    }
}
