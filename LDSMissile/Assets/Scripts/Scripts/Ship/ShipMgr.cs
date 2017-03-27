using UnityEngine;
using System.Collections;

/// <summary>
/// Game游戏中加载飞机管理器
/// </summary>
public class ShipMgr : MonoBehaviour {

    //飞机模型
    private GameObject model;
    //玩家操控的飞机
    private GameObject player;

    void Start () {
        string ship = PlayerPrefs.GetString("PlayerName");
        switch (ship)
        {
            case "PlayerUI/PlayerSpin0":
                model = Resources.Load<GameObject>("Ship/Ship_0");
                player = GameObject.Instantiate(model, Vector3.zero, Quaternion.identity) as GameObject;
                player.AddComponent<Ship>();
                player.tag = "Player";
                player.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
                break;
            case "PlayerUI/PlayerSpin1":
                model = Resources.Load<GameObject>("Ship/Ship_1");
                player = GameObject.Instantiate(model, Vector3.zero, Quaternion.identity) as GameObject;
                player.AddComponent<Ship>();
                player.tag = "Player";
                player.GetComponent<Transform>().localScale = new Vector3(3, 3, 3);
                break;
            case "PlayerUI/PlayerSpin2":
                model = Resources.Load<GameObject>("Ship/Ship_2");
                player = GameObject.Instantiate(model, Vector3.zero, Quaternion.identity) as GameObject;
                player.AddComponent<Ship>();
                player.tag = "Player";
                player.GetComponent<Transform>().localScale = new Vector3(2, 2, 2);
                break;
            case "PlayerUI/PlayerSpin3":
                model = Resources.Load<GameObject>("Ship/Ship_3");
                player = GameObject.Instantiate(model, Vector3.zero, Quaternion.identity) as GameObject;
                player.AddComponent<Ship>();
                player.tag = "Player";
                player.GetComponent<Transform>().localScale = new Vector3(2, 2, 2);
                break;
            case "PlayerUI/PlayerSpin4":
                model = Resources.Load<GameObject>("Ship/Ship_4");
                player = GameObject.Instantiate(model, Vector3.zero, Quaternion.identity) as GameObject;
                player.AddComponent<Ship>();
                player.tag = "Player";
                player.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
                break;
        }
	}
}
