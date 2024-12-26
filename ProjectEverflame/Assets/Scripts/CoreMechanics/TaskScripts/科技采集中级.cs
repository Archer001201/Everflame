using System;
using System.Collections;
using Misc;
using UnityEngine;
using EventHandler = Utilities.EventHandler;

namespace CoreMechanics.TaskScripts
{
    public sealed class 科技采集中级 : BaseTask
    {
        private RainManager _rainManager;

        private void Start()
        {
            Rain(3, 30);
        }

        private void OnEnable()
        {
            goal = 15;
            EventHandler.onCollectScienceResource += UpdateAmount;
        }

        private void OnDisable()
        {
            EventHandler.onCollectScienceResource -= UpdateAmount;
            if (amount < goal) Punish();
        }
        
        protected override void Reward()
        {
            base.Reward();
            // gameManager.HandleProsperity(25, true);
            ChangePlayerSpeed(1.5f, 20f);
            // Debug.Log("get reward");
            gameManager.HandleProsperity(15, true);
            // gameManager.HandleScienceExp(gameManager.scienceExp*0.2f, true);
        }

        protected override void Punish()
        {
            base.Punish();
            // gameManager.HandleNatureExp(gameManager.natureExp * 0.3f, false);
            // gameManager.HandleScienceExp(gameManager.scienceExp * 0.25f, false);
            ChangePlayerSpeed(0.5f, 10f);
            StartDestroyResources(ResourceType.科技, 10);
            // Debug.Log("get punish");
        }
    }
}
