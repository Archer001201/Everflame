using UnityEngine;

namespace CoreMechanics.EventScripts
{
    public class 能源矩阵 : BaseDevelopmentalEvent
    {
        protected override void Pickup()
        {
            gameManager.HandleNatureExp(gameManager.natureExp * 0.20f, true);
            gameManager.HandleScienceExp(gameManager.scienceExp * 0.30f, true);
            // ChangePlayerSpeed(1.5f, 10f);
            base.Pickup();
        }
    }
}