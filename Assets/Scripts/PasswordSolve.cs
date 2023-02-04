using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    int currentTier = 0;
    // Start is called before the first frame update
    void Start()
    {
        RootWordsLibrary.LoadRootWords();

        // get a word from the list
        RootModel word = RootWordsLibrary.RootWords.GetWordByTier(currentTier);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
