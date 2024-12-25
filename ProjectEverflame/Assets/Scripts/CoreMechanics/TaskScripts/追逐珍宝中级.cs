using System;
using System.Collections.Generic;
using Misc;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CoreMechanics.TaskScripts
{
    public sealed class 追逐珍宝中级 : BaseTask
    {
        public GameObject prefab;
        public List<Runner> runners = new List<Runner>();

        private void OnEnable()
        {
            goal = 1;
        }

        private void OnDisable()
        {
            if (amount < goal)
            {
                Punish();
                foreach (var r in runners)
                {
                    if (r != null) Destroy(r.gameObject);
                }
            }
        }

        private void Start()
        {
            var origin = new Vector3(0, 0, 0);
            var radius = 20f;
            prefab = Resources.Load<GameObject>("Prefabs/珍宝");

            if (prefab != null)
            {
                for (var i = 0; i < goal; i++)
                {
                    // 在原点附近的随机位置生成实例
                    Vector3 randomPosition = origin + Random.insideUnitSphere * radius;

                    // 保持在地面上，即Y轴为0
                    randomPosition.y = 0;

                    // 实例化对象
                    var obj = Instantiate(prefab, randomPosition, Quaternion.identity);
                    var runner = obj.GetComponent<Runner>();
                    runner.task = this;
                    runners.Add(runner);
                }
            }
        }

        protected override void Reward()
        {
            gameManager.HandleProsperity(15, true);
            gameManager.HandleNatureExp(gameManager.natureExp * 0.3f, true);
            // Debug.Log("get reward");
        }

        protected override void Punish()
        {
            // gameManager.HandleScienceExp(gameManager.scienceExp * 0.3f, false);
            StartDestroyResources(ResourceType.科技, 10);
            ChangePlayerSpeed(0.5f, 10f);
            // Debug.Log("get punish");
        }

        public override void UpdateAmount(float val)
        {
            amount++;
            if (amount >= goal)
            {
                Reward();
                foreach (var r in runners)
                {
                    if(r) Destroy(r.gameObject);
                }
                Destroy(gameObject);
            }
        }
    }
}
