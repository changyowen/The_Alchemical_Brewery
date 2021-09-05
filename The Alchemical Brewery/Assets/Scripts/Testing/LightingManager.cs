using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    //Scene References
    [SerializeField] private GameObject SunMoonParent = null;
    [SerializeField] private Light DirectionalLight = null;
    [SerializeField] private LightingPreset Preset = null;
    [SerializeField] Light[] streetLights = null;
    //Variables
    [SerializeField, Range(0, 24)] private float TimeOfDay = 12;
    [SerializeField] private float timeSpeed = 0;
    [SerializeField] private Vector2 streetLightTimer = Vector2.zero;

    public bool isStreetLightOn = false;
    public bool isGameScene = true;

    private void Update()
    {
        if (Preset == null)
            return;

        if (Application.isPlaying)
        {
            if(!isGameScene)
            {
                //(Replace with a reference to the game time)
                TimeOfDay += Time.deltaTime * timeSpeed;
                TimeOfDay %= 24; //Modulus to ensure always between 0-24
                UpdateLighting(TimeOfDay / 24f);
                //UpdateStreetLighting(0f);
                UpdateStreetLightingNew(0f);
            }
            else
            {
                float officialTimeOfDay = ClockSystem.Instance.TimeOfDay;
                UpdateLighting(officialTimeOfDay / 24f);
                //UpdateStreetLighting(officialTimeOfDay);
                UpdateStreetLightingNew(officialTimeOfDay);
            }
        }
        else
        {
            UpdateLighting(TimeOfDay / 24f);
        }
    }


    private void UpdateLighting(float timePercent)
    {
        //Set ambient and fog
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        //If the directional light is set then rotate and set it's color, I actually rarely use the rotation because it casts tall shadows unless you clamp the value
        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);

            //DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f), 90, 0));
            SunMoonParent.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, (timePercent * 360f)));
        }

    }



    //Try to find a directional light to use if we haven't set one
    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;

        //Search for lighting tab sun
        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        //Search scene for light that fits criteria (directional)
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }

    private void UpdateStreetLighting(float _timeOfScene)
    {
        if(!isGameScene)
        {
            for (int i = 0; i < streetLights.Length; i++)
            {
                if (TimeOfDay >= streetLightTimer.x || TimeOfDay <= streetLightTimer.y)
                {
                    streetLights[i].enabled = true;
                }
                else
                {
                    streetLights[i].enabled = false;
                }
            }
        }
        else
        {
            for (int i = 0; i < streetLights.Length; i++)
            {
                if (_timeOfScene >= streetLightTimer.x || _timeOfScene <= streetLightTimer.y)
                {
                    streetLights[i].enabled = true;
                }
                else
                {
                    streetLights[i].enabled = false;
                }
            }
        }
        
    }
    private void UpdateStreetLightingNew(float _timeOfScene)
    {
        if (!isGameScene)
        {
            if (TimeOfDay >= streetLightTimer.x || TimeOfDay <= streetLightTimer.y)
            {
                if (!isStreetLightOn)
                {
                    isStreetLightOn = true;
                    StartCoroutine(DelayLight(true));
                }

            }
            else
            {
                if (isStreetLightOn)
                {
                    isStreetLightOn = false;
                    StartCoroutine(DelayLight(false));
                }
            }
        }


        //else
        //{
        //    for (int i = 0; i < streetLights.Length; i++)
        //    {
        //        if (_timeOfScene >= streetLightTimer.x || _timeOfScene <= streetLightTimer.y)
        //        {
        //            streetLights[i].enabled = true;
        //        }
        //        else
        //        {
        //            streetLights[i].enabled = false;
        //        }
        //    }
        //}

    }

    IEnumerator DelayLight(bool LightOn)
    {
        for(int i = 0; i < streetLights.Length; i++)
        {
            float waitTime = .2f;
            yield return new WaitForSeconds(waitTime);
            streetLights[i].enabled = LightOn;
            streetLights[i].intensity = 0;
            if (LightOn)
            {
                LightFlickering(i);
            }
        }
    }

    private void LightFlickering(int num)
    {
        for (int i = 0; i <= 15; i++)
        {
            streetLights[num].intensity = i;
            Debug.Log(streetLights[num] + " " + streetLights[num].intensity);
        }
    }
}
