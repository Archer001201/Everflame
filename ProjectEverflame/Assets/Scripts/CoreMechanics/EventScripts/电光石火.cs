using System.Collections;
using Ui;
using UnityEngine;

namespace CoreMechanics.EventScripts
{
    public class 电光石火 : BaseDevelopmentalEvent
    {
        private const float Duration = 30;

        protected override void Pickup()
        {
            StartTask(TaskType.自然采集初级,Duration);
            base.Pickup();
        }
    }
}