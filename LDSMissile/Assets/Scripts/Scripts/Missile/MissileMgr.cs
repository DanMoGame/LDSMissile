using UnityEngine;
using System.Collections;
/// <summary>
/// 导弹生成管理器
/// </summary>
public class MissileMgr : MonoBehaviour {

    //获取当前管理器
    private Transform m_Trans;
    //导弹生成点
    private Transform[] createPoints;
    //导弹预制体
    private GameObject m_MissPrefab;
    //导弹预制体
    private string[] m_MissPrefabs = { "Missile_3", "Missile_2", "Missile_1" };

    void Start() {
        m_Trans = gameObject.GetComponent<Transform>();
        createPoints = GameObject.Find("CreatePoints").GetComponent<Transform>().GetComponentsInChildren<Transform>();
        //m_MissPrefab = Resources.Load<GameObject>("Missile_3");

        //调用生成器
        InvokeRepeating("CreateMissile", 3.0f, 5.0f);
    }

    /// <summary>
    /// 生成导弹
    /// </summary>
    private void CreateMissile()
    {
        int index = Random.Range(0, createPoints.Length);
        int indexPre = Random.Range(0, 3);
        m_MissPrefab = Resources.Load<GameObject>(m_MissPrefabs[indexPre]);
        GameObject.Instantiate(m_MissPrefab, createPoints[index].position, Quaternion.identity, m_Trans);
    }

    //停止导弹生成
    public void StopCreate()
    {
        CancelInvoke();
    }
}
