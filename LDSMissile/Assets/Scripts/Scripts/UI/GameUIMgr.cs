using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
/// <summary>
/// 游戏界面UI逻辑控制
/// </summary>
public class GameUIMgr : MonoBehaviour {

    //游戏界面Panel
    private GameObject m_GamePanel;
    //结束界面Panel
    private GameObject m_OverPanel;
    //游戏界面金币数UI
    private UILabel m_StartNum;
    //游戏控制器
    private GameObject m_ButtonControl;
    //重新开始游戏按钮
    private GameObject m_BtnReset;
    //时间UI
    private UILabel m_Time;

    private ShopData shopData;
    //获取XML中最高分数
    private int XMLScore;
    //获取XML中金币数量
    private int XMLGold;
    //xml存档路径.
    private string savePath = "Assets/Data/SaveData.xml";
    //@"C:\Users\LDS\AppData\LocalLow\DefaultCompany\LDSGuided Missile\SaveData.xml";

    //OverPanel----------------------------------------

    //你的分数UI
    private UILabel m_ScoreNum;
    //本轮获得金币数量
    private UILabel m_GetStarNum;
    //本轮获得时间
    private UILabel m_GetTimeNum;

    //时间
    private int time;
    public int Time
    {
        get { return time; }
        set { time = value; UpdateTimeLabel(time); }
    }

    void Start () {

        shopData = new ShopData();
        shopData.ReadScoreAndGold(savePath);
        //获取XML中数据
        XMLScore = shopData.heightScore;
        XMLGold = shopData.goldCount;

        m_GamePanel = GameObject.Find("GamePanel");
        m_OverPanel = GameObject.Find("OverPanel");
        m_StartNum = GameObject.Find("StartNum").GetComponent<UILabel>();
        m_Time = GameObject.Find("Time").GetComponent<UILabel>();
        m_StartNum.text = "0";
        m_Time.text = "0.0";
        StartCoroutine("AddTime");

        //OverPanel----------------------------------------

        m_GetTimeNum = GameObject.Find("GameTime/GetTimeNum").GetComponent<UILabel>();
        m_ScoreNum = GameObject.Find("Score/ScoreNum").GetComponent<UILabel>();
        m_GetStarNum = GameObject.Find("GameStar/GetStarNum").GetComponent<UILabel>();
        m_ButtonControl = GameObject.Find("ButtonControl");
        m_BtnReset = GameObject.Find("BtnReset");

        //-------------------------------------------------

        //按钮点击事件绑定
        UIEventListener.Get(m_BtnReset).onClick = ResetButtonClick;
        //结束面板初始化隐藏
        m_OverPanel.SetActive(false);
    }

    #region UpdateScoreLabel 更新游戏面板分数UI
    /// <summary>
    /// 更新游戏面板分数UI
    /// </summary>
    public void UpdateScoreLabel(int score)
    {
        m_StartNum.text = score.ToString();
    }
    #endregion

    #region UpdateTimeLabel 更新游戏面板时间UI
    /// <summary>
    /// 更新游戏面板时间UI
    /// </summary>
    /// <param name="time"></param>
    public void UpdateTimeLabel(int time)
    {
        if (time < 60)
        {
            m_Time.text = "0:" + time;
        }
        else
        {
            m_Time.text = time / 60 + ":" + time % 60;
        }
    }
    #endregion

    #region SetOverPanelUIInfo 给结束面板UI赋值
    //给结束面板UI赋值
    private void SetOverPanelUIInfo()
    {
        int score = int.Parse(m_StartNum.text);
        m_GetTimeNum.text = "+" + time.ToString();
        m_ScoreNum.text = "" + (int)(score + time * 0.65f);
        m_GetStarNum.text = "+" + score * 3;
        SetPlayerScoreInfo(m_ScoreNum.text);
        SetPlayerGoldInfo(score * 3);
    }
    #endregion

    #region SetPlayerScoreInfo 最高分数存储
    /// <summary>
    /// 最高分数存储
    /// </summary>
    /// <param name="Score"></param>
    private void SetPlayerScoreInfo(string Score)
    {
        if (int.Parse(Score) > XMLScore)
        {
            shopData.UpdateXMLData(savePath,"HeightScore", Score);
        }
    }
    #endregion

    #region SetPlayerGoldInfo 金币数量存储
    /// <summary>
    /// 金币数量存储
    /// </summary>
    /// <param name="Gold">获得的金币数量</param>
    private void SetPlayerGoldInfo(int Gold)
    {
        Gold = Gold + XMLGold;
        shopData.UpdateXMLData(savePath,"GoldCount", Gold.ToString());
    }
    #endregion

    #region ShowOverPanel 显示结束面板
    /// <summary>
    /// 显示结束面板
    /// </summary>
    public void ShowOverPanel()
    {
        StopAddTime();
        m_ButtonControl.SetActive(false);
        m_OverPanel.SetActive(true);
        SetOverPanelUIInfo();
    }
    #endregion

    #region ResetButtonClick 重新开始游戏按钮点击事件
    /// <summary>
    /// 重新开始游戏按钮点击事件
    /// </summary>
    /// <param name="go"></param>
    private void ResetButtonClick(GameObject go)
    {
        SceneManager.LoadScene("Start");
    }
    #endregion

    #region AddTime 携程累加时间计时器
    /// <summary>
    /// 携程累加时间计时器
    /// </summary>
    /// <returns></returns>
    IEnumerator AddTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Time++;
        }
    }
    #endregion

    #region StopAddTime 停止携程
    /// <summary>
    /// 停止携程
    /// </summary>
    private void StopAddTime()
    {
        StopCoroutine("AddTime");
    }
    #endregion
}
