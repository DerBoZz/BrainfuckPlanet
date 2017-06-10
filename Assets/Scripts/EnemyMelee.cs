using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : Enemy {
    public float meleeRange;
    public float animationTime;



    void Update()
    {

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
    }
}
    
