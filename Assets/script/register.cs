using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class register : MonoBehaviour
{
    public Button exitBtn;

    // 拖拽赋值
    //账号输入框
    public InputField accountInput;

    //密码输入框
    public InputField pwdInput;

    //注册按钮
    public Button registeBtn;

    string accountKey = "accountKey";
    string pwdKey = "pwdKey";


    // Start is called before the first frame update
    void Start()
    {
        exitBtn.onClick.AddListener(OnClickExit);
        registeBtn.onClick.AddListener(OnRegisteBtn);//注册按钮,点击事件,监听,执行函数OnRegisteBtn
    }
    private void OnClickExit()
    {
        //将自己隐藏
        gameObject.SetActive(false);
    }


    public void OnRegisteBtn()
    {
        // 读取输入框数据
        string accountStr = accountInput.text;
        string pwdStr = pwdInput.text;
        if (!string.IsNullOrEmpty(PlayerPrefs.GetString("accountKey", "")))
        {
            string rcdAccount = PlayerPrefs.GetString("accountKey", "");
            if (accountStr == rcdAccount)
            {
                TipsPanel.Instance.CreateTips("账户已存在，请重新注册");
                return;
            }
        }
        if (accountStr.Length <= 0)
        {
            Debug.Log("账号不能为空,请输入账号");
            TipsPanel.Instance.CreateTips("账号不能为空,请输入账号");
            return;
        }
        if (pwdStr.Length <= 0)
        {
            TipsPanel.Instance.CreateTips("密码不能为空,请输入密码");
            return;
        }
        //保存到本地缓存--本地持久化存储
        PlayerPrefs.SetString(accountKey, accountStr);// 键值 对
        PlayerPrefs.SetString(pwdKey, pwdStr);//存密码
        TipsPanel.Instance.CreateTips("注册成功!");
        gameObject.SetActive(false);
    }
}
