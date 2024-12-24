using UnityEngine;

namespace CoreMechanics.EventScripts
{
    public class 蒸汽工潮 : BaseDevelopmentalEvent
    {
        protected override void Pickup()
        {
            StartDestroyResources(ResourceType.科技, 10);
            LaunchBomb(15, 3);
            base.Pickup();
        }
    }
}