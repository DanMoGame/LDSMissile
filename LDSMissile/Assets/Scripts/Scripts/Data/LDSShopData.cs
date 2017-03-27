using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System.Collections.Generic;

public class LDSShopData : MonoBehaviour {

    //本地读取路径
    private string localShopReadpath;
    //本地写入路径
    private string localShopWritepath;
    //Shop标志位
    private int isShopFirst;
    //XML
    XmlDocument xdoc = new XmlDocument();
    //存储商城数据集合
    public List<ShopItem> shopList = new List<ShopItem>();
    //飞机移动速度
    public string m_speed = "+2";
    //飞机旋转速度
    public string m_rotate = "+3";

    IEnumerator wwwthing()
    {
        WWW www = new WWW(localShopReadpath);
        yield return www;
        byte[] message = www.bytes;
        File.WriteAllBytes(Application.persistentDataPath + "/ShopData.xml", message);
        localShopWritepath = Application.persistentDataPath + "//ShopData.xml";
        ShopDocDeal();
    }

    private void ShopDocDeal()
    {
        xdoc.Load(localShopWritepath);
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
    private void Awake()
    {
        LoadXml();
    }

    public void LoadXml()
    {
        isShopFirst = PlayerPrefs.GetInt("isShop", 0);
        if (isShopFirst == 0)
        {
            PlayerPrefs.SetInt("isShop", 1);
            if (Application.platform == RuntimePlatform.Android)
            {
                localShopReadpath = Application.streamingAssetsPath + "/ShopData.xml";
            }
            else
            {
                localShopReadpath = "file:///" + Application.streamingAssetsPath + "/ShopData.xml";
            }
            StartCoroutine(wwwthing());
            return;
        }
        else
        {
            localShopWritepath = Application.persistentDataPath + "//ShopData.xml";
        }
        ShopDocDeal();
    }
}
