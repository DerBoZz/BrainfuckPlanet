using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {
    public int damage;
    public int health;


    public void Damage(int damageGet)
    {
        health -= damageGet;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
