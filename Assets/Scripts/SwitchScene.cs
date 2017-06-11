using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SwitchScene : MonoBehaviour {

    float timer;
	// Use this for initialization
	void Start () {
        timer = 10f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            int index = SceneManager.GetActiveScene().buildIndex;
            index = (index + 1) % SceneManager.sceneCount;
            SceneManager.LoadScene(index);
        }
	}
}
