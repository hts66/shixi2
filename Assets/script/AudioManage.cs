using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManage : MonoBehaviour
{
    // 单例全局访问点，任何脚本直接 AudioManage.Instance.xxx()
    public static AudioManage Instance;

    [Header("背景音乐播放器")]
    public AudioSource bgmAudioSource;
    [Header("音效播放器（复用同一个，无需多个音源）")]
    public AudioSource sfxAudioSource;

    [Header("背景音乐素材")]
    public AudioClip bgmClip;
    [Header("按钮点击音效素材")]
    public AudioClip buttonClickClip;

    // 全局音量数值
    [HideInInspector] public float bgmVolume;
    [HideInInspector] public float sfxVolume;
    [HideInInspector] public bool bgmMute;
    [HideInInspector] public bool sfxMute;

    void Awake()
    {
        // 单例初始化，防止多个音频管理器
        if (Instance != null && Instance != this) Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 切换场景不销毁音频
        }

        // 读取本地保存的音量设置
        LoadAudioSetting();
    }

    void Start()
    {
        // 自动循环播放背景音乐
        PlayBGM();
    }

    #region 背景音乐控制
    /// <summary>播放背景音乐</summary>
    public void PlayBGM()
    {
        if (bgmClip == null) return;
        bgmAudioSource.clip = bgmClip;
        bgmAudioSource.loop = true;
        bgmAudioSource.volume = bgmMute ? 0 : bgmVolume;
        bgmAudioSource.Play();
    }

    /// <summary>修改背景音乐音量</summary>
    public void SetBGMVolume(float value)
    {
        bgmVolume = value;
        if (!bgmMute) bgmAudioSource.volume = bgmVolume;
        SaveAudioSetting();
    }

    /// <summary>背景音乐静音开关</summary>
    public void ToggleBGMMute(bool isMute)
    {
        bgmMute = isMute;
        bgmAudioSource.volume = bgmMute ? 0 : bgmVolume;
        SaveAudioSetting();
    }
    #endregion

    #region 音效控制（按钮点击共用）
    /// <summary>播放按钮点击音效（实例方法）</summary>
    public void PlayButtonClickSFX()
    {
        if (buttonClickClip == null || sfxMute) return;
        sfxAudioSource.volume = sfxVolume;
        sfxAudioSource.PlayOneShot(buttonClickClip);
    }

    // ======== 方案A新增静态方法，按钮直接调用无需拖拽物体 ========
    /// <summary>静态方法：所有场景按钮事件直接选中这个函数播放点击音效</summary>
    public static void PlayBtnSound()
    {
        if (Instance != null)
        {
            Instance.PlayButtonClickSFX();
        }
    }

    /// <summary>修改音效音量</summary>
    public void SetSFXVolume(float value)
    {
        sfxVolume = value;
        SaveAudioSetting();
    }

    /// <summary>音效静音开关</summary>
    public void ToggleSFXMute(bool isMute)
    {
        sfxMute = isMute;
        SaveAudioSetting();
    }
    #endregion

    #region 本地存储读写音量
    void LoadAudioSetting()
    {
        // 默认值：音量0.8，不静音
        bgmVolume = PlayerPrefs.GetFloat("BGM_Volume", 0.8f);
        sfxVolume = PlayerPrefs.GetFloat("SFX_Volume", 0.8f);
        bgmMute = PlayerPrefs.GetInt("BGM_Mute", 0) == 1;
        sfxMute = PlayerPrefs.GetInt("SFX_Mute", 0) == 1;
    }

    void SaveAudioSetting()
    {
        PlayerPrefs.SetFloat("BGM_Volume", bgmVolume);
        PlayerPrefs.SetFloat("SFX_Volume", sfxVolume);
        PlayerPrefs.SetInt("BGM_Mute", bgmMute ? 1 : 0);
        PlayerPrefs.Save();
    }
    #endregion
}
