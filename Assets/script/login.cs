using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class login : MonoBehaviour
{
    //定义注册面板
    public GameObject registerPage;
    //定义注册按钮
    public Button registerBtn;

    //账号
    public InputField accountInput;
    //密码
    public InputField passwordInput;
    //登录按钮
    public Button loginBtn;


    // Start is called before the first frame update
    void Start()
    {
        //注册面板隐藏
        registerPage.SetActive(false);
        //注册按钮的点击事件
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
        if (accountStr == null || accountStr.Length <= 0)
        {
            TipsPanel.Instance.CreateTips("账号不能为空,请输入账号");
            return;
        }
        if (passwordStr == null || passwordStr.Length <= 0)
        {
            TipsPanel.Instance.CreateTips("密码不能为空,请输入密码");
            return;
        }

        string rcdAccount = PlayerPrefs.GetString("accountKey", "");
        if (!rcdAccount.Equals(accountStr))
        {
            TipsPanel.Instance.CreateTips("账号不存在,请输入正确账号,或者注册新账号");
            return;
        }

        string rcdPwd = PlayerPrefs.GetString("pwdKey", "");
        if (!rcdPwd.Equals(accountStr))
        {
            TipsPanel.Instance.CreateTips("密码错误,请输入正确密码");
            return;
        }
        //进入下一个界面
        TipsPanel.Instance.CreateTips("登录成功,进入游戏");
        //TODO 切换场景
        SceneManager.LoadScene("MenuScene");
    }
}
