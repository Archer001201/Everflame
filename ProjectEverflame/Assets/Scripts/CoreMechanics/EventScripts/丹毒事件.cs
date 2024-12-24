using UnityEngine;

namespace CoreMechanics.EventScripts
{
    public class 丹毒事件 : BaseDevelopmentalEvent
    {
        protected override void Pickup()
        {
            ReverseExp(ResourceType.自然, 10);
            base.Pickup();
        }
    }
}