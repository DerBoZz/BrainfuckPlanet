using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttackRange : MonoBehaviour {

        void OnTriggerStay2D(Collider2D col)
        {
            if (col.gameObject.tag == "Player")
            {
                transform.parent.GetComponent<EnemyRanged>().Attack(col);
            }
        }
    
}
