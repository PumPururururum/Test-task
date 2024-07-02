using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Item;
public class PopUpManager : MonoBehaviour
{
    //PopUp Data
    public GameObject PopUp;
    public TMP_Text ItemName;
    public Image PopUpImage;
    public TMP_Text PopUpStatAmount;
    public TMP_Text PopUpWeight;
    public TMP_Text ActionName;
    public TMP_Text StatName;


    private ItemSlot currentItemSlot;
    private Item changingItem;

    private InventoryManager inventoryManager;
    private Character character;


    private void Start()
    {
        character = GameObject.Find("Player").GetComponent<Character>();
        inventoryManager = GameObject.Find("Inventory").GetComponent<InventoryManager>();
    }
    public void OpenPopUp(ItemSlot itemSlot)
    {
        if (itemSlot.isFull)
        {
            currentItemSlot = itemSlot;
            PopUp.SetActive(true);
            ItemName.text = itemSlot.item.ItemName;
            PopUpImage.sprite = itemSlot.item.sprite;
            PopUpStatAmount.text = itemSlot.item.AmountToChangeStat.ToString();
            
            PopUpWeight.text = (itemSlot.item.weight * itemSlot.quantity).ToString();

            if (itemSlot.item.type == Item.ItemType.Ammo)
                ActionName.text = "Купить";
            if (itemSlot.item.type == Item.ItemType.FirstAid)
                ActionName.text = "Лечить";
            if (itemSlot.item.type == Item.ItemType.TorsoDefense || itemSlot.item.type == Item.ItemType.HeadDefense)
                ActionName.text = "Экипировать";

            if (itemSlot.item.changeableStat == Item.ChangeableStat.Defense)
                StatName.text = "Защита:";
            if (itemSlot.item.changeableStat == Item.ChangeableStat.HP)
                StatName.text = "Лечение:";
            if (itemSlot.item.changeableStat == Item.ChangeableStat.none)
            {
                StatName.text = "";
                PopUpStatAmount.text = "";
            }
                
        }
    }
    public void ClosePopUp()
    {
        PopUp.SetActive(false);
    }

    public void Delete()
    {
        currentItemSlot.EmptySlot();
    }

    public void UseItem()
    {
        if (currentItemSlot.item.type == ItemType.Ammo)
        {
            inventoryManager.AddItem(currentItemSlot.item, currentItemSlot.item.maxStack);
           
        }

        if (currentItemSlot.item.type == ItemType.FirstAid)
        {
            character.Heal(currentItemSlot.item.AmountToChangeStat);
            currentItemSlot.quantity--;
        }

        if (currentItemSlot.item.type == ItemType.HeadDefense)
        {
            if (character.IsHead)
            {
                changingItem = currentItemSlot.item;
                currentItemSlot.item = character.defenseHead;
                character.EquipHead(changingItem);
            }
            else
            {
                character.EquipHead(currentItemSlot.item);
                currentItemSlot.quantity--;

            }
        }
        if (currentItemSlot.item.type == ItemType.TorsoDefense)
        {
            if (character.IsTorso)
            {
                changingItem = currentItemSlot.item;
                currentItemSlot.item = character.defenseTorso;
                
                character.EquipTorso(changingItem);
            }
            else
            {
                character.EquipTorso(currentItemSlot.item);
                currentItemSlot.quantity--;
            }
        }

        
        
        currentItemSlot.UpdateSlot();
        ClosePopUp();
    }
}
