using UnityEngine;
using UnityEngine.UI;

public class MenuUserDataSync : MonoBehaviour
{
    [Header("账号文本")]
    public Text accountTxt;
    [Header("金币文本")]
    public Text goldTxt;
    [Header("等级文本")]
    public Text lvTxt;

    void Start()
    {
        // 读取本地保存的登录信息
        string loginAccount = PlayerPrefs.GetString("currentAccount", "未登录");
        int userGold = PlayerPrefs.GetInt("gold", 0);
        int userLevel = PlayerPrefs.GetInt("level", 0);

        // 赋值到UI文字
        accountTxt.text = $"账号：{loginAccount}";
        goldTxt.text = $"金币：{userGold}";
        lvTxt.text = $"等级：{userLevel}";
    }
}
