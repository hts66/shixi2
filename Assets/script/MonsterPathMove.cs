using UnityEngine;
using System.Collections.Generic;

public class MonsterPathMove : MonoBehaviour
{
    [Header("怪物移动速度")]
    public float moveSpeed = 2.5f;
    [Header("到达节点判定距离（越大转弯越提前）")]
    public float reachPointDistance = 0.3f;

    // 自动获取，不需要在面板赋值，删掉public暴露
    private List<Transform> pathPoints;
    private int currentTargetIndex;
    private Transform currentTargetPoint;
    private float fixedYHeight;

    void Start()
    {
        // 锁定怪物初始Y高度，全程不会上下浮动
        fixedYHeight = transform.position.y;

        // 自动找到场景里的PathRoot，读取所有子Cube作为路径点
        GameObject pathRootObj = GameObject.Find("PathRoot");
        if (pathRootObj == null)
        {
            Debug.LogError("场景找不到PathRoot物体，请检查层级！");
            enabled = false;
            return;
        }

        pathPoints = new List<Transform>();
        foreach (Transform child in pathRootObj.transform)
        {
            pathPoints.Add(child);
        }

        // 没有路径直接停止运行
        if (pathPoints.Count == 0)
        {
            Debug.LogWarning("PathRoot下没有任何路径Cube子物体！");
            enabled = false;
            return;
        }

        // 初始化第一个目标点
        currentTargetIndex = 0;
        currentTargetPoint = pathPoints[currentTargetIndex];
    }

    void Update()
    {
        if (currentTargetPoint == null) return;

        // 只水平转向，不会上下歪头
        Vector3 lookDir = currentTargetPoint.position - transform.position;
        lookDir.y = 0;
        transform.rotation = Quaternion.LookRotation(lookDir);

        // 移动时强制锁定Y坐标，不会埋地消失
        Vector3 targetPos = currentTargetPoint.position;
        targetPos.y = fixedYHeight;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        // 只计算地面平面距离，忽略高度
        Vector3 monsterFlat = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 pointFlat = new Vector3(currentTargetPoint.position.x, 0, currentTargetPoint.position.z);
        float distance = Vector3.Distance(monsterFlat, pointFlat);

        if (distance < reachPointDistance)
        {
            SwitchNextPoint();
        }
    }

    // 切换下一个路径点
    void SwitchNextPoint()
    {
        currentTargetIndex++;
        // 走到最后一个终点
        if (currentTargetIndex >= pathPoints.Count)
        {
            ArriveDestination();
            return;
        }
        currentTargetPoint = pathPoints[currentTargetIndex];
    }

    // 抵达终点销毁怪物
    void ArriveDestination()
    {
        Debug.Log("怪物到达终点");
        Destroy(gameObject);
    }

    // Scene窗口绘制绿色路径辅助线，方便查看路线
    void OnDrawGizmos()
    {
        GameObject pathRootObj = GameObject.Find("PathRoot");
        if (pathRootObj == null) return;

        List<Transform> tempPath = new List<Transform>();
        foreach (Transform child in pathRootObj.transform)
        {
            tempPath.Add(child);
        }

        if (tempPath.Count < 2) return;
        Gizmos.color = Color.green;
        for (int i = 0; i < tempPath.Count - 1; i++)
        {
            Transform a = tempPath[i];
            Transform b = tempPath[i + 1];
            if (a != null && b != null)
            {
                Gizmos.DrawLine(a.position, b.position);
                Gizmos.DrawSphere(a.position, 0.25f);
            }
        }
        Gizmos.DrawSphere(tempPath[tempPath.Count - 1].position, 0.25f);
    }
}
