using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Materials")]
public class Material : ScriptableObject
{
    public Item.itemType itemType;
    public string MaterialName;
    public Sprite MaterialSprite;
}
