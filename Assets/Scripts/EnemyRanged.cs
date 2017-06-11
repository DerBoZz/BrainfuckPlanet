using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : Enemy {

    private Animator anim;
    public GameObject projectile;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
        
    {
        if(collision.tag == "Player")
        {
            float angle = Vector3.Angle(transform.position, collision.transform.position);
            float distance = gameObject.GetComponent<CircleCollider2D>().radius;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            RaycastHit2D raycast = Physics2D.Raycast(transform.position, collision.transform.position-transform.position, distance);
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
            if (raycast.collider != null && raycast.collider.tag == "Player")
            {
                GameObject project = Instantiate(projectile, transform.position,Quaternion.identity);

            }
        }
        

        
    }

}
