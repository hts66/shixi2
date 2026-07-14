using UnityEngine;
using UnityEngine.UI;

public class AudioSettingPanel : MonoBehaviour
{
    [Header("音量滑块")]
    public Slider sliderBGM;
    public Slider sliderSFX;
    [Header("静音开关")]
    public Toggle toggleBGM;
    public Toggle toggleSFX;
    [Header("关闭按钮")]
    public Button btnClose;

    private AudioManage audioMgr;

    void Start()
    {
        audioMgr = AudioManage.Instance;
        // ========== 修复1：UI显示反转，有音乐则勾选，静音则不勾选 ==========
        toggleBGM.isOn = !audioMgr.bgmMute;
        toggleSFX.isOn = !audioMgr.sfxMute;

        // 滑块绑定音量修改（不变）
        sliderBGM.onValueChanged.AddListener(audioMgr.SetBGMVolume);
        sliderSFX.onValueChanged.AddListener(audioMgr.SetSFXVolume);

        // ========== 修复2：Toggle切换逻辑反转 ==========
        toggleBGM.onValueChanged.AddListener((bool isOpen) =>
        {
            // isOpen=true代表勾选（开启音乐），传入mute= false
            audioMgr.ToggleBGMMute(!isOpen);
        });
        toggleSFX.onValueChanged.AddListener((bool isOpen) =>
        {
            audioMgr.ToggleSFXMute(!isOpen);
        });

        // 关闭按钮（不变）
        btnClose.onClick.AddListener(() => gameObject.SetActive(false));
    }
}
