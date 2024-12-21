using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class MoveController : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out var hit))
            {
                _agent.SetDestination(hit.point);
            }
        }
    }
}
