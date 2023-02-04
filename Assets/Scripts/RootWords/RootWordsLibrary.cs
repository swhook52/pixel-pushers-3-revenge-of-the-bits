using UnityEngine;

public static class RootWordsLibrary
{
    public static RootWordsModel RootWords { get; set; }

    public static void LoadRootWords()
    {
        var jsonTextFile = Resources.Load<TextAsset>("rootWordsData");
        RootWords = JsonUtility.FromJson<RootWordsModel>(jsonTextFile.text);
    }

    public static RootModel GetWordByTier(int tier)
    {
        return RootWords.rootWords.Find(x => x.tier == tier);
    }

    public static void DoSomething()
    {

    }
}
