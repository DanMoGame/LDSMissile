using UnityEngine;
using System.Collections;
/// <summary>
/// 商城Item控制器
/// </summary>
public class ShopItemUI : MonoBehaviour {

    //ShopItem
    private Transform m_Trans;
    //飞机的速度
    private UILabel m_Speed;
    //飞机的旋转
    private UILabel m_Rotate;
    //飞机的价格
    private UILabel m_Price;
    //飞机的父物体
    private GameObject m_ShipParent;
    //购买按钮
    private GameObject m_BtnBuy;
    //商品价格
    public int m_ItemPrice;
    //商品ID编号
    public int m_Itemid;


    void Awake () {
        m_Trans = gameObject.GetComponent<Transform>();
        m_Speed = m_Trans.FindChild("Speed/SpeedNum").GetComponent<UILabel>();
        m_Rotate = m_Trans.FindChild("Rotate/RotateNum").GetComponent<UILabel>();
        m_Price = m_Trans.FindChild("BtnBuy/Buy").GetComponent<UILabel>();
        m_ShipParent = m_Trans.FindChild("Model").gameObject;
        m_BtnBuy = m_Trans.FindChild("BtnBuy").gameObject;

        UIEventListener.Get(m_BtnBuy).onClick = BuyButtonClick;
    }

    /// <summary>
    /// 给物品赋值
    /// </summary>
    public void SetUIValue(string id,string speed,string rotate,string price,GameObject model,int state)
    {
        m_Speed.text = speed;
        m_Rotate.text = rotate;
        m_Price.text = price;

        m_ItemPrice = int.Parse(price);
        m_Itemid = int.Parse(id);

        //判断状态
        if (state == 1)
        {
            m_BtnBuy.SetActive(false);
        }

        //实例化飞机，设置参数
        GameObject ship = NGUITools.AddChild(m_ShipParent, model);
        ship.layer = 20;
    }

    /// <summary>
    /// 购买按钮点击事件
    /// </summary>
    /// <param name="go"></param>
    private void BuyButtonClick(GameObject go)
    {
        SendMessageUpwards("CalcItemPrice", this);
    }

    /// <summary>
    /// 购买成功
    /// </summary>
    public void BuyEnd()
    {
        m_BtnBuy.SetActive(false);
    }
}
