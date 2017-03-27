using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

public class LDSSaveData : MonoBehaviour {

    //本地读取路径
    private string localSaveReadpath;
    //本地写入路径
    private string localSaveWritepath;
    //Shop标志位
    private int isSaveFirst;
    //XML
    XmlDocument xdoc = new XmlDocument();
    //商品的购买状态集合
    public List<int> shopState = new List<int>();
    //金币数
    public int goldCount = 0;
    //最高分数
    public int heightScore = 0;

    IEnumerator wwwthing()
    {
        WWW www = new WWW(localSaveReadpath);
        yield return www;
        byte[] message = www.bytes;
        File.WriteAllBytes(Application.persistentDataPath + "/SaveData.xml", message);
        localSaveWritepath = Application.persistentDataPath + "//SaveData.xml";
        SaveDocDeal();
    }

    public void SaveDocDeal()
    {
        //XML
        XmlDocument xdoc = new XmlDocument();
        xdoc.Load(localSaveWritepath);
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
    private void Awake()
    {
        LoadXml();
    }

    public void LoadXml()
    {
        isSaveFirst = PlayerPrefs.GetInt("isSave", 0);
        if (isSaveFirst == 0)
        {
            PlayerPrefs.SetInt("isSave", 1);
            if (Application.platform == RuntimePlatform.Android)
            {
                localSaveReadpath = Application.streamingAssetsPath + "/SaveData.xml";
            }
            else
            {
                localSaveReadpath = "file:///" + Application.streamingAssetsPath + "/SaveData.xml";
            }
            StartCoroutine(wwwthing());
            return;
        }
        else
        {
            localSaveWritepath = Application.persistentDataPath + "//SaveData.xml";
        }
        SaveDocDeal();
    }

    /// <summary>
    /// 更新XML文档内容
    /// </summary>
    public void UpdateXMLData(string key, string value)
    {
        xdoc.Load(localSaveWritepath);
        //获取根节点
        XmlNode root = xdoc.SelectSingleNode("SaveData");
        //获取子节点
        XmlNodeList nodeList = root.ChildNodes;
        foreach (XmlNode node in nodeList)
        {
            if (node.Name == key)
            {
                node.InnerText = value;
                xdoc.Save(localSaveWritepath);
            }
        }
    }

}
