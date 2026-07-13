using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadMgr : MonoBehaviour
{
    // 加载游戏场景
    public void LoadGameScene()
    {
        // 参数是Build Settings里添加的场景文件名
        SceneManager.LoadScene("GameScene");
    }
}
