using UnityEngine;

namespace CoreMechanics.EventScripts
{
    public class 星果盛宴 : BaseDevelopmentalEvent
    {
        protected override void Pickup()
        {
            gameManager.HandleNatureExp(gameManager.natureExp * 0.30f, true);
            // ChangePlayerSpeed(1.5f, 10f);
            base.Pickup();
        }
    }
}