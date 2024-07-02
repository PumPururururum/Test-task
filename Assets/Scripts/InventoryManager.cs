using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;


public class InventoryManager : MonoBehaviour
{

    public ItemSlot[] itemSlot;
    public Item[] items;
    private Character player;

    
    private void Start()
    {
        //PlayerPrefs.SetInt("IsFirstTime", 0);
        player = GameObject.Find("Player").GetComponent<Character>();

        if (PlayerPrefs.GetInt("IsFirstTime") == 1)
            LoadInventory();
        if (PlayerPrefs.GetInt("IsFirstTime") == 0 )
        {
            PlayerPrefs.SetInt("IsFirstTime", 1);
  
            for (int i = 0; i < items.Length; i++)
            {
                AddItem(items[i], items[i].maxStack);
            }
            SaveInventory();
        
        }

    }

    public void AddItem(Item item, int itemQuantity)
    {

        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false)
            {
                itemSlot[i].AddItem(item, itemQuantity);
                return;
            }
        }
    }

    public ItemSlot SearchItem(string itemName)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == true && itemSlot[i].item.ItemName == itemName )
            {
                return itemSlot[i];
            }
        }
        return null;
    }

    public void SaveInventory()
    {
        SaveSystem.SavePlayer(this, player);
    }

    public void LoadInventory()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        for(int i = 0; i < data.itemNames.Count; i++)
        {
            for (int j = 0; j < items.Length; j++)
            {
                if (data.itemNames[i] == items[j].ItemName)
                    {
                        AddItem(items[j], data.itemQuantities[i]);
                    }
                if (data.EquipedTorso == items[j].ItemName)
                {
                    player.EquipTorso(items[j]);
                }
                if (data.EquipedHead == items[j].ItemName)
                {
                    player.EquipHead(items[j]);
                }
            }
        }
       
    }
}

