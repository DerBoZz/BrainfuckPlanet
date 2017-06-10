using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : Enemy {

    public int health = 50;
    public override void Damage(int damageGet)
    {
        health -= damageGet;
        if (health <= 0)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
