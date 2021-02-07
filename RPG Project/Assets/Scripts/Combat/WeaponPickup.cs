using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] private Weapon weapon=null;
        [SerializeField] private float respawnTime = 5;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(HideForSeconds(respawnTime));
                if (weapon != null)
                {
                    other.gameObject.GetComponent<Fighter>().EquipWeapon(weapon);
                }
                else
                {
                    Debug.LogError("Weapon asset is missing from this pickup", this);
                }
            }
        }
        private IEnumerator HideForSeconds(float seconds)
        {
            ShowPickup(false);
            yield return new WaitForSeconds(seconds);
            ShowPickup(true);
        }

        private void ShowPickup(bool show)
        {
            GetComponent<Collider>().enabled = show;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(show);
            }
        }
        private void HidePickup()
        {
            ShowPickup(false);
        }
    }
}