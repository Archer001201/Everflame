using UnityEngine;

namespace CoreMechanics.EventScripts
{
    public class 奇妙微生物 : BaseDevelopmentalEvent
    {
        protected override void Pickup()
        {
            gameManager.HandleNatureExp(gameManager.natureExp * 0.40f, true);
            // ChangePlayerSpeed(1.5f, 10f);
            base.Pickup();
        }
    }
}