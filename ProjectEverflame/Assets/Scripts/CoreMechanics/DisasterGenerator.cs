using System;
using System.Collections;
using Ui;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

// ReSharper disable IteratorNeverReturns

namespace CoreMechanics
{
    public class DisasterGenerator : MonoBehaviour
    {
        public Vector2 range;
        public UiManager uiManager;
        public float duration;
        public GameManager gameManager;

        private void OnEnable()
        {
            StartCoroutine(GenerateDisaster());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private IEnumerator GenerateDisaster()
        {
            while (true)
            {
                var timer = Random.Range(range.x, range.y);

                for (var countdown = timer; countdown > 0; countdown -= 1f)
                {
                    uiManager.UpdateAlarmText((int)countdown);
                    yield return new WaitForSeconds(1f);
                }
                
                uiManager.disasterPanel.SetActive(true);
                
                yield return new WaitForSeconds(duration);
                
                uiManager.disasterPanel.SetActive(false);
            }
        }
    }
}
