using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {
    
    public int fireRate;
    public bool equipable;



    public abstract void Attack();

}
