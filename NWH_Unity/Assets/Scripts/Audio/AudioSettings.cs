using UnityEngine;
using FMODUnity;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public void ChangeMasterVolume(float value)
    {
        AudioManager.Instance.SetMasterVolume(value);
    }

    public void ChangeMusicVolume(float value)
    {
        AudioManager.Instance.SetMusicVolume(value);
    }

    public void ChangeSFXVolume(float value)
    {
       AudioManager.Instance.SetSFXVolume(value);
    }

    public void ChangeAmbienceVolume(float value)
    {
       AudioManager.Instance.SetAmbientVolume(value);
    }

    public void ChangeUIVolume(float value)
    {
        AudioManager.Instance.SetUIVolume(value);
    }

}
