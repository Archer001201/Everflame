using System;
using CoreMechanics;
using TMPro;
using UnityEngine;
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
        
        [Header("Disaster Panel")]
        public GameObject disasterPanel;
        public TextMeshProUGUI dNameText;
        public TextMeshProUGUI dDescriptionText;

        private void Awake()
        {
            disasterPanel.SetActive(false);
            gameManager.SetUiManager(this);
            UpdateUiElements();
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
    }
}
