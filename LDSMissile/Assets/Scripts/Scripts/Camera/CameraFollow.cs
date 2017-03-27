using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    private Transform m_Trans;

    private Transform m_Player;
	
	void Start () {
        m_Trans = gameObject.GetComponent<Transform>();
        m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
	
	
	void Update () {
        m_Trans.position = m_Player.transform.position + new Vector3(0,50,0);
	}
}
