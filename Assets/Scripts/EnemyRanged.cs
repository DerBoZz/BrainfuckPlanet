using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : Enemy {

    public int firerate;
    private Animator anim;
    public GameObject projectile;

    

    public void Attack(Collider2D collision)
        
    {
        if(collision.tag == "Player")
        {
            float angle = Vector3.Angle(transform.position, collision.transform.position);
            float distance = 30f;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponentInChildren<CircleCollider2D>().enabled = false;
            RaycastHit2D raycast = Physics2D.Raycast(transform.position, collision.transform.position-transform.position, distance);
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            gameObject.GetComponentInChildren<CircleCollider2D>().enabled = true;
            if (raycast.collider != null && raycast.collider.tag == "Player")
            {
                Vector3 dir = collision.transform.position- transform.position;
                float a = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                Quaternion rot = Quaternion.AngleAxis(a, Vector3.forward);
                Instantiate(projectile, transform.position,rot);

            }
        }        
    }
    }
