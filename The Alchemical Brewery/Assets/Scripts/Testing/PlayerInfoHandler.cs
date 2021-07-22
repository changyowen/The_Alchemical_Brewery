using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoHandler : MonoBehaviour
{
    public static PlayerInfoHandler Instance { get; private set; }

    public Vector3 playerPosition;

    public int playerPotionHolder = 0;
    public List<int> playerIngredientHolder = new List<int>();

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
