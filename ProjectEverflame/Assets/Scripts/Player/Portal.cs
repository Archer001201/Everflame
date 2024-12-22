using UnityEngine;

public class Portal : MonoBehaviour
{
    public static Portal PortalA; // 第一个传送门的引用
    public static Portal PortalB; // 第二个传送门的引用

    public Transform targetPosition; // 指定的传送位置

    private bool _isTeleporting; // 防止重复传送的标记

    private void Awake()
    {
        // 动态管理两个传送门
        if (Portal.PortalA == null)
        {
            Portal.PortalA = this;
        }
        else if (Portal.PortalB == null)
        {
            Portal.PortalB = this;
        }
        else
        {
            // 替换最老的传送门
            Destroy(Portal.PortalA.gameObject);
            Portal.PortalA = Portal.PortalB;
            Portal.PortalB = this;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 检查是否为玩家
        if (!_isTeleporting && other.CompareTag("Player"))
        {
            Portal targetPortal = GetTargetPortal();
            if (targetPortal != null && targetPortal.targetPosition != null)
            {
                // 执行传送
                StartCoroutine(Teleport(other, targetPortal.targetPosition));
            }
        }
    }

    // 获取目标传送门
    private Portal GetTargetPortal()
    {
        if (this == PortalA)
            return PortalB;
        else if (this == PortalB)
            return PortalA;
        return null;
    }

    // 执行传送
    private System.Collections.IEnumerator Teleport(Collider player, Transform targetTransform)
    {
        // 设置传送状态，防止连锁触发
        _isTeleporting = true;

        // 禁用物理交互
        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; // 暂时禁用物理效果，避免位移冲突
        }

        // 瞬间移动到目标传送位置
        player.transform.position = targetTransform.position;
        player.transform.rotation = targetTransform.rotation;

        // 延迟短时间防止重复触发
        yield return new WaitForSeconds(0.2f);

        // 恢复物理交互
        if (rb != null)
        {
            rb.isKinematic = false;
        }

        _isTeleporting = false;
    }

    private void OnDestroy()
    {
        // 清理引用
        if (this == PortalA)
        {
            PortalA = null;
        }
        else if (this == PortalB)
        {
            PortalB = null;
        }
    }
}


