using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] private Weapon weapon=null;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                if (weapon != null)
                {
                    other.gameObject.GetComponent<Fighter>().EquipWeapon(weapon);
                }
                else
                {
                    Debug.LogError("Weapon asset is missing from this pickup", this);
                }
                Destroy(gameObject);
            }
        }
    }
}