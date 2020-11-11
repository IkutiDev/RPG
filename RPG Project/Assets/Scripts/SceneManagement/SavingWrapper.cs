using RPG.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";
        private SavingSystem savingSystem;
        private void Awake()
        {
            savingSystem = GetComponent<SavingSystem>();
        }
        private void Update()
        {
            if (Keyboard.current.lKey.wasPressedThisFrame)
            {
                Load();
            }
            if (Keyboard.current.sKey.wasPressedThisFrame)
            {
                Save();
            }
        }

        public void Save()
        {
            savingSystem.Save(defaultSaveFile);
        }

        public void Load()
        {
            savingSystem.Load(defaultSaveFile);
        }
    }
}