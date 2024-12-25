using Ui;
using UnityEngine;

namespace CoreMechanics.EventScripts
{
    public class 觉醒之翼 : BaseDevelopmentalEvent
    {
        private const float Duration = 30;
        
        protected override void Pickup()
        {
            StartTask(TaskType.科技采集高级,Duration);
            base.Pickup();
        }
    }
}