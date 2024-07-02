using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Item defenseHead;
    public Item defenseTorso;

    public Weapon currentWeapon;

    public bool IsHead;
    public bool IsTorso;
    public bool IsAlive;

    public HealthBar healthBar;
    public Image imageHead;
    public Image imageTorso;
    public TMP_Text textHead;
    public TMP_Text textTorso;
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
       
    }

    public void AttackHead(Character character)
    {
        character.currentHealth -= currentWeapon.WeaponDamage - character.defenseHead.AmountToChangeStat;
        character.healthBar.SetHealth(character.currentHealth);
    }
    public void AttackTorso(Character character)
    {
        character.currentHealth -= currentWeapon.WeaponDamage - character.defenseTorso.AmountToChangeStat;
        character.healthBar.SetHealth(character.currentHealth);
    }

    public void Heal(int health)
    {
        currentHealth += health;
        if(currentHealth > 100) currentHealth = 100;
        healthBar.SetHealth(currentHealth);
    }
    public void EquipHead(Item item)
    {
        defenseHead = item;
        imageHead.enabled = true;
        imageHead.sprite = item.sprite;
        textHead.text = item.AmountToChangeStat.ToString();
        IsHead = true;
    }
    public void EquipTorso(Item item)
    {
        defenseTorso = item;
        imageTorso.enabled = true;
        imageTorso.sprite = item.sprite;
        textTorso.text = item.AmountToChangeStat.ToString();
        IsTorso = true;
    }

    public void EquipWeapon(Weapon weapon)
    {
        currentWeapon = weapon;
    }
}
