using System;
using System.Collections;
using System.Collections.Generic;
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
        public Vector2 period1Range;
        public Vector2 period2Range;
        public Vector2 period3Range;
        public Vector2 period4Range;
        public List<DisasterStruct> universalDisasters;
        public List<DisasterStruct> period1Disasters;
        public List<DisasterStruct> period2Disasters;
        public List<DisasterStruct> period3Disasters;
        public List<DisasterStruct> period4Disasters;
        public List<DisasterStruct> currentDisasters;

        private void Awake()
        {
            gameManager.SetDisasterGenerator(this);
            UpdateDisasterList();
        }

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
                var nextDisaster = currentDisasters[Random.Range(0, currentDisasters.Count)];

                for (var countdown = timer; countdown > 0; countdown -= 1f)
                {
                    uiManager.UpdateAlarmText((int)countdown, nextDisaster.disasterType.ToString());
                    yield return new WaitForSeconds(1f);
                }
                
                uiManager.UpdateDisasterPanel(nextDisaster);
                uiManager.disasterPanel.SetActive(true);
                DealDamage(nextDisaster);
                
                yield return new WaitForSeconds(duration);
                
                uiManager.disasterPanel.SetActive(false);
            }
        }

        public void UpdateDisasterList()
        {
            currentDisasters.Clear();
            currentDisasters.AddRange(universalDisasters);
            switch (gameManager.currentPeriod)
            {
                case CivilPeriod.荒原纪:
                    currentDisasters.AddRange(period1Disasters); 
                    range = period1Range;
                    break;
                case CivilPeriod.启程纪:
                    currentDisasters.AddRange(period2Disasters);
                    range = period2Range;
                    break;
                case CivilPeriod.黎明纪:
                    currentDisasters.AddRange(period3Disasters); 
                    range = period3Range;
                    break;
                case CivilPeriod.星辉纪: 
                    currentDisasters.AddRange(period4Disasters); 
                    range = period4Range;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void DealDamage(DisasterStruct disaster)
        {
            var damage = (gameManager.level / 5 + 1)*disaster.damageToProsperity;
            switch (gameManager.trendRatio)
            {
                case <= 0.2f:
                    if (disaster.disasterType == DisasterType.天灾) damage = (int)(damage * 1.5f);
                    else damage = (int)(damage * 0.5f);
                    break;
                case <= 0.4f:
                    if (disaster.disasterType == DisasterType.天灾) damage = (int)(damage * 1.25f);
                    else damage = (int)(damage * 0.75f);
                    break;
                case <= 0.6f:
                    break;
                case <= 0.8f:
                    if (disaster.disasterType == DisasterType.天灾) damage = (int)(damage * 0.75f);
                    else damage = (int)(damage * 1.25f);
                    break;
                case <= 1.0f:
                    if (disaster.disasterType == DisasterType.天灾) damage = (int)(damage * 0.5f);
                    else damage = (int)(damage * 1.5f);
                    break;
            }
            gameManager.HandleProsperity(damage, false);
            gameManager.HandleNatureExp((int)(gameManager.levelExp*disaster.damageRateToNature), false);
            gameManager.HandleScienceExp((int)(gameManager.levelExp*disaster.damageRateToScience), false);
        }
    }
}
