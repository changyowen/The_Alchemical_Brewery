using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAI : MonoBehaviour
{
    public FishingMiniGame fishingMiniGame;
    public Transform fishMarker;
    public Transform topPivot_obj;
    public Transform bottomPivot_obj;

    public float smoothMotion = 1;
    public float timerMultiplicator = 3;

    float fishTimer = 0;
    float fishDestination;
    float fishPosition = .5f;
    float fishSpeed;

    void Update()
    {
        if(fishingMiniGame.startMiniGame)
        {
            FishSwinging();
        }
    }

    void FishSwinging()
    {
        fishTimer -= Time.deltaTime;
        if (fishTimer < 0)
        {
            fishTimer = UnityEngine.Random.value * timerMultiplicator;

            fishDestination = UnityEngine.Random.value;
        }

        fishPosition = Mathf.SmoothDamp(fishPosition, fishDestination, ref fishSpeed, smoothMotion);
        fishMarker.position = Vector3.Lerp(bottomPivot_obj.position, topPivot_obj.position, fishPosition);
    }

    public void ResetFishAIValue()
    {
        fishTimer = 0;
        fishPosition = .5f;
    }
}
