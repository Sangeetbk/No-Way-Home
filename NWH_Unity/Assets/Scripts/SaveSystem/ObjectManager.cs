using SGS.Inventory;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public ItemData_SO[] items;
    public Quest_SO[] questData;

   

    public static ObjectManager Instance;

    private void Awake()
    {
        Instance = this;

        // loading all the assets we needed
        items = Resources.LoadAll<ItemData_SO>("Items");
        questData = Resources.LoadAll<Quest_SO>("QuestItem");
    }

    private void Start()
    {
        // get all of the resources 
        
        
    }

    public ItemData_SO GetItemByID(string id)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].GetItemId() == id)
            {
                return items[i];
            }
            
        }
        Debug.LogError("No Item has been found");
        return null;
    }

    public Quest_SO GetQuestItemByID(string id)
    {
        for (int i = 0; i < questData.Length; i++)
        {
            if (questData[i].ID == id)
            {
                return questData[i];
            }

        }
        Debug.LogError("No Quest Item has been found");
        return null;
    }
}
