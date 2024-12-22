using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Player
{
    public class Teleport : MonoBehaviour
    {
        public GameObject portalPrefab;         // 传送门预制体
        public int maxPortals = 2;              // 最大传送门数量
        public int charge = 20;
        
        private Queue<GameObject> _portalPool;  // 传送门对象池
        private InputControls _controls;
        private UiManager _uiManager;

        private void Awake()
        {
            // 初始化输入控制
            _controls = new InputControls();
            _controls.Play.UseAbility.performed += context => PlacePortal();

            // 初始化对象池
            _portalPool = new Queue<GameObject>();
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

        // 放置传送门
        private void PlacePortal()
        {
            if (charge < 10) return;
            charge -= 10;
            _uiManager.UpdatePortal(charge);
            
            // 计算位置和方向
            Vector3 spawnPosition = new Vector3(
                transform.position.x + transform.forward.x * 2f, // X 轴按前方向量偏移
                0.3f,                                             // Y 轴固定为 0
                transform.position.z + transform.forward.z * 2f // Z 轴按前方向量偏移
            ); // 在物体前方2米生成
            Quaternion spawnRotation = Quaternion.LookRotation(-transform.forward); // 面向背后的方向

            // 检查对象池数量
            if (_portalPool.Count >= maxPortals)
            {
                // 删除最早的传送门
                GameObject oldestPortal = _portalPool.Dequeue();
                Destroy(oldestPortal); // 释放旧传送门资源
            }

            // 创建新传送门并加入队列
            GameObject newPortal = Instantiate(portalPrefab, spawnPosition, spawnRotation);
            _portalPool.Enqueue(newPortal);
        }

        public void SetUiManager(UiManager uiManager)
        {
            _uiManager = uiManager;
        }

        public void Charging(int val)
        {
            charge += val;
            charge = Mathf.Clamp(charge, 0, 20);
            _uiManager.UpdatePortal(charge);
        }
    }
}
