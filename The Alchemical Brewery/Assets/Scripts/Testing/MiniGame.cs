using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame : MonoBehaviour
{
    //assigning use
    public Transform topPivot, bottomPivot;
    public Transform potionMarker;
    public Transform hookArea;
    public SpriteRenderer hookSpriteRenderer;
    public Transform resultBarContainer;

    //Editable value
    public float timerMultiplicator = 3;
    public float smoothMotion = 1;
    public float hookSize = 0.1f;
    public float hookPower = 5f;
    public float hookPullPower = 0.001f;
    public float hookGravityPower = 0.005f;
    public float hookProgressDegradationPower = 0.1f;

    //Internal Data
    float fishPosition;
    float fishDestination;
    float fishTimer;
    float fishSpeed;
    float hookPosition = .5f;
    float hookProgress = 1f;
    float hookPullVelocity;

    void Start()
    {
        ResizeHook();
    }

    void Update()
    {
        PotionSwinging();
        Hook();
        ResultUpdate();
    }

    void ResizeHook()
    {
        Bounds b = hookSpriteRenderer.bounds;
        float ysize = b.size.y;
        Vector3 ls = hookArea.localScale;
        float distance = Vector3.Distance(topPivot.position, bottomPivot.position);
        ls.y = (distance / ysize * hookSize);
        hookArea.localScale = ls;
    }

    void Hook()
    {
        float bottomPos = (hookSize / 2) / (topPivot.position.y - bottomPivot.position.y);
        float topPos = 1 - ((hookSize / 2) / (topPivot.position.y - bottomPivot.position.y));

        if (Input.GetMouseButton(0))
        {
            if (hookPosition == bottomPos)
            {
                hookPullVelocity = 0;
            }

            if (hookPullVelocity < 0)
            {
                hookPullVelocity += hookPullPower * Time.deltaTime * 2;
            }
            hookPullVelocity += hookPullPower * Time.deltaTime;
        }
        else
        {
            if(hookPosition == topPos)
            {
                hookPullVelocity = 0;
            }

            if (hookPullVelocity > 0)
            {
                hookPullVelocity -= hookPullPower * Time.deltaTime * 1.05f;
            }
            hookPullVelocity -= hookGravityPower * Time.deltaTime;
        }

        hookPosition += hookPullVelocity;
       
        hookPosition = Mathf.Clamp(hookPosition, bottomPos, topPos);
        hookArea.position = Vector3.Lerp(bottomPivot.position, topPivot.position, hookPosition);
    }

    void PotionSwinging()
    {
        fishTimer -= Time.deltaTime;
        if (fishTimer < 0)
        {
            fishTimer = UnityEngine.Random.value * timerMultiplicator;

            fishDestination = UnityEngine.Random.value;
        }

        fishPosition = Mathf.SmoothDamp(fishPosition, fishDestination, ref fishSpeed, smoothMotion);
        potionMarker.position = Vector3.Lerp(bottomPivot.position, topPivot.position, fishPosition);
    }

    void ResultUpdate()
    {
        Vector3 ls = resultBarContainer.localScale;
        ls.y = hookProgress;
        resultBarContainer.localScale = ls;

        float hookMin = hookPosition - (hookSize / 2 / 7);
        float hookMax = hookPosition + (hookSize / 2 / 7);
        Debug.Log(hookSize);
        if(hookMin > fishPosition || hookMax < fishPosition)
        {
            hookProgress -= hookProgressDegradationPower * Time.deltaTime;
        }

        hookProgress = Mathf.Clamp(hookProgress, 0, 1);
    }
}
