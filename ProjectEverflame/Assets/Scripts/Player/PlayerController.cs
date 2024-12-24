using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Ui;
using UnityEngine;
using UnityEngine.AI;
using Debug = UnityEngine.Debug;

namespace Player
{
    public enum Ability
    {
        Thruster, Teleport
    }
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerController : MonoBehaviour
    {
        public LayerMask groundLayer;
        public LayerMask eventLayer;
        public Animator animator;
        public bool isStunned;
        private NavMeshAgent _agent;
        private Camera _camera;
        private UiManager _uiManager;
        // public Ability currentAbility;
        public List<Ability> abilities;
        public int index;
        public Thruster thruster;
        public Teleport teleport;
        private InputControls _input;

        private void Awake()
        {
            _camera = Camera.main;
            _agent = GetComponent<NavMeshAgent>();
            _input = new InputControls();
            _input.Play.SwitchAbility.performed += context => SwitchAbility(context.ReadValue<Vector2>());
        }

        private void OnEnable()
        {
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();
        }

        private void Update()
        {
            float speed = _agent.velocity.magnitude;
            // Debug.Log(speed);
            animator.SetFloat("MoveSpeed", speed);

            if (isStunned) return;
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

        /// <summary>
        /// 让玩家进入眩晕状态，无法移动
        /// </summary>
        /// <param name="duration">眩晕持续时间（秒）</param>
        public void StunPlayer(float duration)
        {
            // 防止重复眩晕
            if (isStunned) return;

            StartCoroutine(StunCoroutine(duration));
        }

        private IEnumerator StunCoroutine(float duration)
        {
            // 标记眩晕状态
            isStunned = true;

            // 禁用NavMeshAgent移动功能
            _agent.isStopped = true;
            _agent.velocity = Vector3.zero; // 确保停止移动
            // animator.SetBool("IsStunned", true); // 播放眩晕动画（需要动画控制器支持）

            // 等待持续时间
            yield return new WaitForSeconds(duration);

            // 恢复移动能力
            _agent.isStopped = false;
            // animator.SetBool("IsStunned", false); // 退出眩晕动画

            isStunned = false;
        }

        private void SwitchAbility(Vector2 vec2)
        {
            switch (vec2.y)
            {
                case > 0:
                    index++;
                    // if (index >= abilities.Count) index = 0;
                    break;
                case < 0:
                    index--;
                    // if (index < 0) index = abilities.Count - 1;
                    break;
            }

            index = Mathf.Clamp(index, 0, abilities.Count - 1);
            switch (abilities[index])
            {
                case Ability.Teleport:
                    teleport.enabled = true;
                    thruster.enabled = false;
                    break;
                case Ability.Thruster:
                    teleport.enabled = false;
                    thruster.enabled = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
