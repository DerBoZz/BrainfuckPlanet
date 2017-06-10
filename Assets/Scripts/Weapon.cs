using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {
    
    public int fireRate;
    public bool enabledWeapon;



    public abstract void Attack();

}
