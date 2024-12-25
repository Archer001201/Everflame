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
    }
}
