using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalScript : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<AudioSource>().Play();
    }
}
