using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
public class WeaponSelection : MonoBehaviour
{

    private Character Player;
    public Weapon weapon;
    public Image ThisImage;
    public Image OtherImage;
    private void Start()
    {
        Player = GameObject.Find("Player").GetComponent<Character>();
    }
    public void ChangeWeapon()
    {
        if (Player.currentWeapon != weapon)
        {
            Player.currentWeapon = weapon;
            ThisImage.enabled = true;
            OtherImage.enabled = false;
        }
    }
}
