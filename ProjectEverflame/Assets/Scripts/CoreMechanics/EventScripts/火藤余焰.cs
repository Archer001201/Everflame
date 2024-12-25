using UnityEngine;

namespace CoreMechanics.EventScripts
{
    public class 火藤余焰 : BaseDevelopmentalEvent
    {
        protected override void Pickup()
        {
            AddThruster(1);
            base.Pickup();
        }
    }
}