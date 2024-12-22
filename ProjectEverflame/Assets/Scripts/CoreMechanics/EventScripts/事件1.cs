using UnityEngine;

namespace CoreMechanics.EventScripts
{
    public class 事件1 : BaseDevelopmentalEvent
    {
        protected override void Pickup()
        {
            base.Pickup();
            Debug.Log("事件1");
        }
    }
}