﻿using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    [SerializeField] private float projectileSpeed = 1f;

    private Health target = null;

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;
        transform.LookAt(GetAimLocation());
        transform.Translate(Vector3.forward*Time.deltaTime*projectileSpeed);
    }

    public void SetTarget(Health target)
    {
        this.target = target;
    }

    private Vector3 GetAimLocation()
    {
        CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
        if (targetCapsule == null)
        {
            return target.transform.position;
        }
        return target.transform.position + Vector3.up * targetCapsule.height/2;
    }
}
