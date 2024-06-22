using SGS.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemViewer3D : MonoBehaviour, IDragHandler
{
    public ItemData_SO targetItem;
    public TextMeshProUGUI itemName;
    private GameObject itemInstance;

    public float rotationSpeed = 5f;        // Speed of rotation
    public Vector3 itemPosition = new Vector3(0, 0, 2);  // Position to instantiate the item
    public Transform itemParent;             // Parent transform to hold the item instance

    private void Start()
    {
        if (itemParent == null)
        {
            itemParent = new GameObject("ItemParent").transform;
        }
    }

    private void OnDestroy()
    {
        // Clean up the instantiated item when this object is destroyed
        if (itemInstance != null)
        {
            Destroy(itemInstance);
        }
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            ShowItem();
        }
    }

    private void ShowItem()
    {
        // Destroy the previously instantiated item, if any
        if (itemInstance != null)
        {
            Destroy(itemInstance);
        }

        if (targetItem == null || targetItem.DropPrefab == null)
        {
            itemName.text = "No Item to Display";
            Debug.LogWarning("Target item or DropPrefab is null.");
            return;
        }

        // Set the item name text
        itemName.text = targetItem.name;

        // Instantiate the new item and set its position and rotation
        itemInstance = Instantiate(targetItem.DropPrefab, itemPosition, Quaternion.identity, itemParent);

        // Reset rotation to default
        itemInstance.transform.localEulerAngles = Vector3.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (itemInstance == null)
        {
            Debug.LogWarning("No item instance to rotate.");
            return;
        }

        float rotationX = -eventData.delta.x * rotationSpeed * Time.deltaTime;
        float rotationY = eventData.delta.y * rotationSpeed * Time.deltaTime;

        // Apply the rotation to the item instance
        itemInstance.transform.Rotate(Vector3.up, rotationX, Space.World);
        itemInstance.transform.Rotate(Vector3.right, rotationY, Space.World);
    }
}
