using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttackRange : MonoBehaviour {

    private bool shooting = true;

        void OnTriggerStay2D(Collider2D col)
        {
            if (col.gameObject.tag == "Player" && shooting)
            {
                transform.parent.GetComponent<EnemyRanged>().Attack(col);
                shooting = false;
            
            }
            else
                StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(2.0f);
        shooting = true;
        Debug.Log("Waited");
    }
    
}
