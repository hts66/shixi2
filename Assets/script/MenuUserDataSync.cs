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

    void Start()
    {
        // 读取登录缓存数据
        string loginAccount = PlayerPrefs.GetString("currentAccount", "未登录");
        int userGold = PlayerPrefs.GetInt("gold", 0);
        int userLevel = PlayerPrefs.GetInt("level", 0);
        string avatarResPath = PlayerPrefs.GetString("currentAvatarPath", "Avatar/UI_EmotionIcon1");

        // 同步文字信息
        accountTxt.text = $"账号：{loginAccount}";
        goldTxt.text = $"金币：{userGold}";
        lvTxt.text = $"等级：{userLevel}";

        // 动态加载Resources里的头像并赋值到UI
        Sprite avatarSprite = Resources.Load<Sprite>(avatarResPath);
        if (avatarSprite != null)
        {
            headImage.sprite = avatarSprite;
        }
        else
        {
            Debug.LogError("头像加载失败，资源路径：" + avatarResPath);
        }
    }
}
