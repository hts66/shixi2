using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class login : MonoBehaviour
{
    //注册面板
    public GameObject registerPage;
    //注册按钮
    public Button registerBtn;

    //账号输入框
    public InputField accountInput;
    //密码输入框
    public InputField passwordInput;
    //登录按钮
    public Button loginBtn;


    void Start()
    {
        //默认隐藏注册面板
        registerPage.SetActive(false);
        //绑定按钮点击事件
        registerBtn.onClick.AddListener(OnClickRegister);
        loginBtn.onClick.AddListener(OnLoginBtn);
    }

    private void OnClickRegister()
    {
        registerPage.SetActive(true);
    }

    public void OnLoginBtn()
    {
        string accountStr = accountInput.text;
        string passwordStr = passwordInput.text;

        //账号为空校验
        if (string.IsNullOrEmpty(accountStr))
        {
            TipsPanel.Instance.CreateTips("账号不能为空，请输入账号");
            return;
        }
        //密码为空校验
        if (string.IsNullOrEmpty(passwordStr))
        {
            TipsPanel.Instance.CreateTips("密码不能为空，请输入密码");
            return;
        }

        //读取本地保存账号密码
        string rcdAccount = PlayerPrefs.GetString("accountKey", "");
        if (!rcdAccount.Equals(accountStr))
        {
            TipsPanel.Instance.CreateTips("账号不存在，请确认账号或前往注册");
            return;
        }

        string rcdPwd = PlayerPrefs.GetString("pwdKey", "");
        // 修复原代码BUG：之前错误和accountStr对比，现在改为和passwordStr对比
        if (!rcdPwd.Equals(passwordStr))
        {
            TipsPanel.Instance.CreateTips("密码错误，请确认密码");
            return;
        }

        //登录成功提示
        TipsPanel.Instance.CreateTips("登录成功，正在进入游戏...");
        // 修改跳转场景：从 MenuScene 改为 logoScene 加载界面
        SceneManager.LoadScene("logoScene");
    }
}
