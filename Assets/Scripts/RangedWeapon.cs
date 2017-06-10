using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon {
    private int ammunition;
    public GameObject projectilePrefab;

    void Start()
    {
        ammunition = 0;
        equipable = false;
    }

    public override void Attack()
    {
        Instantiate(projectilePrefab, transform);
        ammunition--;
        if(ammunition <= 0)
        {
            equipable = false;
        }
    }

    public void GatherAmmo(int amount)
    {
        ammunition += amount;
        equipable = true;
    }
    

}
