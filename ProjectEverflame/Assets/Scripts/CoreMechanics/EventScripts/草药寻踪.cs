using System.Collections;
using UnityEngine;

namespace CoreMechanics.EventScripts
{
    public class 草药寻踪 : BaseDevelopmentalEvent
    {
        protected override void Pickup()
        {
            base.Pickup();
            Debug.Log("task picked up");
            StartTask(Task());
        }

        protected override IEnumerator Task()
        {
            Debug.Log("cao yao xun zong");
            yield return new WaitForSeconds(3);
            Debug.Log("task end");
        }
    }
}