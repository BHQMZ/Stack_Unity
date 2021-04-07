using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Mono.Data.Sqlite;

public class StyleClick : MonoBehaviour, IPointerClickHandler
{
    private Sprite sprite;
    public GameObject Cubes;

    private void Start()
    {
        sprite = Resources.Load("ShopImg/NotUnlock", typeof(Sprite)) as Sprite;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (transform.Find("Image").GetComponent<Image>().sprite != sprite && Parameter.StyleName != name)
        {
            transform.parent.Find(Parameter.StyleName).Find("Select").gameObject.SetActive(false);
            Parameter.StyleName = name;
            SqliteManager.Instance.Open();
            SqliteManager.Instance.executeNonQuery(string.Format("update PitchStyle set name = '{0}' where id=1",name));
            transform.Find("Select").gameObject.SetActive(true);
            if (name == "Style1")
            {
                SqliteDataReader reader = SqliteManager.Instance.executeQuery("select * from Style where deblocking = 1");
                int count = SqliteManager.Instance.executeScalar("select count(*) from Style where deblocking = 1");
                if (count > 0) {
                    int i = Random.Range(0, count+1);
                    if (i == count)
                    {
                        Cubes.GetComponent<Renderer>().material.mainTexture = null;
                    }
                    else {
                        while (reader.Read()) {
                            i--;
                            if (i == -1) {
                                Cubes.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Style/" + reader["name"].ToString());
                            }
                        }
                    }
                }
                reader.Close();
            }
            else if (name == "Style2") {
                Cubes.GetComponent<Renderer>().material.mainTexture = null;
            }
            else {
                Cubes.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Style/" + Parameter.StyleName);
            }
            SqliteManager.Instance.Close();
        }
        else {
            if (Parameter.Count >= 200) {
                transform.root.Find("PromptStore").Find("Prompt").gameObject.SetActive(true);
                Parameter.BuyStyleName = name;
            }
        }

    }
}
