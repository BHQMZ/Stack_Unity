using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReturnController : MonoBehaviour,IPointerClickHandler {

    public void OnPointerClick(PointerEventData eventData)
    {
        transform.root.Find("MainInterface").gameObject.SetActive(true);
        transform.root.Find("RankInterface").gameObject.SetActive(false);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            transform.root.Find("MainInterface").gameObject.SetActive(true);
            transform.root.Find("RankInterface").gameObject.SetActive(false);
        }
    }
}
