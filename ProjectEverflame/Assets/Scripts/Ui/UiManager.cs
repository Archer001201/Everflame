using System;
using System.Collections;
using System.Collections.Generic;
using CoreMechanics;
using CoreMechanics.EventScripts;
using CoreMechanics.TaskScripts;
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
        // public TextMeshProUGUI natureText;
        // public TextMeshProUGUI scienceText;
        // public TextMeshProUGUI trendText;
        public TextMeshProUGUI alarmText;
        public TextMeshProUGUI disasterText;
        public GameObject alarmPanel;
        // public TextMeshProUGUI thrusterText;
        // public TextMeshProUGUI portalText;
        public Image trendImage;
        public Image expImage;
        public Image prosperityImage;
        public TextMeshProUGUI prosperityText;
        public TextMeshProUGUI tipsText;
        
        [Header("Disaster Panel")]
        public GameObject disasterPanel;
        public Image disasterImage;
        public TextMeshProUGUI dNameText;
        public TextMeshProUGUI dDescriptionText;
        
        [Header("Event Panel")]
        public GameObject eventPanel;
        public TextMeshProUGUI eNameText;
        public TextMeshProUGUI eEffectText;
        public TextMeshProUGUI eDescriptionText;
        public GameObject eventObject;
        
        [Header("Task Panel")]
        public GameObject taskPanel;
        public GameObject taskPrefab;
        private TaskManager _taskManager;
        
        [Header("Abilities Panel")]
        public GameObject thrusterPanel;
        public GameObject portalPanel;
        public TextMeshProUGUI portalText;
        public TextMeshProUGUI thrusterText;

        public GameObject gameOverPanel;
        public GameObject victoryPanel;

        private void Awake()
        {
            disasterPanel.SetActive(false);
            eventPanel.SetActive(false);
            gameManager.SetUiManager(this);
            UpdateUiElements();
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().SetUiManager(this);
            GameObject.FindWithTag("Player").GetComponent<Thruster>().SetUiManager(this);
            GameObject.FindWithTag("Player").GetComponent<Teleport>().SetUiManager(this);
            // _taskManager = GameObject.FindWithTag("TaskManager").GetComponent<TaskManager>();
            // GameObject.FindWithTag("TaskManager").GetComponent<TaskManager>().SetUiManager(this);
        }

        private void Update()
        {
            float targetFillAmount = gameManager.trendRatio;
            // 使用 Mathf.Lerp 平滑过渡到目标值
            trendImage.fillAmount = Mathf.Lerp(trendImage.fillAmount, targetFillAmount, 5f * Time.deltaTime);
        
            float num1 = gameManager.prosperity / gameManager.maxProsperity;
            prosperityImage.fillAmount = Mathf.Lerp(prosperityImage.fillAmount, num1, 5f * Time.deltaTime);
        
            float num2 = (gameManager.levelExp - gameManager.currentPeriodExp) /
                         (gameManager.nextPeriodExp - gameManager.currentPeriodExp);
            expImage.fillAmount = Mathf.Lerp(expImage.fillAmount, num2, 5f * Time.deltaTime);
            
            if (gameManager.prosperity <= 0) gameOverPanel.SetActive(true);
            if (gameManager.level >= 16) victoryPanel.SetActive(true);
        }

        public void UpdateUiElements()
        {
            periodText.text = gameManager.currentPeriod.ToString();
            // levelText.text = "等级：" + gameManager.level;
            // levelExpText.text = "经验：" + gameManager.levelExp + " 下一级经验：" + gameManager.levelUpExp;
            // healthText.text = "繁荣：" + gameManager.prosperity;
            // natureText.text = "<sprite name=leaf> " + gameManager.natureExp;
            // natureText.text = $"<sprite name=leaf> {(int)gameManager.natureExp}";
            // scienceText.text = $"<sprite name=settings> {(int)gameManager.scienceExp}";
            // trendText.text = "趋势：" + gameManager.trendRatio;
            // Debug.Log(gameManager.trendRatio);
            prosperityText.text = gameManager.prosperity + "/" + gameManager.maxProsperity;



            // float num1 = gameManager.levelExp - gameManager.currentPeriodExp;
            // float num2 = gameManager.nextPeriodExp - gameManager.currentPeriodExp;
            // expImage.fillAmount = (gameManager.level%4-1)*0.25f + gameManager.levelExp/gameManager.levelUpExp * 0.25f;
            // expImage.fillAmount = num1 / num2;

            // if ()
            tipsText.text = gameManager.trendRatio switch
            {
                // trendImage.fillAmount = gameManager.trendRatio;
                // prosperityImage.fillAmount = gameManager.prosperity / gameManager.maxProsperity;
                // expImage.fillAmount = (gameManager.levelExp - gameManager.currentPeriodExp) /
                //                       (gameManager.nextPeriodExp - gameManager.currentPeriodExp);
                < 40 => "天灾将造成更多伤害；抵御人祸能力增强",
                > 60 => "人祸将造成更多伤害；抵御天灾能力增强",
                _ => tipsText.text
            };
        }

        public void UpdateAlarmText(int time, string strName)
        {
            if (time > 20) alarmPanel.SetActive(false);
            else
            {
                alarmPanel.SetActive(true);
                disasterText.text = strName;
                alarmText.text = "即将到来 " + time + "s";
            }
        }

        public void UpdateDisasterPanel(DisasterStruct disaster)
        {
            dNameText.text = disaster.name;
            dDescriptionText.text = disaster.description;
            disasterImage.sprite = disaster.background;
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
            thrusterText.text = (int)val + "";
        }

        public void UpdatePortal(float val)
        {
            portalText.text = (int)val + "";
        }

        public void CreateTask(EventStruct eventStruct, TaskType type, float time)
        {
            if (taskPanel.transform.childCount > 2)
            {
                Debug.Log("task list is full");
                return;
            }
            var obj = Instantiate(taskPrefab, taskPanel.transform);

            switch (type)
            {
                case TaskType.自然采集初级: obj.AddComponent<自然采集初级>(); break;
                case TaskType.自然采集中级: obj.AddComponent<自然采集中级>(); break;
                case TaskType.追逐珍宝初级: obj.AddComponent<追逐珍宝初级>(); break;
                case TaskType.追逐珍宝中级: obj.AddComponent<追逐珍宝中级>(); break;
                case TaskType.追逐珍宝高级: obj.AddComponent<追逐珍宝高级>(); break;
                case TaskType.科技采集中级: obj.AddComponent<科技采集中级>(); break;
                case TaskType.科技采集高级: obj.AddComponent<科技采集高级>(); break;
                case TaskType.科技不采集: obj.AddComponent<科技不采集>(); break;
                case TaskType.出圈中级: obj.AddComponent<出圈中级>(); break;
            }
            
            var p = obj.GetComponent<TaskPanel>();
            p.UpdateTaskText(eventStruct.effect);
            p.StartTask(time);
        }
    }
}
