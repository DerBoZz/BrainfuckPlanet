using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {
    public int damage;
    public int health;

    public Transform position1;
    public Transform position2;
    public float movementSpeed;

    public abstract void Damage(int damageGet);

}
