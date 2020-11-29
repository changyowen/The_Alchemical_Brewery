using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldObject : MonoBehaviour
{
    SpriteRenderer sr;
    public Sprite[] ingredientSprite;
    GameObject parent;
    PlayerManager playerManager;

    int spriteIndex;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        parent = transform.parent.gameObject;
        playerManager = parent.GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteIndex = playerManager.holdObject;
        AssignSprite();
    }

    void AssignSprite()
    {
        switch(spriteIndex)
        {
            case 0:
                {
                    sr.sprite = null;
                    break;
                }
            case 1:
                {
                    sr.sprite = ingredientSprite[1];
                    break;
                }
            case 2:
                {
                    sr.sprite = ingredientSprite[2];
                    break;
                }
            case 3:
                {
                    sr.sprite = ingredientSprite[3];
                    break;
                }
            case 4:
                {
                    sr.sprite = ingredientSprite[4];
                    break;
                }
            case 5:
                {
                    sr.sprite = ingredientSprite[5];
                    break;
                }
            case 6:
                {
                    sr.sprite = ingredientSprite[6];
                    break;
                }
            case 7:
                {
                    sr.sprite = ingredientSprite[7];
                    break;
                }
            case 8:
                {
                    sr.sprite = ingredientSprite[8];
                    break;
                }
            case 9:
                {
                    sr.sprite = ingredientSprite[9];
                    break;
                }
            case 10:
                {
                    sr.sprite = ingredientSprite[10];
                    break;
                }
            case 11:
                {
                    sr.sprite = ingredientSprite[11];
                    break;
                }
            case 12:
                {
                    sr.sprite = ingredientSprite[12];
                    break;
                }
        }
    }
}
