using System;
using System.Collections;
using UnityEngine;
using EventHandler = Utilities.EventHandler;

namespace Misc
{
    public class Environment : MonoBehaviour
    {
        public ParticleSystem dustParticles;
        public GameObject model;
        public Vector2 range = Vector2.one;
        public bool isActive;
        public bool isTest;

        private void Awake()
        {
            model.SetActive(false);
        }

        private void OnEnable()
        {
            if (isTest) StartCoroutine(Activate(true));
            EventHandler.onUpdateEnvironment += UpdateState;
        }

        private void OnDisable()
        {
            EventHandler.onUpdateEnvironment -= UpdateState;
        }

        private IEnumerator Activate(bool activateModel)
        {
            dustParticles.Play(true);
            yield return new WaitForSeconds(dustParticles.main.duration/2f);
            isActive = activateModel;
            model.SetActive(isActive);
        }

        private void UpdateState(int level)
        {
            if (isTest) return;
            if (range.x <= level && level <= range.y)
            {
                if (!isActive) StartCoroutine(Activate(true));
            }
            else
            {
                if (isActive) StartCoroutine(Activate(false));
            }
        }
    }
}
