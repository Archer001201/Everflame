using UnityEngine;

namespace CoreMechanics.EventScripts
{
    public class 污染之乱 : BaseDevelopmentalEvent
    {
        protected override void Pickup()
        {
            ChangePlayerSpeed(0.5f, 10);
            ReverseExp(ResourceType.自然, 10);
            gameManager.HandleNatureExp(gameManager.natureExp * 0.1f, false);
            gameManager.HandleScienceExp(gameManager.scienceExp * 0.1f, false);
            base.Pickup();
        }
    }
}