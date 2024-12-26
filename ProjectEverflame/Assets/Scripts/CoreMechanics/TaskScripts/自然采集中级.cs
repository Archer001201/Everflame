using System;
using System.Collections;
using Player;
using UnityEngine;
using EventHandler = Utilities.EventHandler;

namespace CoreMechanics.TaskScripts
{
    public sealed class 自然采集中级 : BaseTask
    {
        private void OnEnable()
        {
            goal = 10;
            EventHandler.onCollectNatureResource += UpdateAmount;
            Bomb(30, 2);
        }

        private void OnDisable()
        {
            EventHandler.onCollectNatureResource -= UpdateAmount;
            if (amount < goal) Punish();
        }
        
        protected override void Reward()
        {
            base.Reward();
            gameManager.HandleProsperity(25, true);
            AddThruster(1);
            // ChangePlayerSpeed(1.5f, 10f);
            // Debug.Log("get reward");
            // gameManager.HandleNatureExp(gameManager.natureExp*0.2f, true);
            // gameManager.HandleScienceExp(gameManager.scienceExp*0.2f, true);
        }

        protected override void Punish()
        {
            base.Punish();
            // gameManager.HandleNatureExp(gameManager.natureExp * 0.25f, false);
            // gameManager.HandleScienceExp(gameManager.scienceExp * 0.25f, false);
            ChangePlayerSpeed(0.5f, 10f);
            ClearThruster();
            // Debug.Log("get punish");
        }
    }
}
