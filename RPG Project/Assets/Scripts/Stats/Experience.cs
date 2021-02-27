using RPG.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        public float experiencePoints { get; private set; } = 0;

        public Action onExperiencedGained;

        public void GainExperience(float experience)
        {
            experiencePoints += experience;
            onExperiencedGained?.Invoke();
        }
        public object CaptureState()
        {
            return experiencePoints;
        }

        public void RestoreState(object state)
        {
            experiencePoints = (float)state;
        }
    }

}