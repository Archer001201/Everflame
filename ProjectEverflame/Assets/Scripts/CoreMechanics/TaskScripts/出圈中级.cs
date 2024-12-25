using System;
using System.Collections;
using Misc;
using UnityEngine;
using EventHandler = Utilities.EventHandler;

namespace CoreMechanics.TaskScripts
{
    public sealed class 出圈中级 : BaseTask
    {
        private Circle _circle;
        private void Start()
        {
            Rain(3, 20);
            Bomb(3, 20);
        }

        private void OnEnable()
        {
            goal = 1;
            _circle = GameObject.FindWithTag("Circle").GetComponent<Circle>();
            _circle.EnableCollider(true);
            EventHandler.onEscape += UpdateAmount;
        }

        private void OnDisable()
        {
            _circle.EnableCollider(false);
            EventHandler.onEscape -= UpdateAmount;
            if (amount < goal) Punish();
        }
        
        protected override void Reward()
        {
            // gameManager.HandleProsperity(25, true);
            // ChangePlayerSpeed(1.5f, 20f);
            // Debug.Log("get reward");
            gameManager.HandleProsperity(20, true);
            AddThruster(2);
            // gameManager.HandleScienceExp(gameManager.scienceExp*0.2f, true);
        }

        protected override void Punish()
        {
            gameManager.HandleNatureExp(gameManager.natureExp * 0.25f, false);
            gameManager.HandleScienceExp(gameManager.scienceExp * 0.25f, false);
            // ChangePlayerSpeed(0.5f, 10f);
            // StartDestroyResources(ResourceType.科技, 10);
            // Debug.Log("get punish");
            ChangePlayerSpeed(0.5f, 10f);
        }
    }
}
