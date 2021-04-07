using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Resume : MonoBehaviour,IPointerClickHandler {

    public void OnPointerClick(PointerEventData eventData)
    {
        SqliteManager.Instance.Open();
        //游戏结束，记录分数和时间
        SqliteManager.Instance.executeNonQuery(string.Format("insert into RankingList(grade) values({0})", Parameter.Grade));
        //更新数据库积分
        SqliteManager.Instance.executeNonQuery(string.Format("update game set counts = {0} where id=1", Parameter.Count));
        SqliteManager.Instance.Close();

        Parameter.IsGameOver = false;
        SceneManager.LoadScene(0);
        Parameter.Grade = 0;
    }
}
