using Ui;
using UnityEngine;

namespace CoreMechanics.EventScripts
{
    public class 追风竞赛 : BaseDevelopmentalEvent
    {
        private const float Duration = 60;
        
        protected override void Pickup()
        {
            StartTask(TaskType.追逐珍宝高级,Duration);
            base.Pickup();
        }
    }
}