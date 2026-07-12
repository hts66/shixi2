using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoAudioPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // 获取自身挂载的AudioSource组件
        audioSource = GetComponent<AudioSource>();

        // 判断是否赋值了音频，防止空引用报错
        if (audioSource != null && audioSource.clip != null)
        {
            // 播放logo音效
            audioSource.Play();
            // 根据音频长度延时，播放完成后跳转菜单场景
            float playTime = audioSource.clip.length;
            Invoke(nameof(JumpToMenu), playTime);
        }
        else
        {
            // 没有音频资源，直接跳转
            JumpToMenu();
        }
    }

    /// <summary>音频播放完毕，跳转到主菜单MenuScene</summary>
    void JumpToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
