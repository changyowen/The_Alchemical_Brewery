using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFieldOfViewHandler : MonoBehaviour
{
    RayCastMovement rayCastMovement;
    public CinemachineVirtualCamera virtualCameraMain;

    public float scrollSensitivity = 20f;
    public float teleportView = 50f;

    float fieldOfView = 32f;
    bool isTransitioning = false;

    private void Start()
    {
        rayCastMovement = GetComponent<RayCastMovement>();
        fieldOfView = virtualCameraMain.m_Lens.FieldOfView;
    }

    private void Update()
    {
        if(virtualCameraMain != null) //camera not null
        {
            if(!StageManager.pauseGame && StageManager.dayTimeGameplay) //is not pause game
            {
                if(rayCastMovement.rayCastMode == RayCastMovementMode.Normal) //normal mode
                {
                    float distanceNeededTravel = Mathf.Abs(fieldOfView - virtualCameraMain.m_Lens.FieldOfView);
                    if(distanceNeededTravel > 0.5f)
                    {
                        virtualCameraMain.m_Lens.FieldOfView = Mathf.Lerp(virtualCameraMain.m_Lens.FieldOfView, fieldOfView, 2 * Time.unscaledDeltaTime);
                    }
                    else
                    {
                        if (Input.GetAxis("Mouse ScrollWheel") < 0f) //forward
                        {
                            fieldOfView += scrollSensitivity * Time.unscaledDeltaTime;
                        }
                        else if (Input.GetAxis("Mouse ScrollWheel") > 0f) // backwards
                        {
                            fieldOfView -= scrollSensitivity * Time.unscaledDeltaTime;
                        }

                        virtualCameraMain.m_Lens.FieldOfView = Mathf.Clamp(fieldOfView, 25f, 50f);
                        fieldOfView = virtualCameraMain.m_Lens.FieldOfView;
                    }
                }
                else if(rayCastMovement.rayCastMode == RayCastMovementMode.Teleport)
                {
                    float distanceNeededTravel = Mathf.Abs(teleportView - virtualCameraMain.m_Lens.FieldOfView);
                    if (distanceNeededTravel > 0.5f)
                    {
                        virtualCameraMain.m_Lens.FieldOfView = Mathf.Lerp(virtualCameraMain.m_Lens.FieldOfView, teleportView, 2 * Time.unscaledDeltaTime);
                    }
                }
            }
        }
    }
}
