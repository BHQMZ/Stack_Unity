using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StoreConfirm : MonoBehaviour, IPointerClickHandler
{
    private string stylename;

    private void Start()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        transform.root.Find("PromptStore").Find("Scroll View").Find("Viewport").Find("Content").Find(Parameter.BuyStyleName).Find("Image").GetComponent<Image>().sprite = Resources.Load("ShopImg/" + Parameter.BuyStyleName, typeof(Sprite)) as Sprite;
        Parameter.Count -= 200;
        SqliteManager.Instance.Open();
        SqliteManager.Instance.executeNonQuery(string.Format("update Style set deblocking = 1 where name ='{0}'", Parameter.BuyStyleName));
        SqliteManager.Instance.executeNonQuery(string.Format("update game set counts = {0} where id=1", Parameter.Count));
        SqliteManager.Instance.Close();
        transform.root.Find("PromptStore").Find("Prompt").gameObject.SetActive(false);
    }
}
