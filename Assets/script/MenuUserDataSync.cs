using UnityEngine;
using UnityEngine.UI;

public class MenuUserDataSync : MonoBehaviour
{
    [Header("账号文本组件")]
    public Text accountTxt;
    [Header("金币文本组件")]
    public Text goldTxt;
    [Header("等级文本组件")]
    public Text lvTxt;
    [Header("头像Image组件（UserInfoBg下的head）")]
    public Image headImage;

    // 全部12个头像资源路径，和注册脚本保持一致
    private string[] avatarPathList = new string[]
    {
        "Avatar/UI_EmotionIcon1",
        "Avatar/UI_EmotionIcon2",
        "Avatar/UI_EmotionIcon3",
        "Avatar/UI_EmotionIcon4",
        "Avatar/UI_EmotionIcon5",
        "Avatar/UI_EmotionIcon6",
        "Avatar/UI_EmotionIcon7",
        "Avatar/UI_EmotionIcon8",
        "Avatar/UI_EmotionIcon9",
        "Avatar/UI_EmotionIcon10",
        "Avatar/UI_EmotionIcon11",
        "Avatar/UI_EmotionIcon12"
    };

    private string currentLoginAccount;

    void Start()
    {
        // 读取登录缓存数据
        currentLoginAccount = PlayerPrefs.GetString("currentAccount", "未登录");
        int userGold = PlayerPrefs.GetInt("gold", 0);
        int userLevel = PlayerPrefs.GetInt("level", 0);
        string avatarResPath = PlayerPrefs.GetString("currentAvatarPath", "Avatar/UI_EmotionIcon1");

        // 同步文字信息
        accountTxt.text = $"账号：{currentLoginAccount}";
        goldTxt.text = $"金币：{userGold}";
        lvTxt.text = $"等级：{userLevel}";

        // 加载初始头像
        RefreshAvatar(avatarResPath);

        // 代码自动绑定头像点击事件，不用手动在编辑器选函数
        Button headBtn = headImage.GetComponent<Button>();
        if (headBtn != null)
        {
            headBtn.onClick.AddListener(ChangeRandomAvatar);
        }
    }

    /// <summary>点击头像触发：随机更换头像并保存（加了public，编辑器下拉能识别）</summary>
    public void ChangeRandomAvatar()
    {
        // 随机选一张头像
        int randomIndex = Random.Range(0, avatarPathList.Length);
        string newAvatarPath = avatarPathList[randomIndex];

        // 1. 刷新界面显示新头像
        RefreshAvatar(newAvatarPath);

        // 2. 更新本地全局缓存（当前登录用）
        PlayerPrefs.SetString("currentAvatarPath", newAvatarPath);
        // 3. 绑定到账号永久保存
        PlayerPrefs.SetString($"{currentLoginAccount}_avatarPath", newAvatarPath);
        PlayerPrefs.Save();

        // 判空保护，防止TipsPanel销毁后访问报错
        if (TipsPanel.Instance != null)
        {
            TipsPanel.Instance.CreateTips("头像更换成功！");
        }
    }

    /// <summary>根据路径刷新头像显示</summary>
    void RefreshAvatar(string path)
    {
        Sprite avatarSprite = Resources.Load<Sprite>(path);
        if (avatarSprite != null)
        {
            headImage.sprite = avatarSprite;
        }
        else
        {
            Debug.LogError("头像加载失败，路径：" + path);
        }
    }

    // 退出场景移除监听，防止内存泄漏
    private void OnDestroy()
    {
        Button headBtn = headImage.GetComponent<Button>();
        if (headBtn != null)
        {
            headBtn.onClick.RemoveListener(ChangeRandomAvatar);
        }
    }
}
