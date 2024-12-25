using System;
using CoreMechanics;
using Unity.VisualScripting;
using UnityEngine;

namespace Utilities
{
    public static class EventHandler
    {
        public static Action<float> onCollectNatureResource;

        public static void CollectNatureResource(float amount)
        {
            onCollectNatureResource?.Invoke(amount);
        }
        
        public static Action<ResourceType, float> onDestroyResource;

        public static void DestroyResource(ResourceType type, float time)
        {
            onDestroyResource?.Invoke(type, time);
        }
        
        public static Action<int> onUpdateEnvironment;

        public static void UpdateEnvironment(int level)
        {
            onUpdateEnvironment?.Invoke(level);
        }
        
        public static Action<float> onCollectScienceResource;

        public static void CollectScienceResource(float amount)
        {
            onCollectScienceResource?.Invoke(amount);
        }

        public static Action<float> onEscape;

        public static void Escape(float val)
        {
            onEscape?.Invoke(val);
        }
    }
}
