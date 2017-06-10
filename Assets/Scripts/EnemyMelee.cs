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
    private Animator anim;

    void Start()
    {
        attacking = false;
        forward = true;
        current = 0;
        anim = gameObject.GetComponent<Animator>();
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
            ExecuteAttack();
            Debug.Log("Attacking");
        }
        
    }

    public void Attack(Vector2 aDirectionPlayer)
    {
        attacking = true;
        directionPlayer = aDirectionPlayer;
    }

    private void ExecuteAttack()
    {
        anim.SetBool("Attacking", true);

        if( anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            RaycastHit2D rh = Physics2D.Raycast(transform.position, directionPlayer, meleeRange, 1 << LayerMask.NameToLayer("Player"));
            if (rh.collider != null)
            {
                rh.collider.gameObject.GetComponent<Player>().Damage(damage);
            }
            anim.SetBool("Attacking", false);
        }


    }

    private IEnumerator ChargeAttack()
    {
        //Play Animation
        anim.SetBool("Attacking", true);
        yield return new WaitForSeconds(1);
        RaycastHit2D rh = Physics2D.Raycast(transform.position, directionPlayer, meleeRange, 1 << LayerMask.NameToLayer("Player"));
        if (rh.collider != null)
        {
            rh.collider.gameObject.GetComponent<Player>().Damage(damage);
        }
        attacking = false;
    }
}
    
