using UnityEngine;

namespace CoreMechanics.EventScripts
{
    public class 烛林秘光 : BaseDevelopmentalEvent
    {
        protected override void Pickup()
        {
            gameManager.HandleNatureExp(gameManager.natureExp * 0.30f, true);
            gameManager.HandleScienceExp(gameManager.scienceExp * 0.10f, true);
            base.Pickup();
        }
    }
}