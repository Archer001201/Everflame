using System;
using System.Collections;
using Misc;
using UnityEngine;
using EventHandler = Utilities.EventHandler;

namespace CoreMechanics.TaskScripts
{
    public sealed class 科技不采集 : BaseTask
    {
        // private RainManager _rainManager;

        private void Start()
        {
            Bomb(3, 30);
            Rain(3, 30);
        }

        private void OnEnable()
        {
            goal = 1;
            EventHandler.onCollectScienceResource += UpdateAmount;
        }

        private void OnDisable()
        {
            EventHandler.onCollectScienceResource -= UpdateAmount;
            if (amount < goal) Punish();
        }
        
        protected override void Reward()
        {
            ClearTeleport();
            // gameManager.HandleProsperity(25, true);
            // ChangePlayerSpeed(1.5f, 20f);
            // Debug.Log("get reward");
            // gameManager.HandleProsperity(40, true);
            // AddTeleport(1);
            // gameManager.HandleScienceExp(gameManager.scienceExp*0.2f, true);
        }

        protected override void Punish()
        {
            // gameManager.HandleNatureExp(gameManager.natureExp * 0.3f, false);
            // gameManager.HandleScienceExp(gameManager.scienceExp * 0.25f, false);
            // ChangePlayerSpeed(0.5f, 10f);
            // ReverseExp(ResourceType.科技, 10);
            // gameManager.HandleScienceExp();
            // StartDestroyResources(ResourceType.科技, 10);
            // Debug.Log("get punish");
            gameManager.HandleProsperity(40, true);
            AddTeleport(1);
        }
    }
}
