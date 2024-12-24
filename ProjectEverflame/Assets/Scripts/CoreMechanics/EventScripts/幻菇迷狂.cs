using UnityEngine;

namespace CoreMechanics.EventScripts
{
    public class 幻菇迷狂 : BaseDevelopmentalEvent
    {
        protected override void Pickup()
        {
            gameManager.HandleNatureExp(gameManager.natureExp * 0.20f, false);
            ChangePlayerSpeed(0.5f, 10f);
            base.Pickup();
        }
    }
}