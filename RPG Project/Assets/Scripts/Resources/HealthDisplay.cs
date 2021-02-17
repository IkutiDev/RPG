using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Resources
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;
        [SerializeField] private Text healthText;
        private void Awake()
        {
            health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        }
        private void Update()
        {
            healthText.text = string.Format("{0:0.0}%",health.GetPercentage());
        }

    }
}