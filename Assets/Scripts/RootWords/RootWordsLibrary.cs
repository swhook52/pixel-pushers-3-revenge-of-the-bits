using UnityEngine;

public static class RootWordsLibrary
{
    public static RootWordsModel RootWords { get; set; }

    public static void LoadRootWords()
    {
        var jsonTextFile = Resources.Load<TextAsset>("rootWordsData");
        RootWords = JsonUtility.FromJson<RootWordsModel>(jsonTextFile.text);
    }

    public static void DoSomething()
    {

    }
}
