using System.Collections;
using System.Collections.Generic;
using System;
using Mono.Data.Sqlite;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;

public class PlayGame : MonoBehaviour, IPointerClickHandler
{
    public GameObject Player;
    public GameObject Panel;
    public GameObject Cube;

    private void Start()
    {
        //如果运行在编辑器中
#if UNITY_EDITOR
        //通过路径找到第三方数据库
        SqliteManager.Instance.Open(Application.dataPath + "/Plugins/Android/assets/GameData.db");
        //如果运行在Android设备中
#elif UNITY_ANDROID
 
		//将第三方数据库拷贝至Android可找到的地方
		string appDBPath = Application.persistentDataPath + "/" + "GameData.db";
 
		//如果已知路径没有地方放数据库，那么我们从Unity中拷贝
		if(!File.Exists(appDBPath))
 		{
			//用www先从Unity中下载到数据库
		    WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/" + "GameData.db"); 
 
			//拷贝至规定的地方
		    File.WriteAllBytes(appDBPath, loadDB.bytes);
 
		}

        //在这里重新得到db对象。
        SqliteManager.Instance.Open(appDBPath);

#endif
        //创建表
        SqliteManager.Instance.executeNonQuery("create table if not exists Game(id integer primary key autoincrement,counts integer,voice integer)");
        SqliteManager.Instance.executeNonQuery("create table if not exists Style(id integer primary key autoincrement,name text,deblocking integer default 0)");
        SqliteManager.Instance.executeNonQuery("create table if not exists PitchStyle(id integer primary key,name text)");
        SqliteManager.Instance.executeNonQuery("create table if not exists RankingList(id integer primary key autoincrement,grade integer,time TimeStamp NOT NULL DEFAULT (datetime('now','localtime')))");
        //获取数据
        int count = SqliteManager.Instance.executeScalar("select count(*) from Game where id=1");
        if (count>0)
        {
            Parameter.Count = SqliteManager.Instance.executeScalar("select counts from Game where id=1");
        }
        else {
            SqliteManager.Instance.executeNonQuery("insert into Game(id,counts,voice) values(1,1000,1)");
            Parameter.Count = 1000;
        }

        //获取样式数据
        if (Parameter.StyleName == null)
        {
            if (SqliteManager.Instance.executeScalar("select count(*) from PitchStyle where id=1") > 0)
            {
                SqliteDataReader reader = SqliteManager.Instance.executeQuery("select name from PitchStyle where id=1");
                reader.Read();
                Parameter.StyleName = reader["name"].ToString();
                reader.Close();
            }
            else {
                SqliteManager.Instance.executeNonQuery("insert into PitchStyle(id,name) values(1,'Style2')");
                Parameter.StyleName = "Style2";
            }

        }

        //设置对应样式的图片
        if (Parameter.StyleName != "Style1" && Parameter.StyleName != "Style2")
        {
            Cube.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Style/" + Parameter.StyleName);
        }
        //随机设置一个以解锁样式
        if (Parameter.StyleName == "Style1")
        {
            SqliteDataReader reader1 = SqliteManager.Instance.executeQuery("select * from Style where deblocking = 1");
            int count1 = SqliteManager.Instance.executeScalar("select count(*) from Style where deblocking = 1");
            if (count1 > 0)
            {
                int i = UnityEngine.Random.Range(0, count1 + 1);
                if (i == count1)
                {
                    Cube.GetComponent<Renderer>().material.mainTexture = null;
                }
                else
                {
                    while (reader1.Read())
                    {
                        i--;
                        if (i == -1)
                        {
                            Cube.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Style/" + reader1["name"].ToString());
                        }
                    }
                }
            }
            else
            {
                Cube.GetComponent<Renderer>().material.mainTexture = null;
            }
            reader1.Close();
        }

        if (Parameter.StyleName == "Style2")
        {
            Cube.GetComponent<Renderer>().material.mainTexture = null;
        }



        //获取声音是否关闭，1是开启，0是关闭
        if (SqliteManager.Instance.executeScalar("select voice from Game where id=1") == 1)
        {
            Parameter.IsVoice = true;
            Panel.transform.Find("VoiceImage").GetComponent<Image>().sprite = Resources.Load("Img/voice", typeof(Sprite)) as Sprite;
        }
        else
        {
            Parameter.IsVoice = false;
            Panel.transform.Find("VoiceImage").GetComponent<Image>().sprite = Resources.Load("Img/mute", typeof(Sprite)) as Sprite;
        }
        //关闭数据库
        SqliteManager.Instance.Close();
        //初始化一些数据
        Parameter.IsCameraMobile = false;
        Parameter.IsPerfect = false;
        Parameter.Color = new Color(UnityEngine.Random.Range(1,50)*0.01f, UnityEngine.Random.Range(25, 75) * 0.01f, UnityEngine.Random.Range(1, 100) * 0.01f);
        Cube.GetComponent<Renderer>().material.color = Parameter.Color;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Player.SetActive(true);
        Panel.SetActive(false);
    }
}
