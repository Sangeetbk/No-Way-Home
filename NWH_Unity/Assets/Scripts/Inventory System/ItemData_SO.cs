using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGS.Inventory
{
    [CreateAssetMenu(fileName = "New Item",menuName = "SGS/Inventory/Item")]
    public class ItemData_SO : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private string _itemId = null;

        [Header("Info")]
        [SerializeField] private string _displayName = null;
        [SerializeField] [TextArea] private string _description = null;

        [SerializeField] private Sprite _icon = null;
        public GameObject DropPrefab;
        public ItemType ItemBehaviour;

        [Header("Stackable")] 
        [SerializeField] private bool CanStack = false;

        public int MaxStackAmount;

        static Dictionary<string, ItemData_SO> itemLookupCache;

         public static ItemData_SO GetFromID(string itemID)
        {
            if (itemLookupCache == null)
            {
                itemLookupCache = new Dictionary<string, ItemData_SO>();
                var itemList = Resources.LoadAll<ItemData_SO>("");
                foreach (var item in itemList)
                {
                    if (itemLookupCache.ContainsKey(item._itemId))
                    {
                        Debug.LogError(string.Format("Looks like there's a duplicate GameDevTV.UI.InventorySystem ID for objects: {0} and {1}", itemLookupCache[item.GetItemId()], item));
                        continue;
                    }

                    itemLookupCache[item._itemId] = item;
                }
            }

            if (itemID == null || !itemLookupCache.ContainsKey(itemID)) return null;
            return itemLookupCache[itemID];
        }

        public Sprite GetIcon()
        {
            return _icon;
        }

        public string GetItemId()
        {
            return _itemId;
        }

        public string GetDisplayName()
        {
            return _displayName;
        }

        public string GetDescription()
        {
            return _description;
        }

        public bool IsStackable()
        {
            return CanStack;
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            if(string.IsNullOrWhiteSpace(_itemId))
            {
                _itemId = System.Guid.NewGuid().ToString();
            }
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {

        }
    }

    public enum ItemType
    {
        Resource,
        Equipable,
        Consumable,
    }

    public enum ItemVariety
    {
        None,
        Pickable,
        Openable
    }

}