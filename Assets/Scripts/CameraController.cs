using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour {


    private float Y;
    private bool isMagnify;
    public GameObject Player;
    public GameObject Resume;


    // Use this for initialization
    void Start () {

        Y = (transform.position - Player.transform.position).y-0.5f;
        isMagnify = true;
    }
	

    private void LateUpdate()
    {
        if (Parameter.IsCameraMobile && !Parameter.IsGameOver)
        {
            transform.position=Vector3.Lerp(transform.position,new Vector3(transform.position.x, Player.transform.position.y + Y, transform.position.z), 0.2f);
        }
    }

    private void FixedUpdate()
    {
        if (Parameter.IsGameOver)
        {
            //游戏结束，如果分数大于10，就拉远主摄像机的视野
            if (Parameter.Grade > 10 && isMagnify)
            {
                GetComponent<Camera>().orthographicSize += Parameter.Grade * 0.01f;
                if (GetComponent<Camera>().orthographicSize >= Parameter.Grade / 2.5f)
                {
                    isMagnify = false;
                }
            }
            else
            {
                isMagnify = false;
            }
            if (!isMagnify)
            {
                Resume.SetActive(true);
            }
        }
    }
}
