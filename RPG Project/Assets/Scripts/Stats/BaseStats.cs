using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1,99)]
        [SerializeField] private int startingLevel = 1;
        [SerializeField] private CharacterClass characterClass;
        [SerializeField] private Progression progression = null;
        private int currentLevel = 0;
        private Experience experience;
        private void Awake()
        {
            experience = GetComponent<Experience>();
        }
        private void Start()
        {
            currentLevel = GetLevel();
            if (experience != null)
            {
                experience.onExperiencedGained += UpdateLevel;
            }

        }
        private void OnDestroy()
        {
            if (experience != null)
            {
                experience.onExperiencedGained -= UpdateLevel;
            }
        }
        private void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            if (newLevel > currentLevel)
            {
                currentLevel = newLevel;
            }
        }
        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }
        public int GetLevel()
        {
            if (currentLevel < 1)
            {
                currentLevel = CalculateLevel();
            }
            return currentLevel;
        }
        public int CalculateLevel()
        {
            
            if (experience==null) return startingLevel;
            float currentEXP = experience.experiencePoints;
            for (int level = 1; level < progression.GetMaxPlayerLevel(); level++)
            {
                float EXPToLevelUp = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, level);
                if(EXPToLevelUp> currentEXP)
                {
                    return level;
                }
            }
            return progression.GetMaxPlayerLevel();
        }
    }
}