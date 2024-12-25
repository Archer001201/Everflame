using Ui;
using UnityEngine;

namespace CoreMechanics.EventScripts
{
    public class 阴雨下的星辰 : BaseDevelopmentalEvent
    {
        private const float Duration = 20;
        
        protected override void Pickup()
        {
            StartTask(TaskType.出圈中级,Duration);
            base.Pickup();
        }
    }
}