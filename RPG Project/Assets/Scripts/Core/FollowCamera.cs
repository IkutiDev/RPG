using RPG.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private void Start()
        {
            if (target == null)
            {
                target = FindObjectOfType<PlayerController>().transform;
                if(target==null) { throw new System.Exception("No target in follow camera and no playerController in the scene"); }
            }
        }
        // Update is called once per frame
        void LateUpdate()
        {
            transform.position = target.position;
        }
    }
}