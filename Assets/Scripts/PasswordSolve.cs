using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class PasswordSolve : MonoBehaviour
{
    public static PasswordSolve Instance;
    
    [HideInInspector]
    public string Example = null;

    [HideInInspector]
    public string MaskedExample = null;

    [HideInInspector]
    public string Hint = null;

    [HideInInspector]
    public bool OutOfAttempts = false;

    RootModel Word = null;
    int Attempt = 0;
    bool ShowHint = false;
    System.Random random = new System.Random();
    List<string> UsedExamples = new List<string>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public void GeneratePasswordSolve()
    {
        RootWordsLibrary.LoadRootWords();
        Word = RootWordsLibrary.GetRandomWordByTier(GameManager.Instance.Tier);
        if (Word == null )
        {
            Debug.Log($"Unable to find a word for tier {GameManager.Instance.Tier}");
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

    public bool CheckAnswer(string rootAnswer)
    {
        Attempt++;

        if (Attempt > Word.examples.Count)
        {
            OutOfAttempts = true;
            Reset();
            return false;
        }

        if (rootAnswer == Word.root)
        {
            Reset();
            return true;
        }

        ShowHint = Attempt >= 1;

        UsedExamples.Add(Example);
        StartCoroutine(ScrambleExample());
        return false;
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
        OutOfAttempts = false;
        Attempt = 0;
        Hint = null;
        ShowHint = false;
        Example = null;
        MaskedExample = null;
        UsedExamples.Clear();
        Word = null;
    }


    private string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        if (length < 0)
        {
            return "";
        }

        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }


    private string GetNewExample()
    {
        if (UsedExamples.Count == Word.examples.Count)
        {
            Reset();
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
