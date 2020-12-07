using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        Debug.Log("ItemAssets instance defined!");
    }

    public Sprite Flower1Sprite;
    public Sprite Flower2Sprite;
    public Sprite Flower3Sprite;
    public Sprite Potion1Sprite;
    public Sprite Potion2Sprite; 
    public Sprite Potion3Sprite;
}
