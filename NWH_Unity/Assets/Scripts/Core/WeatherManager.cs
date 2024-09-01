using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.VFX;
public class WeatherManager : MonoBehaviour
{
    //public static WeatherManager Instance { get; private set; }

    [SerializeField, Range(0f, 1f)]
    private float _rainIntensity;

    [SerializeField, Range(0f, 1f)]
    private float _windIntensity;

    [SerializeField, Range(0f, 1f)]
    private float _fogIntensity;

    [SerializeField, Range(0f, 1f)]
    private float _cloudIntensity;

    [SerializeField, Range(0f, 1f)]
    private float _lightIntensity;

    [SerializeField]
    private bool _itsRaining = false;



    /// <summary>
    /// //////////////////////////////////////////////////////////////////
    /// </summary>
    [SerializeField]
    private VisualEffect _rainVFX;

    [SerializeField]
    private Light _sunLight;
    [SerializeField]
    private Volume _fogVolume;


    private float previousRainIntensity;
    private float previousFogIntensity;

    private void Start()
    {
         

    }
    private void Update()
    {


        if (_itsRaining)
        {
            _rainVFX.Play();
            CalculateRainEffect();
            CalculateFogEffect();
            _sunLight.intensity = 8001f;
        }
        else
        {
            ClearWeather();
        }
       
       
    }

  

    private void CalculateFogEffect()
    {
        _fogVolume.gameObject.SetActive(true);
        if (_fogIntensity != previousFogIntensity)
        {
            previousRainIntensity = -_fogIntensity;
            _fogVolume.weight = _fogIntensity;
        }
    }

    private void CalculateRainEffect()
    {
        if (_rainIntensity != previousRainIntensity)
        {
            previousRainIntensity = _rainIntensity;
            _rainVFX.SetFloat("Intensity", _rainIntensity);
             
        }
    }

    private void ClearWeather()
    {
        _rainVFX.Stop();
        _windIntensity = 0;
        _fogVolume.gameObject.SetActive(false);
        _fogVolume.weight = 0;
        _windIntensity = 0;
        _sunLight.intensity = 80001f;
    }
}
