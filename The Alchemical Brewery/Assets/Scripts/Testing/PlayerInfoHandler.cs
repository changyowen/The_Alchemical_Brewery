using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoHandler : MonoBehaviour
{
    public static PlayerInfoHandler Instance { get; private set; }

    public Animator playerAnim;
    public Vector3 playerPosition;

    public List<int> playerPotionHolderList = new List<int>();
    public List<int> playerIngredientHolder = new List<int>();

    public bool drinkingPotionState = false;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        PlayerPositionUpdate();
    }

    void PlayerPositionUpdate()
    {
        playerPosition = transform.position;
    }
}
