using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] private int maxPlayerLevel = 60;
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;
        Dictionary<CharacterClass, Dictionary<Stat,ProgressionStatFormula>> lookupTable = null;
        public float GetStat(Stat stat,CharacterClass characterClass, int level)
        {
            BuildLookup();
            return lookupTable[characterClass][stat].Calculate(level);
        }
        public int GetMaxPlayerLevel()
        {
            return maxPlayerLevel;
        }
        void BuildLookup()
        {
            if (lookupTable != null) return;
            lookupTable = new Dictionary<CharacterClass, Dictionary<Stat, ProgressionStatFormula>>();
            foreach(ProgressionCharacterClass progressionCharacterClass in characterClasses)
            {
                var statTable = new Dictionary<Stat, ProgressionStatFormula>();
                foreach (ProgressionStat progressionStat in progressionCharacterClass.stats)
                {
                    statTable.Add(progressionStat.stat, progressionStat.formula);
                    
                }
                lookupTable.Add(progressionCharacterClass.characterClass,statTable);
            }

        }
        [Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public ProgressionStat[] stats;
        }
        [Serializable]
        class ProgressionStat
        {
            public Stat stat;
            public ProgressionStatFormula formula;
        }
        [Serializable]
        class ProgressionStatFormula
        {
            [Range(1, 1000)]
            [SerializeField] float startingValue = 100;
            [Range(0, 1)]
            [SerializeField] float percentageAdded = 0.0f;
            [Range(0, 1000)]
            [SerializeField] float absoluteAdded = 10;
            public float Calculate(int level)
            {
                if (level <= 1) return startingValue;
                float c = Calculate(level - 1);
                return c + (c * percentageAdded) + absoluteAdded;
            }
        }
    }
}