using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasteController : MonoBehaviour {

    private Transform cubeTransform;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < transform.childCount; i++)
        {
            cubeTransform = transform.GetChild(i);
            if (cubeTransform.position.y < transform.position.y) {
                GameObject.Destroy(cubeTransform.gameObject);
            }
        }
	}
}
