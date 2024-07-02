using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string ItemName;

    public int maxStack;

    public Sprite sprite;

    public float weight;

    public ChangeableStat changeableStat = new ChangeableStat();
    public int AmountToChangeStat;

    public ItemType type = new ItemType();

    public enum ChangeableStat
    {
        none,
        HP,
        Defense
    }

    public enum ItemType
    {
        Ammo,
        FirstAid,
        TorsoDefense,
        HeadDefense
    }
    
}
