using System;
using CoreMechanics;
using Player;
using Ui;
using UnityEngine;
using UnityEngine.Serialization;

namespace Utilities
{
    public enum CivilPeriod
    {
        荒原纪, 启程纪, 黎明纪, 星辉纪,
    }
    public class GameManager : MonoBehaviour
    {
        public int level = 1;
        public CivilPeriod currentPeriod = CivilPeriod.荒原纪;
        public float prosperity = 100;
        public float levelExp;
        public float levelUpExp;
        public float natureExp;
        public float scienceExp;
        public float trendRatio = 0.5f;
        public float nextPeriodExp = 100;
        public float currentPeriodExp; 

        private UiManager _uiManager;
        private DisasterGenerator _disasterGenerator;
        private EventGenerator _eventGenerator;
        private Thruster _thruster;
        private Teleport _teleport;
        private ResourceGenerator _resourceGenerator;

        private void Awake()
        {
            levelUpExp = CalculateLevelUpExp(level);
            nextPeriodExp = CalculateLevelUpExp(((int)(level / 5) + 1) * 4);
        }

        private void Start()
        {
            _thruster = GameObject.FindWithTag("Player").GetComponent<Thruster>();
            _teleport = GameObject.FindWithTag("Player").GetComponent<Teleport>();
        }

        private void HandleLevel(bool levelUp)
        {
            if (levelUp)
            {
                level++;
                prosperity += 50;
                // Debug.Log("health: " + health);
            }
            else level--;
            
            level = Mathf.Clamp(level, 1, int.MaxValue);
            
            levelUpExp = CalculateLevelUpExp(level);
            currentPeriodExp = level > 4 ? CalculateLevelUpExp((int)(level / 5) * 4) : 0;
            nextPeriodExp = CalculateLevelUpExp(((int)(level / 5) + 1) * 4);
            
            currentPeriod = level switch
            {
                < 5 => CivilPeriod.荒原纪,
                < 9 => CivilPeriod.启程纪,
                < 13 => CivilPeriod.黎明纪,
                _ => CivilPeriod.星辉纪
            };

            _thruster.unlocked = level >= 5;
            _teleport.unlocked = level >= 13;
            
            _eventGenerator.UpdateEventList();
            _resourceGenerator.UpdateEventList();
            _disasterGenerator.UpdateDisasterList();
        }

        public void HandleProsperity(float val, bool additive)
        {
            if (additive) prosperity += val;
            else prosperity -= val;
            prosperity = Mathf.Clamp(prosperity, 0, float.MaxValue);
            _uiManager.UpdateUiElements();
        }

        public void HandleNatureExp(float exp, bool additive)
        {
            if (additive)
            {
                natureExp += exp;
                if (_thruster.enabled) _thruster.Charging(exp);
                EventHandler.CollectNatureResource(exp);
            }
            else natureExp -= exp;
            
            natureExp = Mathf.Clamp(natureExp, 0, float.MaxValue);
            
            HandleLevelExp();
            CalculateTrendRatio();
            // trendRatio = Mathf.Clamp(trendRatio, 0, 1);
            _uiManager.UpdateUiElements();
            // Debug.Log("Level: " + level + ", Science: " + scienceExp + ", Nature: " + natureExp + ", Trend: " + trendRatio);
        }
        
        public void HandleScienceExp(float exp, bool additive)
        {
            if (additive)
            {
                scienceExp += exp;
                _teleport.Charging(scienceExp);
            }
            else scienceExp -= exp;
            
            scienceExp = Mathf.Clamp(scienceExp, 0, float.MaxValue);
            
            HandleLevelExp();
            CalculateTrendRatio();
            // trendRatio = Mathf.Clamp(trendRatio, 0, 1);
            _uiManager.UpdateUiElements();
            // Debug.Log("Level: " + level + ", Science: " + scienceExp + ", Nature: " + natureExp + ", Trend: " + trendRatio);
        }

        private void HandleLevelExp()
        {
            levelExp = scienceExp + natureExp;

            var nextLevelExp = CalculateLevelUpExp(level);
            var previousLevelExp = CalculateLevelUpExp(level - 1);
            
            if (levelExp >= nextLevelExp) HandleLevel(true);
            else if (levelExp < previousLevelExp) HandleLevel(false);
            
            // Debug.Log("Exp: " + levelExp + ", Next: " + nextLevelExp + ", Previous: " + previousLevelExp);
        }

        private static int CalculateLevelUpExp(int targetLevel)
        {
            var exp = 3;
            for (var i = 1; i <= targetLevel; i++)
            {
                var para = i / 5 + 1;
                exp += 2 * para + (para + 1) * (i - para);
            }
            return exp;
        }

        private void CalculateTrendRatio()
        {
            trendRatio = (float)scienceExp / levelExp;
            if (levelExp == 0) trendRatio = 0.5f;
            // if (trendRatio < 0) trendRatio = 0;
            trendRatio = Mathf.Clamp01(trendRatio);
            // Debug.Log(trendRatio);
        }

        public void SetUiManager(UiManager manager)
        {
            _uiManager = manager;
        }

        public void SetDisasterGenerator(DisasterGenerator generator)
        {
            _disasterGenerator = generator;
        }

        public void SetEventGenerator(EventGenerator generator)
        {
            _eventGenerator = generator;
        }

        public void SetResourceGenerator(ResourceGenerator generator)
        {
            _resourceGenerator = generator;
        }
    }
}
