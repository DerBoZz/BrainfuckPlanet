using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon {

    public float meleeRange;
    public float animationTime;
    public int damage;

    void Start()
    {
    }

    public override void Attack()
    {
        StartCoroutine(ChargeAttack());
    }

    IEnumerator ChargeAttack()
    {
        //Play Animation
        yield return new WaitForSeconds(animationTime);
        //instead of transform.position handposition? 
        RaycastHit rh;
        if (Physics.Raycast(transform.position, Vector3.forward, out rh, meleeRange, LayerMask.NameToLayer("Enemy")))
        {
            rh.collider.gameObject.GetComponent<Enemy>().Damage(damage);
        }
    }
}
