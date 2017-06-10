using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public int damage;

    public float moveSpeed = 6.0f;
    private float amtToMove;

    //public GameObject onHitEffect;
	
	// Update is called once per frame
	void FixedUpdate () {
        amtToMove = moveSpeed * Time.deltaTime;
        transform.Translate(transform.position * amtToMove);
	}


    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Enemy")
        {
            //Enemy enemy = other.gameObject.GetComponent("Enemy");
            //enemy.Damage(damage);
        }

        //Instantiate(onHitEffect, transform.position, Quaternion.identity);

        //if(!this.gameObject.GetComponent<ParticleSystem>().IsAlive())
        //    Destroy(gameObject);
    }
}


