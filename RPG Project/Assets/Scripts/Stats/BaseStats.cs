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
        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }
        public int GetLevel()
        {
            Experience experience = GetComponent<Experience>();
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