using UnityEngine;

namespace CoreMechanics.EventScripts
{
    public class 铁炬动乱 : BaseDevelopmentalEvent
    {
        protected override void Pickup()
        {
            LaunchBomb(15, 3);
            base.Pickup();
        }
    }
}