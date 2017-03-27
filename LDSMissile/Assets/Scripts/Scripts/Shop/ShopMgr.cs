using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 商城管理器
/// </summary>
public class ShopMgr : MonoBehaviour {

    //商城数据对象.
    private ShopData shopData;
    //xml路径.
    private string xmlPath = "Assets/Data/ShopData.xml";
    //private string xmlPath = @"C:\Users\LDS\AppData\LocalLow\DefaultCompany\LDSGuided Missile\ShopData.xml";
    //xml存档路径.
    private string savePath = "Assets/Data/SaveData.xml";
    //private string savePath = Application.streamingAssetsPath + "/SaveData.xml";
    //private string savePath = @"C:\Users\LDS\AppData\LocalLow\DefaultCompany\LDSGuided Missile\SaveData.xml";
    //商城模板
    private GameObject m_ShopItemPrefab;
    //商城左侧按钮
    private GameObject m_LeftChange;
    //商城右侧按钮
    private GameObject m_RightChange;
    //商城界面BG
    private GameObject m_ShopBG;
    //商品UI的集合
    private List<GameObject> m_ShopUI = new List<GameObject>();
    //商品UI索引
    private int index = 0;
    //界面UI
    private UILabel StartNum;
    private UILabel ScoreNum;
    //引用StartUIMgr
    private StartUIMgr m_StartUIMgr;

    void Start () {
        //实例化商城数据对象.
        shopData = new ShopData();
        m_StartUIMgr = GameObject.Find("UI Root").GetComponent<StartUIMgr>();
        m_ShopBG = GameObject.Find("ShopBG");
        //加载XML.
        shopData.ReadXmlByPath(xmlPath);
        shopData.ReadScoreAndGold(savePath);
        //加载ShopItem
        m_ShopItemPrefab = Resources.Load<GameObject>("UI/ShopItem");

        //按钮事件绑定
        m_LeftChange = GameObject.Find("LeftChange");
        m_RightChange = GameObject.Find("RightChange");
        UIEventListener.Get(m_LeftChange).onClick = LeftButtonClick;
        UIEventListener.Get(m_RightChange).onClick = RightButtonClick;

        //同步UI与XML数据
        StartNum = GameObject.Find("Star/StarNum").GetComponent<UILabel>();
        ScoreNum = GameObject.Find("Score/ScoreNum").GetComponent<UILabel>();
        UpdateUI();

        // 创建商城模块中所有的商品
        CreateAllShopUI();

        SetPlayerInfo(shopData.shopList[0].Model);

    }

    /// <summary>
    /// 更新UI数据显示
    /// </summary>
    private void UpdateUI()
    {
        StartNum.text = shopData.goldCount.ToString();
        ScoreNum.text = shopData.heightScore.ToString();
    }

    /// <summary>
    /// 创建商城模块中所有的商品
    /// </summary>
    private void CreateAllShopUI()
    {
        for (int i = 0; i < shopData.shopList.Count; i++)
        {
            GameObject item = NGUITools.AddChild(m_ShopBG, m_ShopItemPrefab);
            //加载对应的飞机模型
            GameObject ship = Resources.Load<GameObject>(shopData.shopList[i].Model);
            item.GetComponent<ShopItemUI>().SetUIValue(shopData.shopList[i].Id, shopData.shopList[i].Speed, shopData.shopList[i].Rotate, shopData.shopList[i].Price,ship, shopData.shopState[i]);
            //将生成的UI添加进集合中，进行管理
            m_ShopUI.Add(item);
        }
        //隐藏商城UI
        ShopUIHideOrShow(index);
    }

    /// <summary>
    /// 左侧按钮点击事件
    /// </summary>
    private void LeftButtonClick(GameObject go)
    {
        if (index > 0)
        {
            index--;
            int temp = shopData.shopState[index];
            m_StartUIMgr.SetPlayButtonState(temp);
            SetPlayerInfo(shopData.shopList[index].Model);
            ShopUIHideOrShow(index);
        }
    }

    /// <summary>
    /// 右侧按钮点击事件
    /// </summary>
    /// <param name="go"></param>
    private void RightButtonClick(GameObject go)
    {
        if (index < m_ShopUI.Count - 1)
        {
            index++;
            int temp = shopData.shopState[index];
            m_StartUIMgr.SetPlayButtonState(temp);
            SetPlayerInfo(shopData.shopList[index].Model);
            ShopUIHideOrShow(index);
        }
    }

    /// <summary>
    /// 商城UI的显示与隐藏
    /// </summary>
    private void ShopUIHideOrShow(int index)
    {
        for (int i = 0; i < m_ShopUI.Count; i++)
        {
            m_ShopUI[i].SetActive(false);
        }
        m_ShopUI[index].SetActive(true);
    }

    /// <summary>
    /// 计算商品价格
    /// </summary>
    private void CalcItemPrice(ShopItemUI itemUI)
    {
        if (shopData.goldCount >= itemUI.m_ItemPrice)
        {
            //购买成功！隐藏购买按钮,金币数量较少，存档更改
            itemUI.BuyEnd();//隐藏购买按钮
            shopData.goldCount -= itemUI.m_ItemPrice;//减去已经消耗的金币
            UpdateUI();//更新UI
            shopData.UpdateXMLData(savePath, "GoldCount", shopData.goldCount.ToString());
            shopData.UpdateXMLData(savePath, "ID" + itemUI.m_Itemid, "1");//更新XML商品状态
            m_StartUIMgr.SetPlayButtonState(1);
        }
        else
        {
            //购买失败！
        }
    }

    /// <summary>
    /// 存储当前角色信息到PlayerPrefs中
    /// </summary>
    private void SetPlayerInfo(string name)
    {
        PlayerPrefs.SetString("PlayerName", name);
    }
}
