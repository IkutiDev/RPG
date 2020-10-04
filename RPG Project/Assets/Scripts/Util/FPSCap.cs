using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Util
{
    public class FPSCap : MonoBehaviour
    {
        private void Awake()
        {
            Application.targetFrameRate = 120;
        }
    }
}