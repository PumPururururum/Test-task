using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Weapon : ScriptableObject
{
    public string WeaponName;
    public int WeaponDamage;
    public int AmmoToShoot;
    public AmmoType ammoType = new AmmoType();
    public enum AmmoType
    {
        Pistol,
        Rifle
    }

}
