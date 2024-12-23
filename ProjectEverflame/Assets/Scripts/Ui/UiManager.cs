using System;
using CoreMechanics;
using CoreMechanics.EventScripts;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utilities;
using EventType = CoreMechanics.EventType;

// ReSharper disable PossibleLossOfFraction

namespace Ui
{
    public class UiManager : MonoBehaviour
    {
        public GameManager gameManager;
        
        [Header("Simple UI Elements")] 
        public TextMeshProUGUI periodText;
        public TextMeshProUGUI levelText;
        public TextMeshProUGUI levelExpText;
        public TextMeshProUGUI healthText;
        public TextMeshProUGUI natureText;
        public TextMeshProUGUI scienceText;
        // public TextMeshProUGUI trendText;
        public TextMeshProUGUI alarmText;
        public TextMeshProUGUI thrusterText;
        public TextMeshProUGUI portalText;
        public Image trendImage;
        public Image expImage;
        
        [Header("Disaster Panel")]
        public GameObject disasterPanel;
        public TextMeshProUGUI dNameText;
        public TextMeshProUGUI dDescriptionText;
        
        [Header("Event Panel")]
        public GameObject eventPanel;
        public TextMeshProUGUI eNameText;
        public TextMeshProUGUI eEffectText;
        public TextMeshProUGUI eDescriptionText;
        public GameObject eventObject;

        private void Awake()
        {
            disasterPanel.SetActive(false);
            eventPanel.SetActive(false);
            gameManager.SetUiManager(this);
            UpdateUiElements();
            GameObject.FindWithTag("Player").GetComponent<MoveController>().SetUiManager(this);
            GameObject.FindWithTag("Player").GetComponent<Thruster>().SetUiManager(this);
            GameObject.FindWithTag("Player").GetComponent<Teleport>().SetUiManager(this);
        }

        public void UpdateUiElements()
        {
            periodText.text = gameManager.currentPeriod.ToString();
            // levelText.text = "等级：" + gameManager.level;
            levelExpText.text = "经验：" + gameManager.levelExp + " 下一级经验：" + gameManager.levelUpExp;
            healthText.text = "繁荣：" + gameManager.prosperity;
            // natureText.text = "<sprite name=leaf> " + gameManager.natureExp;
            natureText.text = $"<sprite name=leaf> {(int)gameManager.natureExp}";
            scienceText.text = $"<sprite name=settings> {(int)gameManager.scienceExp}";
            // trendText.text = "趋势：" + gameManager.trendRatio;
            trendImage.fillAmount = gameManager.trendRatio;
            
            float num1 = gameManager.levelExp - gameManager.currentPeriodExp;
            float num2 = gameManager.nextPeriodExp - gameManager.currentPeriodExp;
            // expImage.fillAmount = (gameManager.level%4-1)*0.25f + gameManager.levelExp/gameManager.levelUpExp * 0.25f;
            expImage.fillAmount = num1 / num2;
        }

        public void UpdateAlarmText(int time)
        {
            alarmText.text = "未知危机即将到来 " + time + "s";
        }

        public void UpdateDisasterPanel(DisasterStruct disaster)
        {
            dNameText.text = disaster.name;
            dDescriptionText.text = disaster.description;
        }

        public void UpdateEventPanel(GameObject obj)
        {
            if (!obj)
            {
                eventPanel.SetActive(false);
                eventObject = null;
                return;
            }
            
            eventObject = obj;
            var e = eventObject.GetComponent<BaseDevelopmentalEvent>();
            var c = e.eventStruct.eventType switch
            {
                EventType.危机 => Color.red,
                EventType.任务 => Color.cyan,
                EventType.机遇 => Color.yellow,
                _ => throw new ArgumentOutOfRangeException()
            };
            eNameText.text = e.eventStruct.eventName;
            eNameText.color = c;
            eEffectText.text = e.eventStruct.effect;
            eEffectText.color = c;
            eDescriptionText.text = e.eventStruct.description;
            eventPanel.SetActive(true);
        }

        public void UpdateThruster(float val)
        {
            thrusterText.text = "推进器充能：" + (int)val + "/10";
        }

        public void UpdatePortal(float val)
        {
            portalText.text = "传送门充能：" + (int)val + "/20";
        }
    }
}
