using System;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;
        public float GetStat(CharacterClass characterClass, int level)
        {
            foreach (ProgressionCharacterClass progressionCharacterClass in characterClasses)
            {
                if (progressionCharacterClass.characterClass == characterClass)
                {

                    
                }
            }
            return 0f;
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