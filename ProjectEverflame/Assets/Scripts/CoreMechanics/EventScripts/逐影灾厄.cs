using UnityEngine;

namespace CoreMechanics.EventScripts
{
    public class 逐影灾厄 : BaseDevelopmentalEvent
    {
        protected override void Pickup()
        {
            gameManager.HandleNatureExp(gameManager.natureExp * 0.30f, false);
            StartDestroyResources(ResourceType.科技, 10);
            base.Pickup();
        }
    }
}