using System;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;
        [Serializable]
        class ProgressionCharacterClass
        {
            [SerializeField] CharacterClass characterClass;
            [SerializeField] ProgressionStatFormula healthFormula;
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