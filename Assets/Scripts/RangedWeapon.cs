using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon {
    private int ammunition;
    public GameObject projectilePrefab;

    void Start()
    {
        ammunition = 0;
    }

    public override void Attack()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(PlayerStats.weaponList[PlayerStats.equipedWeapon].gameObject.transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Instantiate(projectilePrefab, gameObject.GetComponentsInChildren<Transform>()[1].position, Quaternion.AngleAxis(angle, Vector3.forward));
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
