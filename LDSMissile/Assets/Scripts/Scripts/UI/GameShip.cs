using UnityEngine;
using System.Collections;
/// <summary>
/// 开始UI界面飞机控制器
/// </summary>
public class GameShip : MonoBehaviour {

    private Transform m_Trans;
    private GameObject[] m_MeshTrans;

	void Start () {
        m_Trans = gameObject.GetComponent<Transform>();
        m_MeshTrans = GameObject.FindGameObjectsWithTag("Mesh");
    }
	

	void Update () {
        for (int i = 0; i < m_MeshTrans.Length; i++)
        {
            m_MeshTrans[i].transform.Rotate(new Vector3(0, 0, Time.deltaTime * 100));
        }
    }
}
