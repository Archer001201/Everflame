using System;
using System.Collections;
using UnityEngine;

namespace Misc
{
    public class Dust : MonoBehaviour
    {
        public ParticleSystem dustParticles;
        public GameObject model;

        private void Awake()
        {
            model.SetActive(false);
        }

        private void OnEnable()
        {
            StartCoroutine(PlayDust(true));
        }

        // private void OnDisable()
        // {
        //     StartCoroutine(PlayDust(false));
        // }

        private IEnumerator PlayDust(bool activateModel)
        {
            dustParticles.Play(true);
            yield return new WaitForSeconds(dustParticles.main.duration/2f);
            model.SetActive(activateModel);
        }
    }
}
