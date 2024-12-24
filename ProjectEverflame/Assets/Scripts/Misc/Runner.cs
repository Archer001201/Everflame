using System;
using CoreMechanics.TaskScripts;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Misc
{
    public class Runner : MonoBehaviour
    {
        private NavMeshAgent _agent;
        public float wanderRadius = 10f;
        public float wanderTimer = 5f;
        public BaseTask task;
        private float _timer;

        void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _timer = wanderTimer;
        }

        void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= wanderTimer)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                _agent.SetDestination(newPos);
                _timer = 0;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            task.UpdateAmount(1);
            Destroy(gameObject);
        }

        private static Vector3 RandomNavSphere(Vector3 origin, float dist, int layerMask)
        {
            Vector3 randDirection = Random.insideUnitSphere * dist;

            randDirection += origin;

            NavMeshHit navHit;

            NavMesh.SamplePosition(randDirection, out navHit, dist, layerMask);

            return navHit.position;
        }
    }
}
