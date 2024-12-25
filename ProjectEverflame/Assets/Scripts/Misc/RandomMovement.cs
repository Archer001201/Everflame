using UnityEngine;

namespace Misc
{
    public class RandomMovement : MonoBehaviour
    {
        public float moveSpeed = 1.0f; // 移动速度
        public float range = 5.0f;    // 移动范围
        public Vector3 centerPoint; // 指定中心点

        private Vector3 targetPosition;

        void Start()
        {
            if (centerPoint == Vector3.zero)
            {
                centerPoint = transform.position; // 默认以初始位置为中心
            }
            SetNewRandomTarget();
        }

        void Update()
        {
            MoveToTarget();

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                SetNewRandomTarget();
            }
        }

        void SetNewRandomTarget()
        {
            Vector3 randomOffset = new Vector3(
                Random.Range(-range, range), 
                0, 
                Random.Range(-range, range)
            );

            targetPosition = centerPoint + randomOffset;
            targetPosition = ClampToRange(targetPosition);
        }

        void MoveToTarget()
        {
            transform.position = Vector3.MoveTowards(
                transform.position, 
                targetPosition, 
                moveSpeed * Time.deltaTime
            );
        }

        Vector3 ClampToRange(Vector3 position)
        {
            float x = Mathf.Clamp(position.x, centerPoint.x - range, centerPoint.x + range);
            float y = Mathf.Clamp(position.y, centerPoint.y - range, centerPoint.y + range);
            float z = Mathf.Clamp(position.z, centerPoint.z - range, centerPoint.z + range);
            return new Vector3(x, y, z);
        }
    }
}


