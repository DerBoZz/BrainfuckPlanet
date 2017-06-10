using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public int damage;

    public float moveSpeed = 6.0f;
    private float amtToMove;
    private float lifetime = 2.0f;

    //public GameObject onHitEffect;
	
	// Update is called once per frame
	void FixedUpdate () {
        amtToMove = moveSpeed * Time.deltaTime;
        transform.Translate(transform.position * amtToMove);
        lifetime -= Time.deltaTime;
        if(lifetime <= 0.0f)
        {
            Destroy(gameObject);
        }
	}


    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Enemy")
        {
            Enemy en = col.gameObject.GetComponent<Enemy>();
            en.Damage(damage);
        }

        //Instantiate(onHitEffect, transform.position, Quaternion.identity);

        //if(!this.gameObject.GetComponent<ParticleSystem>().IsAlive())
        //    Destroy(gameObject);
    }
}


