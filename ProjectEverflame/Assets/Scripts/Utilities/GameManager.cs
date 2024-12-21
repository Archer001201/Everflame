using UnityEngine;

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
        public int health = 100;
        public int levelExp;
        public int natureExp;
        public int scienceExp;
        public float trendRatio;

        private void HandleLevel(bool levelUp)
        {
            if (levelUp)
            {
                level++;
                health += 100;
                Debug.Log("health: " + health);
            }
            else level--;
            
            currentPeriod = level switch
            {
                < 6 => CivilPeriod.混元纪,
                < 11 => CivilPeriod.起承纪,
                < 16 => CivilPeriod.黎明纪,
                _ => CivilPeriod.星辉纪
            };
        }

        public void HandleNatureExp(int exp, bool additive)
        {
            if (additive) natureExp += exp;
            else natureExp -= exp;
            
            HandleLevelExp();
            CalculateTrendRatio();
            
            Debug.Log("Level: " + level + ", Science: " + scienceExp + ", Nature: " + natureExp + ", Trend: " + trendRatio);
        }
        
        public void HandleScienceExp(int exp, bool additive)
        {
            if (additive) scienceExp += exp;
            else scienceExp -= exp;
            
            HandleLevelExp();
            CalculateTrendRatio();
            
            Debug.Log("Level: " + level + ", Science: " + scienceExp + ", Nature: " + natureExp + ", Trend: " + trendRatio);
        }

        private void HandleLevelExp()
        {
            levelExp = scienceExp + natureExp;
            
            var nextLevelExp = 0;
            for (var i = 1; i <= level; i++)
            {
                var para = i / 5 + 1;
                nextLevelExp += 5 * para + (para + 1) * (i - para);
            }
            
            var previousLevelExp = 0;
            for (var i = 1; i < level; i++)
            {
                var para = i / 5 + 1;
                previousLevelExp += 5 * para + (para + 1) * (i - para);
            }
            
            if (levelExp >= nextLevelExp) HandleLevel(true);
            else if (levelExp < previousLevelExp) HandleLevel(false);
            
            Debug.Log("Exp: " + levelExp + ", Next: " + nextLevelExp + ", Previous: " + previousLevelExp);
        }

        private void CalculateTrendRatio()
        {
            trendRatio = (float)scienceExp / levelExp;
        }
    }
}
