using UnityEngine;

namespace Misc
{
    public class TransparentInvoker : MonoBehaviour
    {
        public GameObject player;                 // 玩家对象
        public LayerMask obstacleLayer;           // 障碍物层
        public Camera mainCamera;                 // 引用主相机（正交或透视）

        private TransparentHandler lastHandler;   // 记录上次设置透明的对象

        private void Update()
        {
            if (mainCamera == null || player == null)
            {
                Debug.LogWarning("主相机或玩家对象未设置！");
                return;
            }

            // 将玩家位置转换到屏幕空间
            Vector3 screenPoint = mainCamera.WorldToScreenPoint(player.transform.position);

            // 从屏幕空间到世界空间发射射线
            Ray ray = mainCamera.ScreenPointToRay(screenPoint);

#if UNITY_EDITOR
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
#endif

            // 发射射线检测障碍物
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, obstacleLayer))
            {
                // 如果射线碰撞的是玩家本身，则停止检测，不处理透明
                if (hit.collider.gameObject == player)
                {
                    // // 玩家不透明，恢复之前材质
                    if (lastHandler != null)
                    {
                        lastHandler.SwitchToTransparentMaterial(false);
                        lastHandler = null;
                    }
                    return; // 提前返回，终止处理后续逻辑
                }

                // 获取障碍物的透明处理组件
                TransparentHandler handler = hit.collider.GetComponent<TransparentHandler>();
                if (handler != null)
                {
                    if (handler != lastHandler)
                    {
                        // 恢复上次的材质
                        if (lastHandler != null)
                        {
                            lastHandler.SwitchToTransparentMaterial(false);
                        }

                        // 设置当前障碍物为透明
                        handler.SwitchToTransparentMaterial(true);
                        lastHandler = handler;
                    }
                }
            }
            else
            {
                // 如果射线未击中任何障碍物
                if (lastHandler != null)
                {
                    lastHandler.SwitchToTransparentMaterial(false);
                    lastHandler = null;
                }
            }
        }
    }
}


