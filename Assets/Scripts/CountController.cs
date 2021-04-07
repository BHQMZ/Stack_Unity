using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountController : MonoBehaviour {
    private Text text;
    private int count;
    private float time;
    private float runtime;
    public GameObject Add;
	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        count = Parameter.Count;
        time = 1f;
        runtime = time;

    }

    private void LateUpdate()
    {
        //更新积分
        text.text = Parameter.Count.ToString();
    }


    private void FixedUpdate()
    {
        if (int.Parse(text.text) > count) {
            Add.SetActive(true);
            if (runtime > 0)
            {
                runtime -= (1f/60);
            }
            else {
                runtime = time;
                count = int.Parse(text.text);
                Add.SetActive(false);
            }
        }
    }
}
