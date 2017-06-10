using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon {
    public int ammunition;
    public GameObject projectilePrefab;


    public override void Attack()
    {
        Instantiate(projectilePrefab, gameObject.GetComponentsInChildren<Transform>()[1].transform.position, transform.rotation);
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
