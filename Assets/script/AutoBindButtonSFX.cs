using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 脚本执行顺序优先启动，场景加载完成自动绑定按钮音效
[DefaultExecutionOrder(-100)]
public class AutoBindButtonSFX : MonoBehaviour
{
    void Awake()
    {
        // 监听场景切换，新场景加载完自动绑定所有按钮
        SceneManager.sceneLoaded += OnSceneLoaded;
        // 当前已加载场景立刻执行一次绑定
        BindAllButtonInScene();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 新场景加载完毕，自动遍历所有按钮绑定音效
        BindAllButtonInScene();
    }

    // 遍历当前场景全部Button，自动绑定点击音效
    void BindAllButtonInScene()
    {
        Button[] allButtons = Object.FindObjectsOfType<Button>(includeInactive:true);
        foreach (Button btn in allButtons)
        {
            // 避免重复绑定
            btn.onClick.RemoveListener(AudioManage.PlayBtnSound);
            btn.onClick.AddListener(AudioManage.PlayBtnSound);
        }
        Debug.Log($"自动绑定音效完成，当前场景按钮总数：{allButtons.Length}");
    }
}
