﻿using RPG.Control;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        private PlayableDirector playableDirector;
        private GameObject player;
        private void Awake()
        {
            playableDirector = GetComponent<PlayableDirector>();
        }
        private void Start()
        {
            playableDirector.played += DisableControl;
            playableDirector.stopped += EnableControl;
            player = GameObject.FindWithTag("Player");
        }
        private void OnDestroy()
        {
            playableDirector.played -= DisableControl;
            playableDirector.stopped -= EnableControl;
        }
        void DisableControl(PlayableDirector playableDirector)
        {
            
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }
        void EnableControl(PlayableDirector playableDirector)
        {
            player.GetComponent<PlayerController>().enabled = true;
        }
    }
}