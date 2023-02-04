using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootWordsLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RootWordsLibrary.LoadRootWords();

        Debug.Log(RootWordsLibrary.RootWords);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
