using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SO_Holder : MonoBehaviour
{
    public static SO_Holder Instance { get; private set; }

    public Sprite transparentSprite;
    public IngredientData[] ingredientSO;
    public IngredientSpawnForce[] ingredientSpawnForceSO;

    void Awake()
    {
        Instance = this;
    }
}
