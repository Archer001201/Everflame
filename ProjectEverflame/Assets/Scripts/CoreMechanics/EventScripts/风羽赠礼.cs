using UnityEngine;

namespace CoreMechanics.EventScripts
{
    public class 风羽赠礼 : BaseDevelopmentalEvent
    {
        protected override void Pickup()
        {
            // gameManager.HandleNatureExp(gameManager.natureExp * 0.20f, false);
            ChangePlayerSpeed(1.5f, 10f);
            base.Pickup();
        }
    }
}