using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SwitchScene : MonoBehaviour {
    public string sceneName;
    public float timer;
	// Use this for initialization
	void Start () {
        timer = 4f;
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
                
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            if (sceneName == "Startscreen")
            {
                PlayerStats.reset();
            }
            //int index = SceneManager.GetActiveScene().buildIndex;
            //index = (index + 1) % SceneManager.sceneCount;
            //SceneManager.LoadScene(index);
        }
	}
}
