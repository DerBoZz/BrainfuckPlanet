using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : Enemy {
    public float meleeRange;
    public float animationTime;
    
    public float movementSpeed;
    public Transform[] waypoints;


    private int current;
    private bool forward;

    void Start()
    {
        forward = true;
        current = 0;
    }

    void Update()
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

    public void Attack()
    {
        StartCoroutine(ChargeAttack());
    }

    IEnumerator ChargeAttack()
    {
        //Play Animation
        yield return new WaitForSeconds(animationTime);
        //instead of transform.position handposition? 
        RaycastHit rh;
        if (Physics.Raycast(transform.position, Vector3.forward, out rh, meleeRange, LayerMask.NameToLayer("Player")))
        {
            rh.collider.gameObject.gameObject.GetComponent<Player>().Damage(damage);
        }
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
    
