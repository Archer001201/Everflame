using System.Collections;
using Ui;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class MoveController : MonoBehaviour
    {
        public LayerMask groundLayer;
        public LayerMask eventLayer;
        private NavMeshAgent _agent;
        private Camera _camera;
        private UiManager _uiManager;

        private void Awake()
        {
            _camera = Camera.main;
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out var hit2, Mathf.Infinity, eventLayer))
            {
                if (hit2.collider.gameObject == _uiManager.eventObject) return;
                _uiManager.UpdateEventPanel(hit2.collider.gameObject);
            }
            else
            {
                if (_uiManager.eventObject) _uiManager.UpdateEventPanel(null);
            }
            
            
            if (!Input.GetMouseButtonDown(0)) return;
            
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, groundLayer))
            {
                _agent.SetDestination(hit.point);
            }
        }

        public void SetUiManager(UiManager manager)
        {
            _uiManager = manager;
        }

        private IEnumerator ChangeSpeed(float rate, float time)
        {
            _agent.speed *= rate;
            yield return new WaitForSeconds(time);
            _agent.speed /= rate;
        }

        public void StartChangeSpeed(float rate, float time)
        {
            StartCoroutine(ChangeSpeed(rate, time));
        }
    }
}
