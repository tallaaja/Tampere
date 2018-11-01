using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void letsLoadScene(int number)
    {
        SceneManager.LoadScene(number);
    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
