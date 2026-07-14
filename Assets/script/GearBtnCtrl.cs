using UnityEngine;
using UnityEngine.UI;

public class GearBtnCtrl : MonoBehaviour
{
    [Tooltip("音频设置面板物体，拖入场景里的AudioSetPanel")]
    public GameObject audioSetPanel;

    private Button gearBtn;

    void Awake()
    {
        // 自动给齿轮添加Button组件（没有就新建）
        gearBtn = GetComponent<Button>();
        if (gearBtn == null)
        {
            gearBtn = gameObject.AddComponent<Button>();
        }

        // 绑定点击事件：播放音效 + 打开面板
        gearBtn.onClick.AddListener(ClickGear);
    }

    void ClickGear()
    {
        // 播放按钮点击音效
        AudioManage.PlayBtnSound();
        // 弹出设置面板
        if (audioSetPanel != null)
        {
            audioSetPanel.SetActive(true);
        }
    }
}
