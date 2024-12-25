using Ui;
using UnityEngine;

namespace CoreMechanics.EventScripts
{
    public class 荒芜之源 : BaseDevelopmentalEvent
    {
        private const float Duration = 30;
        
        protected override void Pickup()
        {
            StartTask(TaskType.科技不采集,Duration);
            base.Pickup();
        }
    }
}