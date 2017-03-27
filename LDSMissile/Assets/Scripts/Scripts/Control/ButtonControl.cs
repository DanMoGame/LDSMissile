using UnityEngine;
using System.Collections;

public class ButtonControl : MonoBehaviour {

    //左键
    private GameObject m_Left;
    //右键
    private GameObject m_Right;
    //飞机
    private Ship m_ShipTrans;

	void Start () {
        m_Left = GameObject.Find("Left");
        m_Right = GameObject.Find("Right");
        m_ShipTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Ship>();

        UIEventListener.Get(m_Left).onPress = LeftPress;
        UIEventListener.Get(m_Right).onPress = RightPress;
	}

    /// <summary>
    /// 左键按下事件
    /// </summary>
    /// <param name="go">按下的物体</param>
    /// <param name="isPress">是否按下</param>
    private void LeftPress(GameObject go, bool isPress)
    {
        if (isPress)
        {
            m_ShipTrans.IsLeft = true;
        }
        else
        {
            m_ShipTrans.IsLeft = false;
        }
    }

    /// <summary>
    /// 右键按下事件
    /// </summary>
    /// <param name="go">按下的物体</param>
    /// <param name="isPress">是否按下</param>
    private void RightPress(GameObject go, bool isPress)
    {
        if (isPress)
        {
            m_ShipTrans.IsRight = true;
        }
        else
        {
            m_ShipTrans.IsRight = false;
        }
    }
}
