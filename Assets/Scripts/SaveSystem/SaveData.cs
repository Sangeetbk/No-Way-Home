using UnityEngine;

[System.Serializable]
public class SaveData 
{
    // player location
    public SerializableVector3 PlayerPos;
    public SerializableVector3 PlayerRot;
    public SerializableVector3 PlayerLook;


    // player needs
    public float Health;
    public float Hunger;
    public float Thirst;
    public float Sleep;

    // inventory
    public SInventorySlot[] Inventory;

    // dropped items
    public SDroppedItem[] DroppedItems;

    // buildings


    // resources


    // npcs


    //time
    public float TimeOfDay;
}


[System.Serializable]
public struct SInventorySlot
{
    public bool Occupied;
    public string ItemID;
    public int Quantity;
    public bool Equipped;
}

[System.Serializable]
public struct SDroppedItem 
{ 
    public string ItemID;
    public SerializableVector3 Position;
    public SerializableVector3 Rotation;

}



