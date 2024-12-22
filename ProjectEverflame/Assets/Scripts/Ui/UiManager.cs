using System;
using CoreMechanics;
using CoreMechanics.EventScripts;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

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
        public TextMeshProUGUI trendText;
        public TextMeshProUGUI alarmText;
        public TextMeshProUGUI thrusterText;
        public TextMeshProUGUI portalText;
        
        [Header("Disaster Panel")]
        public GameObject disasterPanel;
        public TextMeshProUGUI dNameText;
        public TextMeshProUGUI dDescriptionText;
        
        [Header("Event Panel")]
        public GameObject eventPanel;
        public TextMeshProUGUI eNameText;
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
            levelText.text = "等级：" + gameManager.level;
            levelExpText.text = "经验：" + gameManager.levelExp + " 下一级经验：" + gameManager.levelUpExp;
            healthText.text = "繁荣：" + gameManager.prosperity;
            natureText.text = "自然：" + gameManager.natureExp;
            scienceText.text = "科学：" + gameManager.scienceExp;
            trendText.text = "趋势：" + gameManager.trendRatio;
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
            eNameText.text = e.eventStruct.eventName;
            eDescriptionText.text = e.eventStruct.description;
            eventPanel.SetActive(true);
        }

        public void UpdateThruster(int val)
        {
            thrusterText.text = "推进器充能：" + val + "/10";
        }

        public void UpdatePortal(int val)
        {
            portalText.text = "传送门充能：" + val + "/20";
        }
    }
}
