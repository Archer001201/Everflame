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
            goal = 3;
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
            Debug.Log("get reward");
        }

        protected override void Punish()
        {
            gameManager.HandleProsperity(15, false);
            Debug.Log("get punish");
        }
    }
}
