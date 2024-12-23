using Ui;
using UnityEngine;

namespace CoreMechanics.EventScripts
{
    public class 流火珍宝 : BaseDevelopmentalEvent
    {
        private const float Duration = 20;
        
        protected override void Pickup()
        {
            StartTask(TaskType.追逐珍宝初级,Duration);
            base.Pickup();
        }
    }
}