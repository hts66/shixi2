using UnityEngine;

public class TowerTipUI : MonoBehaviour
{
    [Header("弹窗面板本体")]
    public GameObject tipPanel;

    // 存储当前点击的炮台，生成大炮要用它的坐标
    public Transform currentClickTower;

    [Header("三门大炮预制体，分别对应3个按钮")]
    public GameObject towerPrefab1;
    public GameObject towerPrefab2;
    public GameObject towerPrefab3;

    // 弹出面板，传入点击的炮台底座
    public void ShowTip(Transform towerTrans)
    {
        currentClickTower = towerTrans;
        tipPanel.SetActive(true);
    }

    // 关闭弹窗
    public void CloseTip()
    {
        tipPanel.SetActive(false);
        currentClickTower = null;
    }

    // 按钮t1：生成1号大炮
    public void SpawnTower1()
    {
        SpawnCore(towerPrefab1);
    }
    // 按钮t1 (1)：生成2号大炮
    public void SpawnTower2()
    {
        SpawnCore(towerPrefab2);
    }
    // 按钮t1 (2)：生成3号大炮
    public void SpawnTower3()
    {
        SpawnCore(towerPrefab3);
    }

    // 统一生成逻辑
    void SpawnCore(GameObject prefab)
    {
        if (currentClickTower == null || prefab == null)
        {
            Debug.LogError("缺少炮台底座或大炮预制体！");
            return;
        }
        // 继承炮台X、Z，固定Y=14.08
        Vector3 spawnPos = currentClickTower.position;
        spawnPos.y = 14.08f;
        Instantiate(prefab, spawnPos, currentClickTower.rotation);
        CloseTip();
    }
}
