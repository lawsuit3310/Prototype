using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffect : MonoBehaviour
{
    private void OnParticleSystemStopped()
    {
        Destroy(this.transform.parent.gameObject);
    }
}
