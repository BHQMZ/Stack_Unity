using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeleteDatabase : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData) { 
        transform.root.Find("Prompt").gameObject.SetActive(true);
    }
}
