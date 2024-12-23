using System.Collections;
using UnityEngine;

namespace CoreMechanics.EventScripts
{
    public class 草药寻踪 : BaseDevelopmentalEvent
    {
        private const float Duration = 10;

        protected override void Pickup()
        {
            StartTask(Duration);
            base.Pickup();
        }

        public override IEnumerator Task()
        {
            yield return new WaitForSeconds(Duration);
            Debug.Log("...");
        }
    }
}