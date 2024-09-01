using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : Singleton<AudioManager>
{
    private Bus masterBus;
    private Bus musicBus;
    private Bus uiBus;
    private Bus ambientBus;
    private Bus sfxBus;

    protected override void Awake()
    {
        base.Awake();
        masterBus = RuntimeManager.GetBus("bus:/");
        musicBus = RuntimeManager.GetBus("bus:/Music");
        uiBus = RuntimeManager.GetBus("bus:/UI");
        ambientBus = RuntimeManager.GetBus("bus:/Ambient");
        sfxBus = RuntimeManager.GetBus("bus:/SFX");
    }

    public void SetMasterVolume(float volume)
    {
        masterBus.setVolume(volume);
        Debug.Log("Master Volume: " + volume);
    }

    public void SetMusicVolume(float volume)
    {
        musicBus.setVolume(volume);
        Debug.Log("Music Volume: " + volume);
    }


    public void SetUIVolume(float volume)
    {
        uiBus.setVolume(volume);
        Debug.Log("UI Volume: " + volume);
    }

    public void SetAmbientVolume(float volume)
    {
        ambientBus.setVolume(volume);
        Debug.Log("Ambient Volume: " + volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxBus.setVolume(volume);
        Debug.Log("SFX Volume: " + volume);
    }
}