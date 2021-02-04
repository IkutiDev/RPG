using RPG.Core;
using System;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName ="Weapon", menuName ="Weapons/Make New Weapon", order =0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] private AnimatorOverrideController animatorOverride = null;
        [SerializeField] private GameObject equippedPrefab = null;
        [SerializeField] private float weaponDamage;
        [SerializeField] private float weaponRange=2f;
        [SerializeField] private bool isRightHanded=true;
        [SerializeField] private Projectile projectile = null;

        const string weaponName = "Weapon";

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            DestroyOldWeapon(rightHand, leftHand);
            if (equippedPrefab != null)
            {
                GameObject weapon= Instantiate(equippedPrefab, isRightHanded?rightHand:leftHand);
                weapon.name = weaponName;
            }

            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
        }

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName);
            if (oldWeapon == null) oldWeapon = leftHand.Find(weaponName);
            if (oldWeapon == null) return;
            Destroy(oldWeapon.gameObject);
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }
        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target)
        {
            Projectile projectileInstance = Instantiate(projectile, isRightHanded ? rightHand.position : leftHand.position, Quaternion.identity);
            projectileInstance.SetTarget(target,weaponDamage);
        }
        public float GetRange()
        {
            return weaponRange;
        }
        public float GetDamage()
        {
            return weaponDamage;
        }
    }
}