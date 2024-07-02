using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData 
{
    public List<string> itemNames = new List<string> { };
    public List<int> itemQuantities = new List<int> { };

    public string EquipedHead;
    public string EquipedTorso;

    public PlayerData(InventoryManager inventoryManager, Character character) 
    {
        int n = 0;
        for (int i = 0; i < inventoryManager.itemSlot.Length; i++)
        {
            
            if (inventoryManager.itemSlot[i].isFull)
            {
                itemNames.Add(inventoryManager.itemSlot[i].item.ItemName);
                itemQuantities.Add(inventoryManager.itemSlot[i].quantity);
          
                n++;
            }
        }

        EquipedHead = character.defenseHead.ItemName;
        EquipedTorso = character.defenseTorso.ItemName;
    }
}
