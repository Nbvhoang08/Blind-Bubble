using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustSizeContent : MonoBehaviour
{
    public RawImage rawImage;

    // Start is called before the first frame update
    void Start()
    {
        int screenWidth = Screen.width;  // Chiều rộng màn hình (pixel)
        int screenHeight = Screen.height; // Chiều cao màn hình (pixel)

        Debug.Log("Screen Width: " + screenWidth);
        Debug.Log("Screen Height: " + screenHeight);

        rawImage.rectTransform.sizeDelta = new Vector2(screenHeight, screenHeight);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
