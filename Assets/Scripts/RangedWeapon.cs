using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon {


    public int ammunition;
    public GameObject projectilePrefab;

    private void Update()
    {
        Attack();
    }

    public override void Attack()
    {
        
        Instantiate(projectilePrefab, transform);
        
    }

    public void GatherAmmo(int amount)
    {
        ammunition += amount;
        enabledWeapon = true;
    }

    public void OutOfAmmo()
    {
        enabledWeapon = false;

    }

}
