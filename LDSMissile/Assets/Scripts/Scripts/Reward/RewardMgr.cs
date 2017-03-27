using UnityEngine;
using System.Collections;
/// <summary>
/// 奖励物品管理
/// </summary>
public class RewardMgr : MonoBehaviour {

    //获取当前管理器
    private Transform m_Trans;
    private GameObject m_rewardPrefab;
    //当前奖励物品数量
    private int rewardCount = 0;
    //当前最大奖励物品数量
    private int rewardMaxCount = 5;

    void Start () {
        m_Trans = gameObject.GetComponent<Transform>();
        m_rewardPrefab = Resources.Load<GameObject>("reward");

        InvokeRepeating("CreateReward", 5.0f, 5.0f);
	}

    /// <summary>
    /// 生成奖励物品
    /// </summary>
    private void CreateReward()
    {
        if (rewardCount < rewardMaxCount)
        {
            Vector3 pos = new Vector3(Random.Range(-650, 570), 0, Random.Range(-460,500));
            GameObject.Instantiate(m_rewardPrefab, pos, Quaternion.identity, m_Trans);
            rewardCount++;
        }
    }

    /// <summary>
    /// 停止生成奖励物品
    /// </summary>
    public void StopCreate()
    {
        CancelInvoke();
    }

    //当前奖励物品数量减少
    public void RewardCountDown()
    {
        rewardCount--;
    }
}
