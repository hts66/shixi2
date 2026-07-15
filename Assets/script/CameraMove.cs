using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [Header("镜头前后移动速度")]
    public float moveSpeed = 10f;

    [Header("Z轴移动边界")]
    public float zMax = 12f;
    public float zMin = -50f;

    void Update()
    {
        // 获取W/S输入
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(0, 0, verticalInput);
        moveDir.Normalize();

        // 先执行移动
        transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.World);

        // 锁定Z轴在 -50 ~ 12 之间
        Vector3 clampPos = transform.position;
        clampPos.z = Mathf.Clamp(clampPos.z, zMin, zMax);
        transform.position = clampPos;
    }
}
