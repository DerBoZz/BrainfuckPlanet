using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPaste : MonoBehaviour {



    public void UpdateTarget()          //makes transform look at mousepos
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mousePos = mousePos - transform.position;
        mousePos.z = 0;
        transform.LookAt(mousePos, Vector3.up);
        //transform.rotation = Quaternion.Euler(mousePos.x, mousePos.y, 0);

    }



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
