using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewInventoryData", menuName = "Inventory/InventoryData")]
public class InventoryData : ScriptableObject
{
    public List<string> collectedItems = new List<string>();
    public int importantItemCount = 0;

    public void AddItem(string itemName, bool isImportant)
    {
        collectedItems.Add(itemName);
        if (isImportant)
        {
            importantItemCount++;
        }
    }

    public void ClearData()
    {
        collectedItems.Clear();
        importantItemCount = 0;
    }
}
