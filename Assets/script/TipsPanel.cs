using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsPanel : MonoBehaviour
{
    public static TipsPanel Instance = null;

    public GameObject tipsPrefab;

    private void Awake()
    {
        if (TipsPanel.Instance != null)
        {
            Destroy(TipsPanel.Instance);
            return;
        }
        TipsPanel.Instance = this;
       // DontDestroyOnLoad(TipsPanel.Instance);
    }
    public void CreateTips(string showContent)
    {
        GameObject newgameObject = Instantiate(tipsPrefab, this.transform);
        Tips tips = newgameObject.GetComponent<Tips>();
        tips.Init(showContent);
    }
}
