using Ui;
using UnityEngine;

namespace CoreMechanics.EventScripts
{
    public class 夙愿之痕 : BaseDevelopmentalEvent
    {
        private const float Duration = 30;
        
        protected override void Pickup()
        {
            StartTask(TaskType.追逐珍宝初级,Duration);
            base.Pickup();
        }
    }
}