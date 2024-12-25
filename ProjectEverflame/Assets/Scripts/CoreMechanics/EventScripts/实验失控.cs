using UnityEngine;

namespace CoreMechanics.EventScripts
{
    public class 实验失控 : BaseDevelopmentalEvent
    {
        protected override void Pickup()
        {
            ChangePlayerSpeed(0.5f, 10);
            ReverseExp(ResourceType.自然, 10);
            // gameManager.HandleNatureExp(gameManager.natureExp * 0.3f, false);
            gameManager.HandleScienceExp(gameManager.scienceExp * 0.3f, false);
            base.Pickup();
        }
    }
}