using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private float distFactorOutToIn = 5;
    private float soundVolume = 0.8f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Sound"))
        {
            float lerpFact =  1 - (Vector3.Distance(transform.position, other.ClosestPoint(transform.position)) / distFactorOutToIn);
            other.GetComponent<AudioSource>().volume = Mathf.Lerp(0, soundVolume, lerpFact);
            Debug.Log(Mathf.Lerp(0, soundVolume, lerpFact));
        }
    }
}
