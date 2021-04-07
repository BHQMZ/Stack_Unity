using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfectSprite : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
	}

    private void FixedUpdate()
    {
        if (GetComponent<SpriteRenderer>().color.a > 0)
        {
            float a = GetComponent<SpriteRenderer>().color.a;
            a -= 0.02f;
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, a);
        }
    }
}
