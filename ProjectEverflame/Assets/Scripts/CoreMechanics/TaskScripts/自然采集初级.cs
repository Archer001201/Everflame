using System;
using System.Collections;
using UnityEngine;
using EventHandler = Utilities.EventHandler;

namespace CoreMechanics.TaskScripts
{
    public sealed class 自然采集初级 : BaseTask
    {
        private void OnEnable()
        {
            goal = 5;
            EventHandler.onCollectNatureResource += UpdateAmount;
        }

        private void OnDisable()
        {
            EventHandler.onCollectNatureResource -= UpdateAmount;
            if (amount < goal) Punish();
        }
        
        protected override void Reward()
        {
            gameManager.HandleProsperity(15, true);
            ChangePlayerSpeed(1.5f, 10f);
            // Debug.Log("get reward");
        }

        protected override void Punish()
        {
            gameManager.HandleNatureExp(gameManager.natureExp * 0.1f, false);
            gameManager.HandleScienceExp(gameManager.scienceExp * 0.1f, false);
            ChangePlayerSpeed(0.5f, 10f);
            // Debug.Log("get punish");
        }
    }
}
