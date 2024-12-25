using Ui;
using UnityEngine;

namespace CoreMechanics.EventScripts
{
    public class 喧器白雾 : BaseDevelopmentalEvent
    {
        private const float Duration = 30;
        
        protected override void Pickup()
        {
            StartTask(TaskType.科技采集中级,Duration);
            base.Pickup();
        }
    }
}