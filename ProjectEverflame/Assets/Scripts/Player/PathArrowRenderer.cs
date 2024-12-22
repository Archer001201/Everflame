using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathArrowRenderer : MonoBehaviour
{
    public GameObject arrowPrefab;      // 箭头预制体
    public float arrowSpacing = 2f;     // 箭头之间的距离
    public float arrowHeight = 0.2f;    // 箭头高度偏移
    public float removeDistance = 1f;   // 移除箭头的最小距离

    private NavMeshAgent _agent;
    private List<GameObject> _arrows = new List<GameObject>(); // 保存箭头实例
    private Transform _playerTransform; // 玩家位置引用
    private Vector3 _lastPathEndPosition; // 记录上次路径的终点位置

    private void Start()
    {
        // 获取 NavMeshAgent 和玩家 Transform
        _agent = GetComponent<NavMeshAgent>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // 玩家必须有 "Player" 标签
    }

    private void Update()
    {
        // 检查路径是否发生变化
        if (_agent.hasPath && HasPathChanged())
        {
            ClearArrows();  // 清除旧箭头
            GenerateArrows(); // 重新生成箭头
        }

        // 检查并移除玩家靠近的箭头
        RemoveClosestArrow();
    }

    // 生成箭头
    private void GenerateArrows()
    {
        if (_agent == null || !_agent.hasPath) return;

        // 获取路径节点
        Vector3[] corners = _agent.path.corners;

        // 根据箭头间距生成箭头
        for (int i = 0; i < corners.Length - 1; i++)
        {
            Vector3 start = corners[i];
            Vector3 end = corners[i + 1];
            float distance = Vector3.Distance(start, end);

            // 在两个节点之间按间距放置箭头
            for (float j = 0; j < distance; j += arrowSpacing)
            {
                // 计算插值位置
                Vector3 position = Vector3.Lerp(start, end, j / distance);
                position.y += arrowHeight; // 提升箭头高度，避免贴地

                // 计算箭头朝向
                Quaternion rotation = Quaternion.LookRotation(end - start);

                // 生成箭头实例
                GameObject arrow = Instantiate(arrowPrefab, position, rotation);
                _arrows.Add(arrow); // 添加到列表中，保证顺序
            }
        }

        // 更新路径终点位置
        _lastPathEndPosition = _agent.path.corners[_agent.path.corners.Length - 1];
    }

    // 检查路径是否发生变化
    private bool HasPathChanged()
    {
        if (_agent.path.corners.Length == 0) return false;

        // 获取当前路径终点
        Vector3 currentPathEnd = _agent.path.corners[_agent.path.corners.Length - 1];

        // 判断路径终点是否与上次记录不同
        return Vector3.Distance(currentPathEnd, _lastPathEndPosition) > 0.1f;
    }

    // 移除玩家靠近的箭头
    // private void RemoveClosestArrow()
    // {
    //     if (_arrows.Count == 0 || _playerTransform == null) return;
    //
    //     // 遍历箭头顺序，检查距离
    //     for (int i = 0; i < _arrows.Count; i++)
    //     {
    //         float distance = Vector3.Distance(_arrows[i].transform.position, _playerTransform.position);
    //
    //         // 如果玩家距离该箭头小于移除阈值，则移除
    //         if (distance <= removeDistance)
    //         {
    //             Destroy(_arrows[i]);          // 删除箭头
    //             _arrows.RemoveAt(i);         // 从列表中移除该箭头
    //             break;                        // 只移除一个箭头，退出循环
    //         }
    //     }
    // }
    private void RemoveClosestArrow()
    {
        if (_arrows.Count == 0 || _playerTransform == null) return;

        // 找到距离玩家最近的箭头索引
        int closestIndex = -1;
        float minDistance = Mathf.Infinity;

        for (int i = 0; i < _arrows.Count; i++)
        {
            float distance = Vector3.Distance(_arrows[i].transform.position, _playerTransform.position);

            // 更新最小距离及索引
            if (distance < minDistance)
            {
                minDistance = distance;
                closestIndex = i;
            }
        }

        // 检查最近箭头是否需要删除
        if (closestIndex != -1 && minDistance <= removeDistance)
        {
            // 删除最靠近箭头及其前面的所有箭头
            for (int i = closestIndex; i >= 0; i--)
            {
                Destroy(_arrows[i]);          // 删除箭头对象
                _arrows.RemoveAt(i);         // 从列表中移除箭头
            }
        }
    }


    // 清除旧箭头
    private void ClearArrows()
    {
        foreach (GameObject arrow in _arrows)
        {
            Destroy(arrow);
        }
        _arrows.Clear();
    }
}



