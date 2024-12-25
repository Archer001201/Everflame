using UnityEngine;

namespace Ui
{
    public class WorldSpaceCanvas : MonoBehaviour
    {
        private Transform camTransform; // 摄像机Transform

        void Start()
        {
            camTransform = Camera.main.transform; // 获取主摄像机
        }

        void LateUpdate()
        {
            // Canvas 始终朝向摄像机
            transform.LookAt(transform.position + camTransform.rotation * Vector3.forward,
                camTransform.rotation * Vector3.up);
        }
    }
}
