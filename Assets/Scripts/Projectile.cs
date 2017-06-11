﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public int damage;

    public string tagName;

    public float moveSpeed = 6.0f;
    private float lifetime = 2.0f;
    
	void FixedUpdate () {
        transform.Translate(new Vector2(1,0)*moveSpeed*Time.deltaTime);
        lifetime -= Time.deltaTime;
        if(lifetime <= 0.0f)
        {
            Destroy(gameObject);
        }
	}


    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == tagName)
        {
            col.gameObject.GetComponent<Enemy>().Damage(damage);
            Destroy(gameObject);
        }
    }
}


