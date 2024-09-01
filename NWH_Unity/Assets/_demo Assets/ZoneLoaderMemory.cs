using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
public class ZoneLoaderMemory : MonoBehaviour
{
    [SerializeField] private List<AssetLabelReference> _assetLabelRef;
    private List<AsyncOperationHandle<GameObject>> _loadedAssetHandles = new List<AsyncOperationHandle<GameObject>>();
    private HashSet<int> _loadedIndices = new HashSet<int>(); // Track loaded indices

    public enum CurrentZone { Zone1, Zone2, Zone3, Zone4 }

    public CurrentZone currentZone;

    private void Start()
    {
        // Example: Switch to Zone1
        SwitchZone(CurrentZone.Zone1);
    }
    
    public void SwitchZone(CurrentZone zone)
    {
        switch (zone)
        {
            case CurrentZone.Zone1:
                Debug.Log("I'm from Zone1");
                LoadAssetAtIndex(0); 
                LoadAssetAtIndex(1); 
                break;
            
            case CurrentZone.Zone2:
              
                LoadAssetAtIndex(2); 
                LoadAssetAtIndex(3); 
                UnloadAssets();
                break;
            
            case CurrentZone.Zone3:
                
                LoadAssetAtIndex(4); 
                LoadAssetAtIndex(5); 
                UnloadAssets();

                break;
            
            case CurrentZone.Zone4:
                
                LoadAssetAtIndex(6); 
                LoadAssetAtIndex(7); 
                LoadAssetAtIndex(8); 
                UnloadAssets();

                break;
        }
    }

    private void LoadAssetAtIndex(int index)
    {
        if (index >= 0 && index < _assetLabelRef.Count && !_loadedIndices.Contains(index))
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(_assetLabelRef[index]);
            handle.Completed += OnSceneLoaded;
            _loadedAssetHandles.Add(handle);
            _loadedIndices.Add(index); // Mark this index as loaded
        }
        else
        {
            Debug.LogWarning($"Index {index} is out of range or already loaded.");
        }
    }
    private void UnloadAssets()
    {
        // Release each loaded asset
        foreach (var handle in _loadedAssetHandles)
        {
            Addressables.Release(handle);
        }
        _loadedAssetHandles.Clear();
        _loadedIndices.Clear(); // Reset the loaded indices when unloading
        Debug.Log("Previous zone assets unloaded.");
    }
    private void OnSceneLoaded(AsyncOperationHandle<GameObject> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject loadedAsset = obj.Result;
            // Instantiate or use the loaded asset
            Instantiate(loadedAsset);
        }
        else
        {
            Debug.LogError("Failed to load asset with label.");
        }
    }

  

    private void OnDestroy()
    {
        // Ensure that all assets are released when the object is destroyed
        UnloadAssets();
    }
    
    
}
