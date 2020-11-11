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
        private Fader fader;
        [SerializeField] private float fadeInTime =0.2f;
        private void Awake()
        {
            savingSystem = GetComponent<SavingSystem>();
           
        }
        IEnumerator Start()
        {
            fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
            yield return savingSystem.LoadLastScene(defaultSaveFile);
            yield return fader.FadeIn(fadeInTime);
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