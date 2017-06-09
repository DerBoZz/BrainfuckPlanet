using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPaste : MonoBehaviour {

    public float speed = 1;
    float age = 0;
    public float maxAge = 1;

    // Update is called once per frame
    void Update()
    {
        age += Time.deltaTime;
        transform.Translate(new Vector3(10, 0, 0) * Time.deltaTime * speed, Space.Self);       //speed of projectile
        //Debug.Log(age);
        if (age > maxAge)            //age check
        {
            Destroy(gameObject);

        }
    }

}
