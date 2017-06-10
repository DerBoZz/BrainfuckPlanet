using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : Enemy {
    public float meleeRange;
    public float animationTime;
    
    public float movementSpeed;
    public Transform[] waypoints;

    private bool attacking;
    private int current;
    private bool forward;

    void Start()
    {
        attacking = false;
        forward = true;
        current = 0;
    }

    void Update()
    {
        if (!attacking)
        {
            if (transform.position != waypoints[current].transform.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, movementSpeed * Time.deltaTime);
            }
            else if (forward && current + 1 < waypoints.Length)
            {
                current++;
            }
            else if (!forward && current - 1 >= 0)
            {
                current--;
            }
            if (current + 1 >= waypoints.Length || current - 1 < 0)
            {
                forward = !forward;
            }
        }
        
    }

    public void Attack()
    {
        attacking = true;
        StartCoroutine(ChargeAttack());
    }

    IEnumerator ChargeAttack()
    {
        //Play Animation
        yield return new WaitForSeconds(animationTime);
        //instead of transform.position handposition? 
        RaycastHit2D rh = Physics2D.Raycast(transform.position, Vector2.right, meleeRange, LayerMask.NameToLayer("Player"));
        if (rh.collider != null)
        {
            rh.collider.gameObject.gameObject.GetComponent<Player>().Damage(damage);
        }
        attacking = false;
    }

    public override void Damage(int damageGet)
    {
        health -= damageGet;
        if(health <= 0)
        {
            //Play Death Animation + wait
            Destroy(gameObject);
        }
    }
}
    
