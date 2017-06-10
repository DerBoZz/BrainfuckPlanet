using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon {

    public float meleeRange;
    public int damage;

    void Start()
    {
    }

    public override void Attack()
    {

        RaycastHit rh;
        if (Physics.Raycast(transform.position, Vector3.forward, out rh, meleeRange, LayerMask.NameToLayer("Enemy")))
        {
            rh.collider.gameObject.GetComponent<Enemy>().Damage(damage);
        }
    }


}
