using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreferablePotion : MonoBehaviour
{
    SpriteRenderer sr;
    public Sprite[] potionSprite;

    CustomerAttribute customerAttribute;

    // Start is called before the first frame update
    void Start()
    {
        sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        customerAttribute = transform.parent.GetComponent<CustomerAttribute>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(customerAttribute.preferablePotion)
        {
            case 1:
                {
                    sr.sprite = potionSprite[1];
                    break;
                }
            case 2:
                {
                    sr.sprite = potionSprite[2];
                    break;
                }
            case 3:
                {
                    sr.sprite = potionSprite[3];
                    break;
                }
            case 4:
                {
                    sr.sprite = potionSprite[4];
                    break;
                }
            case 5:
                {
                    sr.sprite = potionSprite[5];
                    break;
                }
            case 6:
                {
                    sr.sprite = potionSprite[6];
                    break;
                }
            case 7:
                {
                    sr.sprite = potionSprite[7];
                    break;
                }
            case 8:
                {
                    sr.sprite = potionSprite[8];
                    break;
                }
            case 9:
                {
                    sr.sprite = potionSprite[9];
                    break;
                }
            case 10:
                {
                    sr.sprite = potionSprite[10];
                    break;
                }
            case 11:
                {
                    sr.sprite = potionSprite[11];
                    break;
                }
            case 12:
                {
                    sr.sprite = potionSprite[12];
                    break;
                }
        }
    }
}
