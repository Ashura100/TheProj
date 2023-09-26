using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class JourNuit : MonoBehaviour
{
    [Range(0, 24)]
    public float timeOfDay;
    public float orbitSpeed = 1.0f;
    public Light sun;
    public Light moon;
    public Volume skyVolume;
    public AnimationCurve starsCurve;

    PhysicallyBasedSky sky;


    bool isNight;

    void Start()
    {
        skyVolume.profile.TryGet(out sky);
    }

    void Update()
    {
        timeOfDay += Time.deltaTime * orbitSpeed;
        if (timeOfDay > 24)
            timeOfDay = 0;
        
        UpdateTime();
    }

    void OnValidate()
    {
        skyVolume.profile.TryGet(out sky);
        UpdateTime();
    }
    void UpdateTime()
    {
        float alpha = timeOfDay / 24.0f;
        float sunRotation = Mathf.Lerp(-90, 270, alpha);
        float moonRotation = sunRotation - 180;
        sun.transform.rotation = Quaternion.Euler(sunRotation, -150.0f, 0);
        moon.transform.rotation = Quaternion.Euler(moonRotation, -150.0f, 0);

        sky.spaceEmissionMultiplier.value = starsCurve.Evaluate(alpha) * 11000.0f;

        NightDayTransition();
    }

    void NightDayTransition()
    {
        if (isNight)
        {
            if (moon.transform.rotation.eulerAngles.x > 180)
            {
                StartDay();
            }
        }
        else
        {
            if (sun.transform.rotation.eulerAngles.x > 180)
            {
                StartNight();
            }
        }   
    }

    void StartDay()
    {
        isNight = false;
        sun.gameObject.SetActive(true);
        moon.gameObject.SetActive(false);
    }

    void StartNight()
    {
        isNight = true;
        sun.gameObject.SetActive(false);
        moon.gameObject.SetActive(true);
    }
}
