using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(NavMeshAgent))]
public class PathRenderer : MonoBehaviour
{
    private NavMeshAgent _agent;
    private LineRenderer _lineRenderer;

    public Color pathColor = Color.cyan;  // 路径颜色
    public float lineWidth = 0.2f;        // 线条宽度

    private void Start()
    {
        // 获取 NavMeshAgent 和 LineRenderer 组件
        _agent = GetComponent<NavMeshAgent>();
        _lineRenderer = GetComponent<LineRenderer>();

        // 设置 LineRenderer 基础属性
        _lineRenderer.startWidth = lineWidth;
        _lineRenderer.endWidth = lineWidth;
        _lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // 使用简单的着色器
        _lineRenderer.startColor = pathColor;
        _lineRenderer.endColor = pathColor;
    }

    private void Update()
    {
        // 更新路径显示
        DrawPath();
    }

    private void DrawPath()
    {
        if (_agent == null || !_agent.hasPath) return;

        // 获取当前路径的节点
        NavMeshPath path = _agent.path;
        Vector3[] corners = path.corners;

        // 设置线条节点数
        _lineRenderer.positionCount = corners.Length;

        // 将路径节点设置到 LineRenderer 上
        for (int i = 0; i < corners.Length; i++)
        {
            _lineRenderer.SetPosition(i, corners[i]);
        }
    }
}

