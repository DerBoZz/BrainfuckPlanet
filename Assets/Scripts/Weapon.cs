using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {

    public int damage;

    public int fireRate;

    public bool enabledWeapon = false;



    public abstract void Attack();

}
