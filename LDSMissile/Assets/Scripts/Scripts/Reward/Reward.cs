using UnityEngine;
using System.Collections;
/// <summary>
/// 奖励物品控制
/// </summary>
public class Reward : MonoBehaviour {

    private Transform m_Trans;

    void Start () {
        m_Trans = gameObject.GetComponent<Transform>();
    }
	
	
	void Update () {
        m_Trans.Rotate(Vector3.left * 2);
	}
}
