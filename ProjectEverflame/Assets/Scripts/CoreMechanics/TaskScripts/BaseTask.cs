using System;
using System.Collections;
using Misc;
using Player;
using Ui;
using UnityEngine;
using Utilities;
using EventHandler = Utilities.EventHandler;

namespace CoreMechanics.TaskScripts
{
    public abstract class BaseTask : MonoBehaviour
    {
        protected GameManager gameManager;
        protected float goal;
        protected float amount;
        protected TaskPanel taskPanel;
        protected bool isDone;
        private AudioClip _succeedClip;
        private AudioClip _failedClip;

        private void Awake()
        {
            gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
            taskPanel = GetComponent<TaskPanel>();
            _succeedClip = Resources.Load<AudioClip>("Arts/Audios/完成任务");
            _failedClip = Resources.Load<AudioClip>("Arts/Audios/资源球");
        }
        
        public virtual void UpdateAmount(float val)
        {
            if (isDone) return;
            amount += val;
            taskPanel.UpdateTaskProgress(amount/goal);
            if (amount >= goal)
            {
                isDone = true;
                Reward();
                Destroy(gameObject);
            }
        }
        
        protected void ChangePlayerSpeed(float rate, float time)
        {
            var p = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            if (p) p.StartChangeSpeed(rate, time);
        }
        
        protected void StartDestroyResources(ResourceType type, float time)
        {
            Utilities.EventHandler.DestroyResource(type, time);
        }

        protected virtual void Reward()
        {
            EventHandler.PlayAudio(_succeedClip);
        }

        protected virtual void Punish()
        {
            EventHandler.PlayAudio(_failedClip);
        }

        protected void Bomb(float time, int wave)
        {
            GameObject.FindWithTag("BombGenerator").gameObject.GetComponent<BombGenerator>().StartSpawn(time, wave);
        }

        protected void Rain(int wave, float time)
        {
            GameObject.FindWithTag("RainManager").GetComponent<RainManager>().StartRain(wave,time);
        }

        protected void AddThruster(int val)
        {
            GameObject.FindWithTag("Player").GetComponent<Thruster>().Charging(val);
        }
        
        protected void AddTeleport(int val)
        {
            GameObject.FindWithTag("Player").GetComponent<Teleport>().Charging(val);
        }

        protected void ClearThruster()
        {
            var t = GameObject.FindWithTag("Player").GetComponent<Thruster>();
            t.Charging(t.charge * -1);
        }
        
        protected void ClearTeleport()
        {
            var t = GameObject.FindWithTag("Player").GetComponent<Teleport>();
            t.Charging(t.charge * -1);
        }
        
        protected void ReverseExp(ResourceType type, float time)
        {
            gameManager.StartReverse(type, time);
        }
    }
}
