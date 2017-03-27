using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
/// <summary>
/// 开始界面UI逻辑控制器
/// </summary>
public class StartUIMgr : MonoBehaviour {

    //-----开始面板UI----------
    //开始面板
    private GameObject m_StartPanel;
    //开始游戏按钮
    private GameObject m_BtnPlay;
    //主界面飞机展示
    private GameObject m_GameShip;
    //游戏音效
    private GameObject m_Audio;
    //------------------------

    //-----设置面板UI----------
    //设置面板
    private GameObject m_SettingPanel;
    //显示设置面板的按钮
    private GameObject m_BtnSetting;
    //设置面板的关闭按钮
    private GameObject m_BtnClose;
    //声音开关按钮
    private GameObject m_BtnSound;
    //获取声音按钮的精灵组件
    private UISprite m_PlaySound;
    //设置面板提示
    private GameObject m_Tip;
    //主音乐控制器
    private bool MainSoundswitchFlag = true;
    //音效控制器
    private bool AudioswitchFlag = true;
    //------------------------

    //-----商城面板UI----------
    //商城面板
    private GameObject m_ShopPanel;
    //显示商城的按钮
    private GameObject m_BtnHome;
    //商城关闭按钮
    private GameObject m_ShopClose;
    //------------------------

    void Start () {
        m_StartPanel = GameObject.Find("StartPanel");
        m_SettingPanel = GameObject.Find("SettingPanel");
        m_BtnHome = GameObject.Find("Home");
        m_BtnSetting = GameObject.Find("Setting");
        m_BtnClose = GameObject.Find("BtnClose");
        m_BtnPlay = GameObject.Find("Play");
        m_ShopPanel = GameObject.Find("ShopPanel");
        m_ShopClose = GameObject.Find("ShopClose");
        m_GameShip = GameObject.Find("GameShip");
        m_BtnSound = GameObject.Find("Sound/BtnSound");
        m_PlaySound = m_BtnSound.GetComponent<UISprite>();
        m_Tip = GameObject.Find("Sound/Tip");
        m_Audio = GameObject.Find("Audio");

        UIEventListener.Get(m_BtnSetting).onClick = SettingButtonClick;
        UIEventListener.Get(m_BtnClose).onClick = CloseButtonClick;
        UIEventListener.Get(m_BtnPlay).onClick = PlayButtonClick;
        UIEventListener.Get(m_BtnHome).onClick = ShopButtonClick;
        UIEventListener.Get(m_ShopClose).onClick = CloseShopButtonClick;
        UIEventListener.Get(m_BtnSound).onClick = MainSound;
        UIEventListener.Get(m_Audio).onClick = MainSound;

        m_SettingPanel.SetActive(false);
        m_ShopPanel.SetActive(false);
        m_Tip.SetActive(false);
    }
    #region MainSound 音乐控制
    /// <summary>
    /// 音乐控制
    /// </summary>
    /// <param name="go"></param>
    private void MainSound(GameObject go)
    {
        if (go.name == "BtnSound")
        {
            if (MainSoundswitchFlag)
            {
                //默认播放音乐
                m_PlaySound.spriteName = "audio_1";
                m_BtnSound.GetComponent<UIButton>().normalSprite = "audio_1";
                MainSoundswitchFlag = false;
            }
            else
            {
                //音乐停止,图片更换
                m_PlaySound.spriteName = "audio_2";
                m_BtnSound.GetComponent<UIButton>().normalSprite = "audio_2";
                MainSoundswitchFlag = true;
            }
            m_Tip.SetActive(true);
        }
        if (go.name == "Audio")
        {
            if (AudioswitchFlag)
            {
                //默认播放音乐
                m_Audio.GetComponent<UISprite>().spriteName = "audio_on";
                m_Audio.GetComponent<UIButton>().normalSprite = "audio_on";
                AudioswitchFlag = false;
            }
            else
            {
                //音乐停止,图片更换
                m_Audio.GetComponent<UISprite>().spriteName = "audio_off";
                m_Audio.GetComponent<UIButton>().normalSprite = "audio_off";
                AudioswitchFlag = true;
            }
        }
}
    #endregion

    #region 设置
    /// <summary>
    /// 设置按钮被点击
    /// </summary>
    /// <param name="go"></param>
    private void SettingButtonClick(GameObject go)
    {
        if (m_ShopPanel.activeSelf == true)
        {
            m_ShopPanel.SetActive(false);
            m_SettingPanel.SetActive(true);
        }
        m_SettingPanel.SetActive(true);
        m_GameShip.SetActive(false);
    }

    /// <summary>
    /// 管理按钮点击事件
    /// </summary>
    /// <param name="go"></param>
    private void CloseButtonClick(GameObject go)
    {
        m_Tip.SetActive(false);
        m_SettingPanel.SetActive(false);
        m_GameShip.SetActive(true);
    }
    #endregion

    #region 游戏开始
    /// <summary>
    /// 开始游戏按钮点击事件
    /// </summary>
    /// <param name="go"></param>
    private void PlayButtonClick(GameObject go)
    {
        //跳转场景
        SceneManager.LoadScene("Game");
    }
    #endregion

    #region 商城
    /// <summary>
    /// 商城按钮点击事件
    /// </summary>
    /// <param name="go"></param>
    private void ShopButtonClick(GameObject go)
    {
        if (m_SettingPanel.activeSelf == true)
        {
            m_SettingPanel.SetActive(false);
            m_ShopPanel.SetActive(true);
        }
        m_ShopPanel.SetActive(true);
        m_GameShip.SetActive(false);
    }

    /// <summary>
    /// 商城关闭按钮点击事件
    /// </summary>
    /// <param name="go"></param>
    private void CloseShopButtonClick(GameObject go)
    {
        m_ShopPanel.SetActive(false);
        m_GameShip.SetActive(true);
    }
    #endregion

    #region SetPlayButtonState 这是开始游戏按钮的状态
    /// <summary>
    /// 这是开始游戏按钮的状态 
    /// </summary>
    /// <param name="state"></param>
    public void SetPlayButtonState(int state)
    {
        if (state == 1)
        {
            m_BtnPlay.SetActive(true);
        }
        else
        {
            m_BtnPlay.SetActive(false);
        }
    }
    #endregion
}
