using UnityEngine;

namespace CoreMechanics.EventScripts
{
    public class 合金 : BaseDevelopmentalEvent
    {
        protected override void Pickup()
        {
            AddThruster(1);
            base.Pickup();
        }
    }
}