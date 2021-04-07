using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopEntrance : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        transform.root.Find("MainInterface").gameObject.SetActive(false);
        transform.root.Find("PromptStore").gameObject.SetActive(true);
    }
}
