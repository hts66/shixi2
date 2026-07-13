using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class register : MonoBehaviour
{
    public Button exitBtn;

    // 注册输入框
    //账号输入框
    public InputField accountInput;

    //密码输入框
    public InputField pwdInput;

    //注册按钮
    public Button registeBtn;

    string accountKey = "accountKey";
    string pwdKey = "pwdKey";

    // 全部12个头像的资源路径（对应你Resources/Avatar下的图片）
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


    void Start()
    {
        exitBtn.onClick.AddListener(OnClickExit);
        registeBtn.onClick.AddListener(OnRegisteBtn);
    }

    private void OnClickExit()
    {
        // 关闭注册面板
        gameObject.SetActive(false);
    }


    public void OnRegisteBtn()
    {
        // 获取输入账号密码
        string accountStr = accountInput.text;
        string pwdStr = pwdInput.text;

        // 判断账号已存在
        if (!string.IsNullOrEmpty(PlayerPrefs.GetString("accountKey", "")))
        {
            string rcdAccount = PlayerPrefs.GetString("accountKey", "");
            if (accountStr == rcdAccount)
            {
                TipsPanel.Instance.CreateTips("账号已存在，请更换账号注册");
                return;
            }
        }

        // 账号非空校验
        if (accountStr.Length <= 0)
        {
            Debug.Log("账号不能为空，请输入账号");
            TipsPanel.Instance.CreateTips("账号不能为空，请输入账号");
            return;
        }
        // 密码非空校验
        if (pwdStr.Length <= 0)
        {
            TipsPanel.Instance.CreateTips("密码不能为空，请输入密码");
            return;
        }

        // 1. 保存账号密码到本地
        PlayerPrefs.SetString(accountKey, accountStr);
        PlayerPrefs.SetString(pwdKey, pwdStr);

        // ========== 新增随机分配头像逻辑 ==========
        // 随机取0~11下标，选中其中一张头像
        int randomIndex = Random.Range(0, avatarPathList.Length);
        string randomAvatar = avatarPathList[randomIndex];
        // 以【账号名_avatarPath】为key，绑定该账号专属头像
        PlayerPrefs.SetString($"{accountStr}_avatarPath", randomAvatar);

        // 强制保存本地数据，防止丢失
        PlayerPrefs.Save();

        TipsPanel.Instance.CreateTips("注册成功!");
        gameObject.SetActive(false);
    }
}
