using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SoundsController : MonoBehaviour,IPointerClickHandler {

    private Sprite voice;
    private Sprite mute;

    private void Start()
    {
        voice = Resources.Load("Img/voice", typeof(Sprite)) as Sprite;
        mute = Resources.Load("Img/mute", typeof(Sprite)) as Sprite;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        SqliteManager.Instance.Open();
        if (Parameter.IsVoice)
        {
            Parameter.IsVoice = false;
            gameObject.GetComponent<Image>().sprite = mute;
            SqliteManager.Instance.executeNonQuery("update game set voice = 0 where id=1");
        }
        else {
            Parameter.IsVoice = true;
            gameObject.GetComponent<Image>().sprite = voice;
            SqliteManager.Instance.executeNonQuery("update game set voice = 1 where id =1");
        }
        SqliteManager.Instance.Close();
    }

}
