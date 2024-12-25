using System;
using Player;
using UnityEngine;

namespace Misc
{
    public class Rain : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            other.GetComponent<PlayerController>().ChangeSpeedWithoutTimer(0.5f, true);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            other.GetComponent<PlayerController>().ChangeSpeedWithoutTimer(0.5f, false);
        }
    }
}
