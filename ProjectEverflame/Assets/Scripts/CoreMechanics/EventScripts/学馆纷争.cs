using UnityEngine;

namespace CoreMechanics.EventScripts
{
    public class 学馆纷争 : BaseDevelopmentalEvent
    {
        protected override void Pickup()
        {
            gameManager.HandleScienceExp(gameManager.scienceExp * 0.30f, false);
            StartDestroyResources(ResourceType.科技, 10);
            base.Pickup();
        }
    }
}