using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {
    public int damage;
    public int health;


    public abstract void Damage(int damageGet);

}
