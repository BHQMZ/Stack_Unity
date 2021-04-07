using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class RankController : MonoBehaviour,IPointerClickHandler {

    public void OnPointerClick(PointerEventData eventData)
    {
        transform.root.Find("MainInterface").gameObject.SetActive(false);
        transform.root.Find("RankInterface").gameObject.SetActive(true);
    }
}
