using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using UnityEngine.UI;
using System;

public class RankRecord : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SqliteManager.Instance.Open();
        if (SqliteManager.Instance.executeScalar("select count(*) from RankingList") > 0) {
            transform.Find("Grade").Find("Text1").gameObject.GetComponent<Text>().text = SqliteManager.Instance.executeScalar("select sum(grade) from RankingList").ToString();
            transform.Find("number").Find("Text1").gameObject.GetComponent<Text>().text = SqliteManager.Instance.executeScalar("select count(*) from RankingList").ToString();
            SqliteDataReader reader = SqliteManager.Instance.executeQuery("select * from RankingList ORDER BY grade DESC limit 5");
            int i = 1;
            while (reader.Read()) {
                transform.root.Find("RankInterface").Find("Rank" + i).Find("Grade").gameObject.GetComponent<Text>().text = reader["grade"].ToString();
                transform.root.Find("RankInterface").Find("Rank" + i).Find("Time").gameObject.GetComponent<Text>().text = Convert.ToDateTime(reader["time"].ToString()).ToString("MM/dd");
                i++;
            }
            reader.Close();
        }
        SqliteManager.Instance.Close();

    }
}
