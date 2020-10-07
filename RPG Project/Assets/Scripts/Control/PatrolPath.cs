using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField] private float sizeOfWaypointGizmos=5f;
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            for (int i=0;i<transform.childCount;i++)
            {
                Transform childTransform = transform.GetChild(i);
                Gizmos.DrawSphere(childTransform.position, sizeOfWaypointGizmos);
            }
        }
    }
}