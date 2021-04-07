using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using UnityEngine.UI;

public class StyleController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.Find(Parameter.StyleName).Find("Select").gameObject.SetActive(true);
        SqliteManager.Instance.Open();
        int count = SqliteManager.Instance.executeScalar("select count(*) from Style");
        if (count < transform.childCount - 2)
        {
            for (int i = count + 2; i < transform.childCount; i++)
            {
                SqliteManager.Instance.executeNonQuery(string.Format("insert into Style(name) values('{0}')", transform.GetChild(i).name));
            }
        }
        else {
            SqliteDataReader reader = SqliteManager.Instance.executeQuery("select * from Style");
            while (reader.Read()) {
                if (reader["deblocking"].ToString() != "0") {
                    transform.Find(reader["name"].ToString()).Find("Image").gameObject.GetComponent<Image>().sprite = Resources.Load("ShopImg/" + reader["name"].ToString(), typeof(Sprite)) as Sprite;
                }
            }
        }
        SqliteManager.Instance.Close();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
