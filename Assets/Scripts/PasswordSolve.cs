using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    int Tier = 0;
    int Attempt = 0;

    string Hint = null;
    bool ShowHint = false;
    string UserInput = "";

    string Example = null;

    List<string> UsedExamples = new List<string>();
    RootModel Word = null;
    // Start is called before the first frame update
    void Start()
    {
        RootWordsLibrary.LoadRootWords();
        Word = RootWordsLibrary.GetWordByTier(Tier);
        Example = Word.examples[0];
        Hint = Word.definition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckAnswer()
    {
        Attempt++;

        if (UserInput == Word.root)
        {
            Reset();
            return;
        }

        if(Attempt > Word.examples.Count)
        {
            Fail();
            return;
        }

        ShowHint = Attempt >= 1 ? true : false;

        UsedExamples.Add(Example);
        ScrambleExample();
    }

    void ScrambleExample()
    {
        int ScrambleIterations = 10;
        while (ScrambleIterations > 0)
        {
            Example = "";
            ScrambleIterations--;
        }
    }

    void Reset(){
        Attempt = 0;
        Hint = null;
        ShowHint = false;
        UserInput = "";
        Example = null;
        UsedExamples.Clear();
        Word = null;
    }

     void Success(){}

     void Fail(){
        Reset();
     }

    // private static Random random = new Random();

    // public static string RandomString(int length)
    // {
    //     const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    //     return new string(Enumerable.Repeat(chars, length)
    //         .Select(s => s[random.Next(s.Length)]).ToArray());
    // }
}
