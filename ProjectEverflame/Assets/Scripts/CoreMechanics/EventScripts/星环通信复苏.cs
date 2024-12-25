using UnityEngine;

namespace CoreMechanics.EventScripts
{
    public class 星环通信复苏 : BaseDevelopmentalEvent
    {
        protected override void Pickup()
        {
            AddTeleport(1);
            base.Pickup();
        }
    }
}