using UnityEngine;

namespace CoreMechanics.EventScripts
{
    public class 逐影灾厄 : BaseDevelopmentalEvent
    {
        protected override void Pickup()
        {
            gameManager.HandleScienceExp(gameManager.scienceExp * 0.15f, false);
            ChangePlayerSpeed(0.5f, 10f);
            base.Pickup();
        }
    }
}