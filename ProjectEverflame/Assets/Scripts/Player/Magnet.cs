using System;
using System.Collections;
using System.Collections.Generic;
using CoreMechanics;
using UnityEngine;

namespace Player
{
    public class Magnet : MonoBehaviour
    {
        public List<ResourceType> resourceTypes = new List<ResourceType>();
        public float timer;
        private Collider _collider;
        private Coroutine _coroutine;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            // _collider.enabled = false;
        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.CompareTag("Resource")) return;
            var r = other.GetComponent<DevelopmentalResource>();
            if (r != null && resourceTypes.Contains(r.resourceType))
            {
                // Debug.Log("Magnet triggered");
                r.MoveToPlayer(gameObject);
            }
        }

        private bool isMagnetActive = false; // 控制协程状态

        public void StartMagnet(List<ResourceType> types, float time)
        {
            // 如果协程已经运行，增加时间和类型，不重新启动
            if (isMagnetActive)
            {
                timer += time;
                foreach (var t in types)
                {
                    if (!resourceTypes.Contains(t)) resourceTypes.Add(t);
                }
            }
            else
            {
                // 启动新协程
                _coroutine = StartCoroutine(ActivateMagnet(types, time));
            }
        }

        public IEnumerator ActivateMagnet(List<ResourceType> types, float time)
        {
            // 初始化状态
            isMagnetActive = true;
            timer = time;

            foreach (var t in types)
            {
                if (!resourceTypes.Contains(t)) resourceTypes.Add(t);
            }
            _collider.enabled = true;

            // 动态等待逻辑
            while (timer > 0)
            {
                yield return null; // 每帧检测
                timer -= Time.deltaTime;
            }

            // 完成后重置状态
            _collider.enabled = false;
            resourceTypes.Clear();
            isMagnetActive = false;
            _coroutine = null;
        }
    }
}
