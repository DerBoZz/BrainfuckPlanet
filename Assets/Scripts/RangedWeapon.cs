using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon {
    public int ammunition;
    public GameObject projectilePrefab;


    public override void Attack()
    {
        if(transform.parent.localScale.x > 0.0f)
        {
            //Facing right
            Instantiate(projectilePrefab, gameObject.GetComponentsInChildren<Transform>()[1].transform.position, transform.rotation);
        }
        else if(transform.parent.localScale.x < 0.0f)
        {
            //Facing Left
            Instantiate(projectilePrefab, gameObject.GetComponentsInChildren<Transform>()[1].transform.position, transform.rotation * Quaternion.Euler(0,0,180));
        }
        GetComponent<AudioSource>().Play();
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
