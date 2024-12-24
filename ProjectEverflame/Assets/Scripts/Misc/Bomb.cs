using System;
using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;

namespace Misc
{
    public class Bomb : MonoBehaviour
    {
        public GameObject circle;
        public ParticleSystem vfx1;
        public ParticleSystem vfx2;
        private Collider _collider;
        private bool _isPlayed;
        public BombGenerator generator;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _collider.enabled = false;
        }

        private void OnDisable()
        {
            circle.transform.localScale = Vector3.one;
            _isPlayed = false;
            _collider.enabled = false;
        }

        private void Update()
        {
            if (_isPlayed && vfx1.isStopped && vfx2.isStopped) generator.ReturnToPool(gameObject);
            
            if (_collider.enabled) return;
            if (circle.transform.localScale.x < 0)
            {
                _collider.enabled = true;
                vfx1.Play();
                vfx2.Play();
                _isPlayed = true;
            }
            else
            {
                circle.transform.localScale -= Vector3.one * (Time.deltaTime * 0.5f);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            other.gameObject.GetComponent<PlayerController>().StunPlayer(2);
        }
    }
}
