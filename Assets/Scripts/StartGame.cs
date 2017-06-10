using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManagement.loadScene("Level1");
        }
    }
}
