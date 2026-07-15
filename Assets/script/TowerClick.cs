using UnityEngine;

public class TowerClick : MonoBehaviour
{
    [Header("弹窗管理器Canvas")]
    public TowerTipUI towerTipUI;

    void Update()
    {
        // 鼠标左键点击
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // 点击到自身炮台
                if (hit.collider.gameObject == gameObject)
                {
                    if (towerTipUI != null)
                    {
                        // 把当前炮台Transform传给弹窗，再显示面板
                        towerTipUI.ShowTip(transform);
                    }
                    else
                    {
                        Debug.LogError("TowerClick未赋值TowerTipUI（Canvas）");
                    }
                }
            }
        }
    }
}
