using System;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ZoneLoader : MonoBehaviour
{
    private AssetLoader zoneMemoryLoader;
    public enum CurrentArea
    {
        Area1,
        Area2,
        Area3
    }

    public CurrentArea _currentArea;

    private void Awake()
    {
        zoneMemoryLoader = GetComponentInParent<AssetLoader>();
        if (zoneMemoryLoader == null)
        {
            Debug.Log("Zone Memory Loader is not Loaded");
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            switch (_currentArea)
            {
                case CurrentArea.Area1:
                   zoneMemoryLoader.SwitchZone(AssetLoader.CurrentZone.Zone1);
                    break;
                case CurrentArea.Area2:
                   zoneMemoryLoader.SwitchZone(AssetLoader.CurrentZone.Zone2);
                    break;
                case CurrentArea.Area3:
                   zoneMemoryLoader.SwitchZone(AssetLoader.CurrentZone.Zone3);
                    break;
            }

        }
        
    }
}
