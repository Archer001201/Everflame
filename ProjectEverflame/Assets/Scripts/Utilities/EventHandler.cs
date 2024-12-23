using System;
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
    }
}
