using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifiers
        {
            A,B,C
        }
        [SerializeField] private int sceneToLoadId = -1;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private DestinationIdentifiers destination;
        [SerializeField] private float fadeOutTime=1f;
        [SerializeField] private float fadeInTime = 1f;
        [SerializeField] private float waitTimeWhileFaded = 1f;
        private SavingWrapper savingWrapper;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }
        private IEnumerator Transition()
        {
            if (sceneToLoadId < 0)
            {
                Debug.LogError("Scene to load not set");
                yield break;
            }
            
            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();

            yield return fader.FadeOut(fadeOutTime);

            savingWrapper = FindObjectOfType<SavingWrapper>();
            savingWrapper.Save();
            yield return SceneManager.LoadSceneAsync(sceneToLoadId);

            savingWrapper.Load();

            Portal otherPortal = GetOtherPortal();

            UpdatePlayer(otherPortal);
            savingWrapper.Save();
            yield return new WaitForSeconds(waitTimeWhileFaded);
           yield return  fader.FadeIn(fadeInTime);
            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

        private Portal GetOtherPortal()
        {
            foreach(var portal in FindObjectsOfType<Portal>())
            {
                if (portal != this && portal.destination==destination)
                {
                    return portal;
                }
            }
            return null;
        }
    }
}