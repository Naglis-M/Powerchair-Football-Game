using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class Ball : MonoBehaviour
{
    public float Force;
    // Update is called once per frame
    void OnHit()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.forward * Force, ForceMode.Impulse);
        
    }
}
