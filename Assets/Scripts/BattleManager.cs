using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    
    private Character Player;
    private Character Enemy;
    private bool IsHead;
    
    private InventoryManager inventoryManager;

    public GameObject GameOverScreen;
    public Button buttonShoot;
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<Character>();
        Enemy = GameObject.Find("Enemy").GetComponent<Character>();
        inventoryManager  = GameObject.Find("Inventory").GetComponent<InventoryManager>();
    }

    private void Update()
    {

        if (Player.currentHealth <= 0)
        {
            GameOverScreen.SetActive(true);
        }

    }

    public void PlayerAttack()
    {
        
        if (Player.currentWeapon.ammoType == Weapon.AmmoType.Pistol)
        {
            ItemSlot rightSlot;
            rightSlot = inventoryManager.SearchItem("ѕатроны 9х18мм");

            if (rightSlot != null) CheckWeaponAmmo(rightSlot);
            else Debug.Log("NO Ammo");
        }

        if (Player.currentWeapon.ammoType == Weapon.AmmoType.Rifle)
        {
            ItemSlot rightSlot;
            rightSlot = inventoryManager.SearchItem("ѕатроны 5.45х39");
            if (rightSlot != null) CheckWeaponAmmo(rightSlot);
            else Debug.Log("NO Ammo");
        }

        
    }

    IEnumerator EnemyAttack()
    {
        buttonShoot.interactable = false;

        yield return new WaitForSeconds(1);
       
        if (IsHead)
        {
            Enemy.AttackTorso(Player);
            IsHead = false;
        }
        else
        {
            Enemy.AttackHead(Player);
            IsHead = true;
        }
        buttonShoot.interactable = true;
    }
    
    public void CheckWeaponAmmo(ItemSlot itemSlot)
    {
        if ( itemSlot.quantity >= Player.currentWeapon.AmmoToShoot)
        {
            itemSlot.quantity -= Player.currentWeapon.AmmoToShoot;
            itemSlot.UpdateSlot();
            Player.AttackTorso(Enemy);
            if (!IsEnemyDead())
                StartCoroutine(EnemyAttack());
            else GameVictory();
        }
        else if ( itemSlot.quantity > 0)
        {
            int leftoverAmmo = Player.currentWeapon.AmmoToShoot - itemSlot.quantity;
            string leftoverAmmoName = itemSlot.item.ItemName;
            itemSlot.isFull = false;
            ItemSlot nextItemSlot = inventoryManager.SearchItem(leftoverAmmoName);
            if (nextItemSlot == null)
            {
                Debug.Log("Not enough Ammo");
                itemSlot.isFull = true;
            }
            else if (nextItemSlot.quantity >= leftoverAmmo) 
            {
                nextItemSlot.quantity -= leftoverAmmo;
                itemSlot.EmptySlot();
                nextItemSlot.UpdateSlot();
                Player.AttackTorso(Enemy);
                if(!IsEnemyDead())
                    StartCoroutine(EnemyAttack());
                else GameVictory();
            }
        }
    }

    public bool IsEnemyDead()
    {
        if (Enemy.currentHealth <= 0)
        {
            return true;
        }
        else return false;
    }
    public void GameVictory()
    {
        
        StopAllCoroutines();
        Item selectedItem = inventoryManager.items[Random.Range(0, inventoryManager.items.Length)];
        inventoryManager.AddItem(selectedItem, selectedItem.maxStack);
        buttonShoot.interactable = false;
        inventoryManager.SaveInventory();
    }

    
    
}
