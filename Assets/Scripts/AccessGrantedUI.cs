using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessGrantedUI : MonoBehaviour
{
    void OnEnable()
    {
        Invoke("PlaySound", 1.75f);
    }

    public void PlaySound()
    {
        GetComponent<AudioSource>().Play();
    }
}
