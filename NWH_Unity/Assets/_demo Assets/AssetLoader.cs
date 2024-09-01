using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetLoader : MonoBehaviour
{
    [SerializeField] private AssetReference Part1;
    [SerializeField] private AssetReference Part2;
    [SerializeField] private AssetReference Part3;
    [SerializeField] private AssetReference Part4;
    [SerializeField] private AssetReference Part5;
    [SerializeField] private AssetReference Part6;

    private List<AsyncOperationHandle<GameObject>> _loadedAssetHandles = new List<AsyncOperationHandle<GameObject>>();
    [SerializeField] private List<GameObject> _spawnedAssets = new List<GameObject>();

    public enum CurrentZone { Zone1, Zone2, Zone3, Zone4 }
    public CurrentZone currentZone;

    private void Start()
    {
        LoadAsset(Part1);
        LoadAsset(Part2);
    }

    public void SwitchZone(CurrentZone zone)
    {
        

        switch (zone)
        {
            case CurrentZone.Zone1:
                LoadAsset(Part3);
                LoadAsset(Part4);
                break;

            case CurrentZone.Zone2:
                LoadAsset(Part5);
                LoadAsset(Part6);
                break;

            case CurrentZone.Zone3:
                UnloadCurrentZoneAssets();
                // Load assets for Zone3
                break;

            case CurrentZone.Zone4:
                // Load assets for Zone4
                break;
        }

        currentZone = zone;
    }

    private void LoadAsset(AssetReference assetRef)
    {
        var handle = assetRef.LoadAssetAsync<GameObject>();
        handle.Completed += OnSceneLoaded;
        _loadedAssetHandles.Add(handle);
    }

    private void OnSceneLoaded(AsyncOperationHandle<GameObject> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject loadedAsset = obj.Result;
            var instance = Instantiate(loadedAsset);
            _spawnedAssets.Add(instance);
        }
        else
        {
            Debug.LogError("Failed to load asset.");
        }
    }

    private void UnloadCurrentZoneAssets()
    {
        // Destroy all instantiated objects
        foreach (var spawnedAsset in _spawnedAssets)
        {
            Destroy(spawnedAsset);
        }

        _spawnedAssets.Clear();

        // Release all loaded asset handles
        foreach (var handle in _loadedAssetHandles)
        {
            Addressables.Release(handle);
        }

        _loadedAssetHandles.Clear();
    }

    private void OnDestroy()
    {
        UnloadCurrentZoneAssets();
    }
}
