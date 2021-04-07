using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    //方块是否到达边界
    private bool isBorder;
    //边界的范围
    private float border;
    //移动速度
    private float speed;
    //下方堆叠的方块
    private GameObject cube;
    //切割后多余的方块
    private GameObject dropCube;
    //移动的正方向
    private Vector3 direction;
    //方块是否完美契合下方方块
    private bool isPerfect;
    //方块完美契合次数
    private int perfect;
    //方块与下方方块位置的差值在一定范围内视为完美
    private float deviation;
    //颜色数组
    private float[] rgb;
    //颜色加减
    private int rzf;
    private int gzf;
    private int bzf;
    //声音
    private AudioSource[] Sounds;

    //一开始的底座方块
    public GameObject Cubes;
    //多余方块的父物体
    public GameObject Waste;
    //分数显示的地方
    public GameObject GradeText;
    //主摄像机
    public GameObject MainCamera;
    //完美重合的特效
    public GameObject PerfectSprite;



    void Start() {
        Parameter.IsGameOver = false;
        isBorder = false;
        border = 4;
        speed = 5;
        direction = Vector3.right;
        Parameter.IsCameraMobile = false;
        isPerfect = false;
        perfect = 0;
        deviation = 0.2f;
        rgb = new float[]{Parameter.Color.r*255, Parameter.Color.g*255, Parameter.Color.b*255};
        rzf = 1;
        gzf = 1;
        bzf = 1;
        Parameter.Grade = 0;
        Sounds = MainCamera.GetComponents<AudioSource>();
        GetComponent<Renderer>().material.color = Cubes.GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.mainTexture = Cubes.GetComponent<Renderer>().material.mainTexture;
        transform.localScale = Cubes.transform.localScale;
    }
	
	// Update is called once per frame
	void Update () {
        //判断游戏是否结束
        if (!Parameter.IsGameOver)
        {
            //点击
            onClick();
            //判断游戏是否结束，没有就执行移动
            if (!Parameter.IsGameOver)
            {
                //确定移动方向
                mobile();
                //改变位置，移动
                transform.position += direction * Time.deltaTime * speed;
            }
        }
    }


    //切割（调整物体大小和位置）
    private void shrink() {
        //获取当前物体与下面方块位置的差值
        Vector3 shrink;
        //判断Cubes是否有子物体，用于确定下方物体
        if (Cubes.transform.childCount==0)
        {
            shrink = (transform.position - Cubes.transform.position);
        }
        else {
            shrink = (transform.position - cube.transform.position);
        }

        //判断方向
        if (direction == Vector3.right)
        {
            //判断是否完美
            if (Mathf.Abs(shrink.x) > deviation)
            {
                isPerfect = false;
                perfect = 0;
                //判断是否失败
                if (Mathf.Abs(shrink.x) > transform.localScale.x)
                {
                    //游戏结束
                    Parameter.IsGameOver = true;
                    gameObject.GetComponent<Rigidbody>().useGravity = true;
                }
                else
                {
                    //方块变形，并移动到正确的位置
                    transform.localScale -= new Vector3(Mathf.Abs(shrink.x), 0, 0);
                    transform.position -= new Vector3(shrink.x * 0.5f, 0, 0);

                    //创建多余的方块物体
                    dropCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    dropCube.transform.SetParent(Waste.transform);
                    //调整大小
                    dropCube.transform.localScale = new Vector3(Mathf.Abs(shrink.x), transform.localScale.y, transform.localScale.z);
                    //判断在什么位置
                    if (shrink.x < 0)
                    {
                        dropCube.transform.position = transform.position + new Vector3((shrink.x - transform.localScale.x) * 0.5f, 0, 0);
                    }
                    else
                    {
                        dropCube.transform.position = transform.position + new Vector3((shrink.x + transform.localScale.x) * 0.5f, 0, 0);
                    }
                    //添加刚体使其下落
                    dropCube.AddComponent<Rigidbody>();
                    //改变颜色
                    dropCube.GetComponent<Renderer>().material.color = gameObject.GetComponent<Renderer>().material.color;
                    dropCube.GetComponent<Renderer>().material.mainTexture = gameObject.GetComponent<Renderer>().material.mainTexture;
                }
            }
            else {
                perfect++;
                isPerfect = true;
            }
        }
        else {

            if (Mathf.Abs(shrink.z) > deviation)
            {
                isPerfect = false;
                perfect = 0;
                if (Mathf.Abs(shrink.z) > transform.localScale.z)
                {

                    Parameter.IsGameOver = true;
                    gameObject.GetComponent<Rigidbody>().useGravity = true;
                }
                else
                {

                    transform.localScale -= new Vector3(0, 0, Mathf.Abs(shrink.z));
                    transform.position -= new Vector3(0, 0, shrink.z * 0.5f);

                    dropCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    dropCube.transform.SetParent(Waste.transform);
                    dropCube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, Mathf.Abs(shrink.z));

                    if (shrink.z < 0)
                    {
                        dropCube.transform.position = transform.position + new Vector3(0, 0, (shrink.z - transform.localScale.z) * 0.5f);
                    }
                    else
                    {
                        dropCube.transform.position = transform.position + new Vector3(0, 0, (shrink.z + transform.localScale.z) * 0.5f);
                    }
                    
                    dropCube.AddComponent<Rigidbody>();
                    dropCube.GetComponent<Renderer>().material.color = gameObject.GetComponent<Renderer>().material.color;
                    dropCube.GetComponent<Renderer>().material.mainTexture = gameObject.GetComponent<Renderer>().material.mainTexture;
                }
            }
            else {
                perfect++;
                isPerfect = true;
            }
        }

        //播放对应的音乐
        playVoice();
    }

    //点击
    private void onClick() {
        //是否被点击
        if (Input.GetMouseButtonDown(0))
        {
            shrink();
            //游戏是否结束
            if (!Parameter.IsGameOver)
            {
                //判断是否完美重合，重合就使其移动到下方物体的正上方
                if (isPerfect)
                {
                    if (cube != null)
                    {
                        transform.position = cube.transform.position + new Vector3(0, 0.5f, 0);
                    }
                    else
                    {
                        transform.position = Cubes.transform.position + new Vector3(0, 0.5f, 0);
                    }

                    //完美数等于7，方块变大
                    if (perfect == 7)
                    {
                        //大小超过3不能变大
                        if (direction == Vector3.forward && transform.localScale.z < 3f || direction == Vector3.right && transform.localScale.x < 3f) {
                            transform.localScale += direction * 0.2f;
                            transform.position += direction * 0.1f;
                        }
                        perfect = 0;
                    }

                    PerfectSprite.transform.position = transform.position - Vector3.up * 0.25f;
                    PerfectSprite.transform.localScale = new Vector3(transform.localScale.x+0.3f, transform.localScale.z+0.3f,1);
                    PerfectSprite.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                    isPerfect = false;
                }

                //创建下方方块
                cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = transform.position;
                cube.transform.localScale = transform.localScale;
                cube.transform.SetParent(Cubes.transform);
                cube.GetComponent<Renderer>().material.color = gameObject.GetComponent<Renderer>().material.color;
                cube.GetComponent<Renderer>().material.mainTexture = gameObject.GetComponent<Renderer>().material.mainTexture;



                //改变物体移动的方向
                if (direction == Vector3.right)
                {
                    direction = Vector3.forward;
                }
                else if (direction == Vector3.forward)
                {
                    direction = Vector3.right;
                }
                //速度变成正数
                speed = Mathf.Abs(speed);
                //位置移动到边界
                isBorder = true;
                transform.position += new Vector3(0, 0.5f, 0);
                transform.position += direction * -border;
                //改变颜色
                if (rgb[0] < 0 || rgb[0] > 255) {
                    rzf = -rzf;
                }
                if (rgb[1] < 0 || rgb[1] > 255)
                {
                    gzf = -gzf;
                }
                if (rgb[2] < 0 || rgb[2] > 255)
                {
                    bzf = -bzf;
                }
                rgb[0] += 20 * rzf;
                rgb[1] += 5 * gzf;
                rgb[2] += 10 * bzf;


                gameObject.GetComponent<Renderer>().material.color = new Color(rgb[0] / 255, rgb[1] / 255, rgb[2] / 255);
                //判断是否需要移主动摄像机
                if (transform.position.y > 1.75f)
                {
                    Parameter.IsCameraMobile = true;
                }
                //分数增加
                Parameter.Grade++;
                //判断积分是否需要增加
                if (Parameter.Grade % 10 == 0 && Parameter.Grade != 0)
                {
                    Parameter.Count++;
                }
            }
            //分数每次增加30，速度增加
            if (Parameter.Grade % 30 == 0 && Parameter.Grade != 0&& speed<=7) {
                speed+=0.5f;
            }
            //改变UI上的分数
            GradeText.GetComponent<Text>().text = Parameter.Grade.ToString();
        }
    }

    //移动的方向
    private void mobile() {
        //判断朝向
        if (direction == Vector3.right) {
            //判断是否到达边界，是就改变速度方向
            if (transform.position.x > border && isBorder == false)
            {
                isBorder = true;
                speed = -speed;
            }
            if (transform.position.x < -border && isBorder == false)
            {
                isBorder = true;
                speed = -speed;
            }
            if (transform.position.x <= border && transform.position.x >= -border)
            {
                isBorder = false;
            }
        }
        if (direction == Vector3.forward) {
            if (transform.position.z > border && isBorder == false)
            {
                isBorder = true;
                speed = -speed;
            }
            if (transform.position.z < -border && isBorder == false)
            {
                isBorder = true;
                speed = -speed;
            }
            if (transform.position.z <= border && transform.position.z >= -border)
            {
                isBorder = false;
            }
        }
    }

    //播放声音
    private void playVoice() {
        if (Parameter.IsVoice)
        {
            if (isPerfect)
            {
                switch (perfect)
                {
                    case 1:
                        Sounds[1].Play();
                        break;
                    case 2:
                        Sounds[2].Play();
                        break;
                    case 3:
                        Sounds[3].Play();
                        break;
                    case 4:
                        Sounds[4].Play();
                        break;
                    case 5:
                        Sounds[5].Play();
                        break;
                    case 6:
                        Sounds[6].Play();
                        break;
                    default:
                        Sounds[7].Play();
                        break;
                }
            }
            else
            {
                Sounds[0].Play();
            }
        }
    }
}
