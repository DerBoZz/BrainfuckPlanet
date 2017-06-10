using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon {
    public int ammunition;
    public GameObject projectilePrefab;


    public override void Attack()
    {
        if(transform.localScale.x > 0.0f)
        {
            //Facing right
            Instantiate(projectilePrefab, gameObject.GetComponentsInChildren<Transform>()[1].transform.position, transform.rotation);
        }
        else if(transform.localScale.x > 0.0f)
        {
            //Facing Left
            Quaternion rot = transform.rotation;
            rot *= Quaternion.Euler(0, 0, 180);
            //Transform bullet = Instantiate(projectilePrefab, gameObject.GetComponentsInChildren<Transform>()[1].transform.position, transform.rotation) as Transform;
                
        }
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
