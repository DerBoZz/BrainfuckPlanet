using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon {
    private int ammunition;
    public GameObject projectilePrefab;

    public RangedWeapon()
    {
        ammunition = 0;
        enabled = false;
    }

    public override void Attack()
    {
        Instantiate(projectilePrefab, transform);
        ammunition--;
        if(ammunition <= 0)
        {
            enabled = false;
        }
    }

    public void GatherAmmo(int amount)
    {
        ammunition += amount;
        enabledWeapon = true;
    }
    

}
