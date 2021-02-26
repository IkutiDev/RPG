using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        BaseStats baseStats;
        Text levelText;
        private void Awake()
        {
            baseStats = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseStats>();
            levelText = GetComponent<Text>();
        }
        private void Update()
        {
            levelText.text = baseStats.GetLevel().ToString();
        }
    }
}