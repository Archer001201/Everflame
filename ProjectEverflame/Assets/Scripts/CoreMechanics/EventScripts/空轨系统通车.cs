using System.Collections.Generic;
using UnityEngine;

namespace CoreMechanics.EventScripts
{
    public class 空轨系统通车 : BaseDevelopmentalEvent
    {
        private List<ResourceType> types = new List<ResourceType>();
        protected override void Pickup()
        {
            types.Add(ResourceType.科技);
            types.Add(ResourceType.自然);
            ActivateMagnet(types, 10f);
            ChangePlayerSpeed(1.5f, 10);
            base.Pickup();
        }
    }
}