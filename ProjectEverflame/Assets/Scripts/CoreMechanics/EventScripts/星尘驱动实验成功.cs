using UnityEngine;

namespace CoreMechanics.EventScripts
{
    public class 星尘驱动实验成功 : BaseDevelopmentalEvent
    {
        protected override void Pickup()
        {
            AddThruster(2);
            base.Pickup();
        }
    }
}