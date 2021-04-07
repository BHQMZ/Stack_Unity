using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RankConfirm : MonoBehaviour,IPointerClickHandler {

    public void OnPointerClick(PointerEventData eventData)
    {
        SqliteManager.Instance.Open();
        SqliteManager.Instance.executeNonQuery("delete from RankingList");
        SqliteManager.Instance.Close();
        transform.root.Find("RankInterface").Find("Grade").Find("Text1").gameObject.GetComponent<Text>().text = "0";
        transform.root.Find("RankInterface").Find("number").Find("Text1").gameObject.GetComponent<Text>().text = "0";
        transform.root.Find("RankInterface").Find("Rank1").Find("Grade").gameObject.GetComponent<Text>().text = "----";
        transform.root.Find("RankInterface").Find("Rank1").Find("Time").gameObject.GetComponent<Text>().text = "----";
        transform.root.Find("RankInterface").Find("Rank2").Find("Grade").gameObject.GetComponent<Text>().text = "----";
        transform.root.Find("RankInterface").Find("Rank2").Find("Time").gameObject.GetComponent<Text>().text = "----";
        transform.root.Find("RankInterface").Find("Rank3").Find("Grade").gameObject.GetComponent<Text>().text = "----";
        transform.root.Find("RankInterface").Find("Rank3").Find("Time").gameObject.GetComponent<Text>().text = "----";
        transform.root.Find("RankInterface").Find("Rank4").Find("Grade").gameObject.GetComponent<Text>().text = "----";
        transform.root.Find("RankInterface").Find("Rank4").Find("Time").gameObject.GetComponent<Text>().text = "----";
        transform.root.Find("RankInterface").Find("Rank5").Find("Grade").gameObject.GetComponent<Text>().text = "----";
        transform.root.Find("RankInterface").Find("Rank5").Find("Time").gameObject.GetComponent<Text>().text = "----";
        transform.parent.parent.gameObject.SetActive(false);
    }
    
}
