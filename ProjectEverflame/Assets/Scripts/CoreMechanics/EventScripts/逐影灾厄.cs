using UnityEngine;

namespace CoreMechanics.EventScripts
{
    public class 逐影灾厄 : BaseDevelopmentalEvent
    {
        protected override void Pickup()
        {
            // gameManager.HandleScienceExp(gameManager.scienceExp * 0.15f, false);
            StartDestroyResources(ResourceType.科技, 8);
            base.Pickup();
        }
    }
}