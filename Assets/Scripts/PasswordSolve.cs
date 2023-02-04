using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class PasswordSolve : MonoBehaviour
{
    public static PasswordSolve Instance;
    
    [HideInInspector]
    public RootModel Word = null;
    
    [HideInInspector]
    public string MaskedExample = null;

    public int Tier;
    int Attempt = 0;
    string Hint = null;
    bool ShowHint = false;
    string UserInput = "";
    string Example = null;
    System.Random random = new System.Random();
    List<string> UsedExamples = new List<string>();

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Tier = 1;
    }

    public void GeneratePasswordSolve()
    {
        RootWordsLibrary.LoadRootWords();
        Word = RootWordsLibrary.GetRandomWordByTier(Tier);
        if (Word == null )
        {
            Debug.Log($"Unable to find a word for tier {Tier}");
            return;
        }

        Example = Word.examples[0];
        MaskedExample = MaskExample();
        Hint = Word.definition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckAnswer()
    {
        Attempt++;

        if (UserInput == Word.root)
        {
            Success();
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


    IEnumerator ScrambleExample()
    {
        int ScrambleIterations = 20;
        while (ScrambleIterations > 0)
        {
            Example = RandomString(Example.Length - Word.root.Length);
            ScrambleIterations--;
            yield return new WaitForSeconds(0.1f); // *skepticism intesifies* should delay the loop iterations by 0.1 seconds
        }

        Example = GetNewExample();
        MaskedExample = MaskExample();
    }


    void Reset(){
        Attempt = 0;
        Hint = null;
        ShowHint = false;
        UserInput = "";
        Example = null;
        MaskedExample = null;
        UsedExamples.Clear();
        Word = null;
    }


     void Success(){
        EditorUtility.DisplayDialog("Success",
                "Success", "OK", "Cancel", DialogOptOutDecisionType.ForThisSession, null);
        Reset();
     }


     void Fail(){
        EditorUtility.DisplayDialog("Fail",
                "Fail", "OK", "Cancel", DialogOptOutDecisionType.ForThisSession, null);
        Reset();
     }


    private string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(System.Linq.Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }


    private string GetNewExample()
    {
        if (UsedExamples.Count == Word.examples.Count)
        {
            Fail();
            return null;
        }

        string example = Word.examples[random.Next(Word.examples.Count)];
        while (UsedExamples.Contains(example))
        {
            example = Word.examples[random.Next(Word.examples.Count)];
        }
        return example;
    }


    private string MaskExample()
    {
        int RootLength = Word.root.Length;
        string RootReplacement = "";
        for (int i = 0; i < RootLength; i++)
        {
            RootReplacement += "*";
        }
        return Example.Replace(Word.root, RootReplacement);
    }

}
