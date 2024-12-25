using System;
using System.Collections.Generic;
using UnityEngine;

namespace Misc
{
    [Serializable]
    public class Materials
    {
        public List<Material> materials;

        public void AddMaterial(Material[] mat)
        {
            materials = new List<Material>();
            foreach (var m in mat)
            {
                materials.Add(m);
            }
        }
    }
    
    public class TransparentHandler : MonoBehaviour
    {
        public Material transparentMaterial;
        public MeshRenderer[] renderers;
        public Materials[] originalMaterials;
        private Material[][] transparentMaterials; // 用于存储透明材质数组

        private void Awake()
        {
            renderers = GetComponentsInChildren<MeshRenderer>();
            originalMaterials = new Materials[renderers.Length];
            transparentMaterials = new Material[renderers.Length][]; // 初始化透明材质数组
        }

        private void Start()
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                if (renderers[i].materials == null) continue;

                // 保存原始材质
                originalMaterials[i] = new Materials();
                originalMaterials[i].AddMaterial(renderers[i].materials);

                // 创建透明材质数组
                var mats = renderers[i].materials;
                transparentMaterials[i] = new Material[mats.Length]; // 初始化透明数组
                for (int j = 0; j < mats.Length; j++)
                {
                    transparentMaterials[i][j] = transparentMaterial; // 全部设置为透明材质
                }
            }

            gameObject.layer = LayerMask.NameToLayer("Environment");
        }

        public void SwitchToTransparentMaterial(bool val)
        {
            // Debug.Log("Switched to transparent material");

            for (var i = 0; i < renderers.Length; i++)
            {
                var r = renderers[i];

                if (val)
                {
                    // 直接使用预先准备好的透明材质数组
                    r.materials = transparentMaterials[i];
                }
                else
                {
                    // 直接使用原始材质数组
                    r.materials = originalMaterials[i].materials.ToArray();
                }
            }
        }
    }
}

