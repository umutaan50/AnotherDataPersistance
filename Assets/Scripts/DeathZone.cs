using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Project name: AnotherDataPersistence
public class DeathZone : MonoBehaviour
{
    public MainManager Manager;

    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
        Manager.GameOver();
    }
}
