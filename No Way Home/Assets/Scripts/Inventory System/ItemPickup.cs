using UnityEngine;
using SGS.Inventory;

    public class ItemPickup : MonoBehaviour, IInteractable
    {
        public ItemVariety ItemVariety;
        public ItemData_SO ItemData;

        public string GetInteractPrompt()
        {
        return ItemData.GetDisplayName();
        }

        public void OnInteract()
        {
            Inventory.Instance.AddItem(ItemData);
            Destroy(gameObject);
        }

    }


