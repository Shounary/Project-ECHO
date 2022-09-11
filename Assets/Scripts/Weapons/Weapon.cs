using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponClass
{
    ArmWeapon,
    ShoulderWeapon,
    SpinalWeapon,
}

public enum WeaponDamageType
{
    Kinetic,
    Energy,
    Explosive,
}

public class Weapon : MonoBehaviour
{
    [SerializeField] protected string weaponName;
    [SerializeField] protected WeaponDamageType weaponDamageType;
}
