using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : Enemy {
    public float meleeRange;
    
    public float movementSpeed;
    public Transform[] waypoints;

    private bool attacking;
    private int current;
    private bool forward;
    private Vector2 directionPlayer;

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
        else
        {
            StartCoroutine(ChargeAttack());
        }
        
    }

    public void Attack(Vector2 aDirectionPlayer)
    {
        attacking = true;
        directionPlayer = aDirectionPlayer;
    }

    private IEnumerator ChargeAttack()
    {
        //Play Animation
        yield return new WaitForSeconds(1);
        RaycastHit2D rh = Physics2D.Raycast(transform.position, directionPlayer, meleeRange, 1 << LayerMask.NameToLayer("Player"));
        if (rh.collider != null)
        {
            rh.collider.gameObject.GetComponent<Player>().Damage(damage);
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
    
