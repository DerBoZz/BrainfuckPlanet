﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loadlevel2Trigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManagement.loadScene("Level2");
        }
    }
}
