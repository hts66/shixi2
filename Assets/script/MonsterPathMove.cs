using UnityEngine;
using System.Collections.Generic;

public class MonsterPathMove : MonoBehaviour
{
    [Header("路径Cube节点，按【起点→拐弯→终点】顺序拖拽")]
    public List<Transform> pathPoints;
    [Header("怪物移动速度")]
    public float moveSpeed = 2.5f;
    [Header("到达节点判定距离（越大转弯越提前）")]
    public float reachPointDistance = 0.3f;

    private int currentTargetIndex;
    private Transform currentTargetPoint;
    private float fixedYHeight;

    void Start()
    {
        // 锁定怪物初始Y高度，全程不会上下浮动
        fixedYHeight = transform.position.y;

        // 没有路径直接停止运行
        if (pathPoints == null || pathPoints.Count == 0)
        {
            Debug.LogWarning("怪物没有配置路径节点！");
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
        if (pathPoints == null || pathPoints.Count < 2) return;
        Gizmos.color = Color.green;
        for (int i = 0; i < pathPoints.Count - 1; i++)
        {
            Transform a = pathPoints[i];
            Transform b = pathPoints[i + 1];
            if (a != null && b != null)
            {
                Gizmos.DrawLine(a.position, b.position);
                Gizmos.DrawSphere(a.position, 0.25f);
            }
        }
        Gizmos.DrawSphere(pathPoints[pathPoints.Count - 1].position, 0.25f);
    }
}
