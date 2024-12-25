using UnityEngine;

namespace CoreMechanics.EventScripts
{
    public class 蒸汽猎鹰 : BaseDevelopmentalEvent
    {
        protected override void Pickup()
        {
            gameManager.HandleScienceExp(gameManager.scienceExp * 0.40f, true);
            // ChangePlayerSpeed(1.5f, 10f);
            base.Pickup();
        }
    }
}