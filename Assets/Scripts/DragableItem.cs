using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class DragableItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler 
{
    public Image image;
    public TMP_Text text;
    [HideInInspector] public Transform ParentAfterDrag;
    public ItemSlot itemSlot;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        ParentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
        
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        transform.SetParent(ParentAfterDrag);
        image.raycastTarget = true;
        
    }

    
}
