using UnityEngine;
using System.Collections;
/// <summary>
/// 飞机控制器
/// </summary>
public class Ship : MonoBehaviour {

    //当前飞机
    private Transform m_Transform;
    //左转控制器
    private bool isLeft = false;
    //右转控制器
    private bool isRight = false;
    //是否死亡
    private bool isDeath = false;
    //导弹生成器脚本
    private MissileMgr m_MissileMgr;
    //奖励生成管理器
    private RewardMgr m_RewardMgr;
    //商城数据对象
    private ShopData shopData;
    //爆炸特效
    private GameObject m_Explode02;
    //本局获得奖励物品个数
    private int rewardNum = 0;
    //游戏界面UI管理器
    private GameUIMgr m_GameUIMgr;

    public bool IsLeft
    {
        get { return isLeft; }
        set { isLeft = value; }
    }

    public bool IsRight
    {
        get { return isRight; }
        set { isRight = value; }
    }

    void Start () {
        shopData = new ShopData();
        m_Transform = gameObject.GetComponent<Transform>();
        m_MissileMgr = GameObject.Find("MissileMgr").GetComponent<MissileMgr>();
        m_RewardMgr = GameObject.Find("RewardMgr").GetComponent<RewardMgr>();
        m_Explode02 = Resources.Load<GameObject>("Explode02");
        m_GameUIMgr = GameObject.Find("UI Root").GetComponent<GameUIMgr>();
    }
	
	void Update () {
        if (isDeath == false)
        {
            switch (shopData.m_speed)
            {
                case "+2":
                    m_Transform.Translate(Vector3.forward * 1);
                    break;
                case "+4":
                    m_Transform.Translate(Vector3.forward * 2);
                    break;
                case "+8":
                    m_Transform.Translate(Vector3.forward * 4);
                    break;
                case "+12":
                    m_Transform.Translate(Vector3.forward * 6);
                    break;
                case "+18":
                    m_Transform.Translate(Vector3.forward * 9);
                    break;
            }
            if (isLeft)
            {
                switch (shopData.m_rotate)
                {
                    case "+3":
                        m_Transform.Rotate(Vector3.up * -1);
                        break;
                    case "+6":
                        m_Transform.Rotate(Vector3.up * -2);
                        break;
                    case "+9":
                        m_Transform.Rotate(Vector3.up * -3);
                        break;
                    case "+15":
                        m_Transform.Rotate(Vector3.up * -5);
                        break;
                    case "+24":
                        m_Transform.Rotate(Vector3.up * -8);
                        break;
                }
            }
            if (isRight)
            {
                switch (shopData.m_rotate)
                {
                    case "+3":
                        m_Transform.Rotate(Vector3.up * 1);
                        break;
                    case "+6":
                        m_Transform.Rotate(Vector3.up * 2);
                        break;
                    case "+9":
                        m_Transform.Rotate(Vector3.up * 3);
                        break;
                    case "+15":
                        m_Transform.Rotate(Vector3.up * 5);
                        break;
                    case "+24":
                        m_Transform.Rotate(Vector3.up * 8);
                        break;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //与四边相撞
        if (other.tag == "Border")
        {
            isDeath = true;
            //播放特效
            GameObject.Instantiate(m_Explode02, m_Transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            m_MissileMgr.StopCreate();
            m_RewardMgr.StopCreate();
            m_GameUIMgr.ShowOverPanel();
        }
        if (other.tag == "Missile")
        {
            isDeath = true;//主角死亡
            //播放特效
            GameObject.Instantiate(m_Explode02, m_Transform.position, Quaternion.identity);
            GameObject.Destroy(other.gameObject);//销毁导弹
            gameObject.SetActive(false);
            m_MissileMgr.StopCreate();
            m_RewardMgr.StopCreate();
            m_GameUIMgr.ShowOverPanel();
        }
        if (other.tag == "Reward")
        {
            rewardNum++;
            m_GameUIMgr.UpdateScoreLabel(rewardNum * 15);
            GameObject.Destroy(other.gameObject);//销毁奖励物品
            m_RewardMgr.RewardCountDown();
        }
    }
}
