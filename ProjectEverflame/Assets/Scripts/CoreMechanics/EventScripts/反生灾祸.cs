using Ui;
using UnityEngine;

namespace CoreMechanics.EventScripts
{
    public class 反生灾祸 : BaseDevelopmentalEvent
    {
        private const float Duration = 30;
        
        protected override void Pickup()
        {
            StartTask(TaskType.自然采集中级,Duration);
            base.Pickup();
        }
    }
}