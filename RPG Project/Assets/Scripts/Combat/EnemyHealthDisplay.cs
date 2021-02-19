using RPG.Resources;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter;
        private void Awake()
        {
            fighter = GameObject.FindGameObjectWithTag("Player").GetComponent<Fighter>();
        }
        private void Update()
        {
            if (fighter.GetTarget() != null) GetComponent<Text>().text = string.Format("{0:0.0}%", fighter.GetTarget().GetPercentage());
            else GetComponent<Text>().text = "N/A";
        }

    }
}