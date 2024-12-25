using Ui;
using UnityEngine;

namespace CoreMechanics.EventScripts
{
    public class 火中泣魂 : BaseDevelopmentalEvent
    {
        private const float Duration = 30;
        
        protected override void Pickup()
        {
            StartTask(TaskType.追逐珍宝中级,Duration);
            base.Pickup();
        }
    }
}