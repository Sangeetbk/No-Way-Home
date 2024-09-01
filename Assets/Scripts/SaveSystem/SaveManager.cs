using SGS.Controls;
using SGS.Inventory;
using System.Collections;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public FirstPersonController PlayerController;
    public CameraController CameraController;
    public Inventory Inventory;

    private string _savePath;
    private void Start()
    {
        _savePath = Path.Combine(Application.persistentDataPath, "Saver");
       StartCoroutine(LoadGame()); 
    }

    IEnumerator LoadGame()
    {
        yield return new WaitForEndOfFrame();
        if (PlayerPrefs.HasKey("Save"))
        {
            Load();
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
           Save();
           Debug.Log("Saving from old system");
            
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
          LoadGame();
        }
    }
    public void Save()
    {
        
        SaveData data = new SaveData();

        // player needs
        /*data.Health = PlayerNeeds.Instance.health.curValue;
        data.Hunger = playerNeeds.Instance.hunger.curValue;
        data.Thirst = playerNeeds.Instance.thirst.curValue;
        data.Sleep = playerNeeds.Instance.sleep.curValue;
        */
        // inventory save system
        data.Inventory = new SInventorySlot[Inventory.Slots.Length];

        for (int i = 0; i < Inventory.Slots.Length; i++)
        {
            data.Inventory[i] = new SInventorySlot();
            data.Inventory[i].Occupied = Inventory.Slots[i].ItemData != null;

            if (!data.Inventory[i].Occupied)
                continue;

            data.Inventory[i].ItemID = Inventory.Slots[i].ItemData.GetItemId();
            data.Inventory[i].Quantity = Inventory.Slots[i].Quantity;
            data.Inventory[i].Equipped = Inventory.UISlots[i].Equipped;
        }

        // dropped items
        //ItemPickup[] droppedItems = Object.FindObjectsByType<ItemPickup>(FindObjectsSortMode.None);

      //  data.DroppedItems = new SDroppedItem[droppedItems.Length];

        // for (int i = 0; i < droppedItems.Length; i++)
        // {
        //     data.DroppedItems[i] = new SDroppedItem {
        //         ItemID = droppedItems[i].ItemData.GetItemId(),
        //         Position = new SerializableVector3(droppedItems[i].transform.position),
        //         Rotation = new SerializableVector3(droppedItems[i].transform.eulerAngles)
        //     };

        //}


        string rawData = JsonUtility.ToJson(data, true);
        File.WriteAllText(_savePath, rawData);
        Debug.Log("saving file");
    }

    public void Load()
    {
        string rawData = File.ReadAllText(_savePath);
        SaveData data = JsonUtility.FromJson<SaveData>(rawData);

        if (PlayerController != null)
        {
         //   PlayerController.transform.position = data.PlayerPos.GetVector3();
          //  PlayerController.transform.eulerAngles = data.PlayerRot.GetVector3();
        }
        else
        {
            Debug.LogError("PlayerController is not assigned.");

            if (CameraController != null)
            {
               // CameraController.transform.eulerAngles = data.PlayerLook.GetVector3();
            }
            else { Debug.LogError("CameraController is not assigned."); }


            if (Inventory != null)
            {
                // inventory
                
                int equippedItem = 999;

                for (int i = 0; i < data.DroppedItems.Length; i++)
                {
                    if (!data.Inventory[i].Occupied)
                        continue;

                    Inventory.Slots[i].ItemData = ObjectManager.Instance.GetItemByID(data.Inventory[i].ItemID);
                    Inventory.Slots[i].Quantity = data.Inventory[i].Quantity;

                    if (data.Inventory[i].Equipped)
                    {
                        equippedItem = i;
                    }
                }

                if (equippedItem != 999)
                {
                    Inventory.SelectItem(equippedItem);
                    Inventory.OnEquipButton();
                }
            }
        }
        Debug.Log("loading files");
    }
}
