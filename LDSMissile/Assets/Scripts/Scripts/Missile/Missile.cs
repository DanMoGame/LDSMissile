using UnityEngine;
using System.Collections;
/// <summary>
/// 导弹控制脚本
/// </summary>
public class Missile : MonoBehaviour {

    //获取当前导弹
    private Transform m_Trans;
    //获取飞机物体
    private Transform m_PlayerTrans;
    //导弹默认的前方
    private Vector3 normalForward = Vector3.forward;
    //爆炸特效
    private GameObject m_Explode02;
    //商城数据对象
    private ShopData shopData;

    void Start () {
        shopData = new ShopData();
        m_Trans = gameObject.GetComponent<Transform>();
        m_PlayerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        m_Explode02 = Resources.Load<GameObject>("Explode02");
    }
	
	void Update () {

        switch (shopData.m_speed)
        {
            case "+2":
                m_Trans.Translate(Vector3.forward * 1.2f);
                break;
            case "+4":
                m_Trans.Translate(Vector3.forward * 2.3f);
                break;
            case "+8":
                m_Trans.Translate(Vector3.forward * 4.4f);
                break;
            case "+12":
                m_Trans.Translate(Vector3.forward * 6.3f);
                break;
            case "+18":
                m_Trans.Translate(Vector3.forward * 9.2f);
                break;
        }

        //导弹追踪角色--------------------------------------------.
        //计算方向（导弹与角色之间的方向）
        Vector3 dir = m_PlayerTrans.position - m_Trans.position;
        //插值计算当前导弹要旋转的角度
        normalForward = Vector3.Lerp(normalForward, dir, Time.deltaTime);
        //四元数,改变当前导弹的朝向
        m_Trans.rotation = Quaternion.LookRotation(normalForward);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Missile")
        {
            //播放特效
            GameObject.Instantiate(m_Explode02, m_Trans.position, Quaternion.identity);
            //导弹相撞，销毁
            GameObject.Destroy(gameObject);
        }
    }
}
