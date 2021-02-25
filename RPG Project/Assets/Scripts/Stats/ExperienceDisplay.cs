using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
{
    public class ExperienceDisplay : MonoBehaviour
    {
        Experience experience;
        Text experienceText;
        private void Awake()
        {
            experience = GameObject.FindGameObjectWithTag("Player").GetComponent<Experience>();
            experienceText = GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            experienceText.text = experience.experiencePoints.ToString();
        }
    }
}