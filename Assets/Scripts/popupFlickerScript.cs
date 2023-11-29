using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class popup : MonoBehaviour
{
    public Sprite newSprite;
    private Image imageComponent;
    private Sprite originalSprite;
    [SerializeField]
    private float flickerTime;

    private void Start()
    {
        imageComponent = GetComponent<Image>();
        
        originalSprite = imageComponent.sprite;
        
        InvokeRepeating("SwapSprite", 0f, flickerTime);
    }

    private void SwapSprite()
    {
        if (newSprite != null)
        {
            imageComponent.sprite = (imageComponent.sprite == originalSprite) ? newSprite : originalSprite;
        }
        else
        {
            Debug.LogWarning("New sprite not assigned. Please assign a sprite in the inspector.");
        }
    }
}
