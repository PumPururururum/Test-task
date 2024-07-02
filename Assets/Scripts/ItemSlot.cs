using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro.EditorUtilities;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IDropHandler
{

    //Item Data
    public bool isFull;
    public Item item;
    public int quantity;

    //Item Slot
    public TMP_Text quantityText;
    [SerializeField]
    private Image itemImage;
    public GameObject ItemPlace;

    private PopUpManager popUpManager;

    private void Start()
    {
        popUpManager = GameObject.Find("PopUpManager").GetComponent<PopUpManager>();
    }
    public void AddItem(Item item, int quantity)
    {
        
        ItemPlace.SetActive(true);
        this.item = item;
        isFull = true;
        this.quantity = quantity;
        if (quantity > 1)
        {
            quantityText.enabled = true;
            quantityText.text = quantity.ToString();
        }
        
        itemImage.enabled = true;
        itemImage.sprite = item.sprite;
    }
    public void EmptySlot()
    {
        quantityText.enabled = false;
        itemImage.enabled = false;
        isFull = false;
        popUpManager.ClosePopUp();
        ItemPlace.SetActive(false);

    }

    public void UpdateSlot()
    {
        quantityText.text = quantity.ToString();
        itemImage.sprite = item.sprite;
        if (quantity <= 0)
        {
            EmptySlot();
        }

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left) 
        {
            popUpManager.OpenPopUp(this);
        }
    }


    public void OnDrop(PointerEventData eventData)
    {
        if(isFull == false)
        {
            GameObject dropped = eventData.pointerDrag;
            DragableItem dragableItem = dropped.GetComponent<DragableItem>();
            //dragableItem.ParentAfterDrag = transform;
            AddItem(dragableItem.itemSlot.item, dragableItem.itemSlot.quantity);

            dragableItem.itemSlot.isFull = false;
            //dragableItem.itemSlot.quantityText.enabled = false;
            //dragableItem.itemSlot.itemImage.enabled = false;
            dragableItem.text.enabled = false;
            dragableItem.image.enabled = false;  

            //dragableItem.itemSlot = this;
           

        }
    }
}
