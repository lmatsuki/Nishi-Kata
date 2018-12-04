﻿using System.Collections;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public bool canFire;
    public float initialDelay;

    protected virtual void Update()
    {
        if (initialDelay > 0)
        {
            StartCoroutine("WaitForInitialDelay");
        }
    }

    IEnumerator WaitForInitialDelay()
    {
        yield return new WaitForSeconds(initialDelay);

        if (!canFire)
        {
            initialDelay = 0;
            canFire = true;
        }
    }
}