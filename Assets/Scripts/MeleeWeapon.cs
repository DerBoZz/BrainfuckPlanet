using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon {

    public float meleeRange;
    public float animationTime;
    public int damage;

    public MeleeWeapon()
    {
        enabled = false;
    }

    public override void Attack(Vector3 direction)
    {
        StartCoroutine(ChargeAttack(direction));
    }

    IEnumerator ChargeAttack(Vector3 direction)
    {
        //Play Animation
        yield return new WaitForSeconds(animationTime);
        //instead of transform.position handposition? , 
        RaycastHit rh;
        
        if (Physics.Raycast(transform.position, direction, out rh, meleeRange, LayerMask.NameToLayer("Player")))
        {
            rh.collider.gameObject.gameObject.GetComponent<Player>().Damage(damage);
        }
    }
}
