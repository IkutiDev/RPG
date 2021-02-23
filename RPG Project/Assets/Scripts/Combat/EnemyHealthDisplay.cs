using RPG.Resources;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter;
        Text healthText;
        private void Awake()
        {
            fighter = GameObject.FindGameObjectWithTag("Player").GetComponent<Fighter>();
            healthText = GetComponent<Text>();
        }
        private void Update()
        {
            if (fighter.GetTarget() != null) healthText.text = string.Format("{0:0.0}%", fighter.GetTarget().GetPercentage());
            else healthText.text = "N/A";
        }

    }
}