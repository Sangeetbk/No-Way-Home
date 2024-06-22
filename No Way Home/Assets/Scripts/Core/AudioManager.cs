using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private EventReference _clickSound;
    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one AudioManager in scene!");

        } 
        Instance = this;
    }

    public void PlayClickSound()
    {
        RuntimeManager.PlayOneShot(_clickSound);
        Debug.Log("Playing click sound");
    }
}
