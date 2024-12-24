using System;
using System.Collections;
using Ui;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    public class Thruster : MonoBehaviour
    {
        public float dashForce = 10f;       
        public float dashDuration = 0.3f;  
        public float charge = 10;
        public bool unlocked;

        private Rigidbody _rb;
        private NavMeshAgent _agent;

        private Vector3 _dashDirection;
        private Vector3 _targetDestination;

        private InputControls _controls;
        private UiManager _uiManager;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _agent = GetComponent<NavMeshAgent>();
            _controls = new InputControls();
            
            _controls.Play.UseAbility.performed += context =>
            {
                if (charge < 10 || !unlocked) return;
                StartCoroutine(Dash());
                charge = 0;
                _uiManager.UpdateThruster(charge);
            };
        }

        private void Start()
        {
            Charging(0);
        }

        private void OnEnable()
        {
            _controls.Enable();
        }

        private void OnDisable()
        {
            _controls.Disable();
        }

        private IEnumerator Dash()
        {
            _targetDestination = _agent.destination; 
            _agent.isStopped = true;               
            _agent.updatePosition = false;          
            _agent.updateRotation = false;
            
            _dashDirection = transform.forward;

            var elapsed = 0f;

            while (elapsed < dashDuration)
            {
                var newPos = transform.position + _dashDirection * (dashForce * Time.deltaTime);
                _rb.MovePosition(newPos);
                _agent.Warp(newPos);  
                elapsed += Time.deltaTime;
                yield return null;
            }
            
            _agent.updatePosition = true;
            _agent.updateRotation = true;
            _agent.isStopped = false;
            
            _agent.SetDestination(_targetDestination);
        }

        public void Charging(float val)
        {
            charge += val;
            charge = Mathf.Clamp(charge, 0, 10);
            _uiManager.UpdateThruster(charge);
        }

        public void SetUiManager(UiManager manager)
        {
            _uiManager = manager;
        }
    }
}
