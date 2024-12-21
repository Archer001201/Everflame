using System;
using System.Collections.Generic;
using UnityEngine;

namespace Misc
{
    public class TransparentHandler : MonoBehaviour
    {
        public Material transparentMaterial;
        public MeshRenderer[] renderers;
        public List<Material> originalMaterials;

        private void Awake()
        {
            renderers = GetComponentsInChildren<MeshRenderer>();

            foreach (var r in renderers)
            {
                originalMaterials.Add(r.material);
            }

            gameObject.layer = LayerMask.NameToLayer("Environment");
        }

        public void SwitchToTransparentMaterial(bool val)
        {
            if (val)
            {
                foreach (var r in renderers)
                {
                    r.material = transparentMaterial;
                }
            }
            else
            {
                for (var i = 0; i < renderers.Length; i++)
                {
                    renderers[i].material = originalMaterials[i];
                }
            }
        }
    }
}
