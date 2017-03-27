using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

/// <summary>
/// 商城功能模块数据操作.
/// </summary>
public class ShopData {

    //存储商城数据集合
    public List<ShopItem> shopList = new List<ShopItem>();
    //商品的购买状态集合
    public List<int> shopState = new List<int>();
    //金币数
    public int goldCount = 0;
    //最高分数
    public int heightScore = 0;
    //飞机移动速度
    public string m_speed = "+2";
    //飞机旋转速度
    public string m_rotate = "+3";

    /// <summary>
    /// 通过指定的路径读取XML文档.
    /// </summary>
    /// <param name="path">xml的路径</param>
    public void ReadXmlByPath(string path)
    {
        //XML
        XmlDocument xdoc = new XmlDocument();
        xdoc.Load(path);
        XmlNode root = xdoc.SelectSingleNode("Shop");
        XmlNodeList nodes = root.ChildNodes;
        foreach (XmlNode node in nodes)
        {
            string speed = node.ChildNodes[0].InnerText;
            string rotate = node.ChildNodes[1].InnerText;
            string model = node.ChildNodes[2].InnerText;
            string price = node.ChildNodes[3].InnerText;
            string id = node.ChildNodes[4].InnerText;
            m_speed = speed;
            m_rotate = rotate;

            //存储List集合中
            ShopItem item = new ShopItem(speed, rotate, model, price, id);
            shopList.Add(item);
        }
    }

    /// <summary>
    /// 读取金币和最高分数.
    /// </summary>
    public void ReadScoreAndGold(string path)
    {
        //XML
        XmlDocument xdoc = new XmlDocument();
        xdoc.Load(path);
        //获取根节点
        XmlNode root = xdoc.SelectSingleNode("SaveData");
        //获取子节点
        XmlNodeList nodeList = root.ChildNodes;

        int m_goldCount = int.Parse(nodeList[0].InnerText);
        int m_heightScore = int.Parse(nodeList[1].InnerText);
        goldCount = m_goldCount;
        heightScore = m_heightScore;

        //读取商品的购买状态
        for (int i = 2; i < 7; i++)
        {
            shopState.Add(int.Parse(nodeList[i].InnerText));
        }
    }

    /// <summary>
    /// 更新XML文档内容
    /// </summary>
    public void UpdateXMLData(string path,string key, string value)
    {
        //XML
        XmlDocument xdoc = new XmlDocument();
        xdoc.Load(path);
        //获取根节点
        XmlNode root = xdoc.SelectSingleNode("SaveData");
        //获取子节点
        XmlNodeList nodeList = root.ChildNodes;
        foreach (XmlNode node in nodeList)
        {
            if (node.Name == key)
            {
                node.InnerText = value;
                xdoc.Save(path);
            }
        }
    }
}
