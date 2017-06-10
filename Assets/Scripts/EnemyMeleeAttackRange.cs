using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttackRange : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            transform.parent.GetComponent<EnemyMelee>().Attack();
        }
    }
}
