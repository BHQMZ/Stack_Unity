using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopReturn : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        transform.root.Find("MainInterface").gameObject.SetActive(true);
        transform.root.Find("PromptStore").gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            transform.root.Find("MainInterface").gameObject.SetActive(true);
            transform.root.Find("PromptStore").gameObject.SetActive(false);
        }
    }
}
