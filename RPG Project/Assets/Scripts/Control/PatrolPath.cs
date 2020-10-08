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
                int j = GetNextIndex(i);
                Gizmos.DrawSphere(GetWaypoint(i), sizeOfWaypointGizmos);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }
        }

        public int GetNextIndex(int i)
        {
            if (i+1 == transform.childCount)
            {
                return 0;
            }
            else
            {
                return i + 1;
            }
        }

        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}