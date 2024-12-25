using System;
using UnityEngine;
using EventHandler = Utilities.EventHandler;

namespace Misc
{
    public class Circle : MonoBehaviour
    {
        private Collider _collider;
        private MeshRenderer _meshRenderer;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            EventHandler.Escape(1);
        }

        public void EnableCollider(bool enable)
        {
            _collider.enabled = enable;
            _meshRenderer.enabled = enable;
        }
    }
}
