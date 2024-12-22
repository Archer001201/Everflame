using UnityEngine;

namespace CoreMechanics.EventScripts
{
    public class 事件2 : BaseDevelopmentalEvent
    {
        protected override void Pickup()
        {
            base.Pickup();
            Debug.Log("事件2");
        }
    }
}