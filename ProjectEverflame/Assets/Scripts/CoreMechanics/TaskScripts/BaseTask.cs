using System;
using System.Collections;
using UnityEngine;
using Utilities;

namespace CoreMechanics.TaskScripts
{
    public abstract class BaseTask : MonoBehaviour
    {
        protected GameManager gameManager;
        protected float goal;
        protected float amount;

        private void Awake()
        {
            gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        }
        
        public virtual void UpdateAmount(float val)
        {
            amount += val;
            if (amount >= goal)
            {
                Reward();
                Destroy(gameObject);
            }
        }

        protected abstract void Reward();
        protected abstract void Punish();
    }
}
