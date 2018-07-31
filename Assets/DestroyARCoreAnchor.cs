using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyARCoreAnchor : MonoBehaviour {

    public Button ExitButton;

	// Use this for initialization
	void Start () {
        ExitButton.onClick.AddListener(DestroyARCore);
	}
	
    void DestroyARCore()
    {

    }
	// Update is called once per frame
	void Update () {
		
	}
}
