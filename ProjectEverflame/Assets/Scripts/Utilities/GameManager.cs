using System;
using CoreMechanics;
using Ui;
using UnityEngine;
using UnityEngine.Serialization;

namespace Utilities
{
    public enum CivilPeriod
    {
        混元纪, 起承纪, 黎明纪, 星辉纪,
    }
    public class GameManager : MonoBehaviour
    {
        public int level = 1;
        public CivilPeriod currentPeriod = CivilPeriod.混元纪;
        public int prosperity = 100;
        public int levelExp;
        public int levelUpExp;
        public int natureExp;
        public int scienceExp;
        public float trendRatio;

        private UiManager _uiManager;
        private DisasterGenerator _disasterGenerator;

        private void Awake()
        {
            levelUpExp = CalculateLevelUpExp(level);
        }

        private void HandleLevel(bool levelUp)
        {
            if (levelUp)
            {
                level++;
                prosperity += 100;
                // Debug.Log("health: " + health);
            }
            else level--;
            
            levelUpExp = CalculateLevelUpExp(level);
            
            currentPeriod = level switch
            {
                < 6 => CivilPeriod.混元纪,
                < 11 => CivilPeriod.起承纪,
                < 16 => CivilPeriod.黎明纪,
                _ => CivilPeriod.星辉纪
            };
            
            _disasterGenerator.UpdateDisasterList();
        }

        public void HandleProsperity(int val, bool additive)
        {
            if (additive) prosperity += val;
            else prosperity -= val;
            
            _uiManager.UpdateUiElements();
        }

        public void HandleNatureExp(int exp, bool additive)
        {
            if (additive) natureExp += exp;
            else natureExp -= exp;
            
            HandleLevelExp();
            CalculateTrendRatio();
            
            _uiManager.UpdateUiElements();
            // Debug.Log("Level: " + level + ", Science: " + scienceExp + ", Nature: " + natureExp + ", Trend: " + trendRatio);
        }
        
        public void HandleScienceExp(int exp, bool additive)
        {
            if (additive) scienceExp += exp;
            else scienceExp -= exp;
            
            HandleLevelExp();
            CalculateTrendRatio();
            
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
        }

        public void SetUiManager(UiManager manager)
        {
            _uiManager = manager;
        }

        public void SetDisasterGenerator(DisasterGenerator generator)
        {
            _disasterGenerator = generator;
        }
    }
}
