using System;
using System.Collections;
using Player;
using Ui;
using UnityEngine;
using Utilities;

namespace CoreMechanics.TaskScripts
{
    public abstract class BaseTask : MonoBehaviour
    {
        protected GameManager gameManager;
        protected float goal;
        protected float amount;
        protected TaskPanel taskPanel;

        private void Awake()
        {
            gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
            taskPanel = GetComponent<TaskPanel>();
        }
        
        public virtual void UpdateAmount(float val)
        {
            amount += val;
            taskPanel.UpdateTaskProgress(amount/goal);
            if (amount >= goal)
            {
                Reward();
                Destroy(gameObject);
            }
        }
        
        protected void ChangePlayerSpeed(float rate, float time)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().StartChangeSpeed(rate, time);
        }
        
        protected void StartDestroyResources(ResourceType type, float time)
        {
            Utilities.EventHandler.DestroyResource(type, time);
        }

        protected abstract void Reward();
        protected abstract void Punish();
    }
}
