using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttackRange : MonoBehaviour {

	void OnTrigger2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Vector2 directionPlayer;
            if(col.gameObject.transform.position.x > transform.position.x)
            {
                //rightside
                directionPlayer = new Vector2(1, 0);
            }
            else
            {
                //leftside
                directionPlayer = new Vector2(-1, 0);
            }
            transform.parent.GetComponent<EnemyMelee>().Attack(directionPlayer);
        }
    }
}
