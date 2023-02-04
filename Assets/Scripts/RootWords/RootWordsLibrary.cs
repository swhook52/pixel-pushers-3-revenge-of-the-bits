using System.Linq;
using UnityEngine;

public static class RootWordsLibrary
{
    public static RootWordsModel RootWords { get; set; }

    public static void LoadRootWords()
    {
        var jsonTextFile = Resources.Load<TextAsset>("rootWordsData");
        RootWords = JsonUtility.FromJson<RootWordsModel>(jsonTextFile.text);
    }

    public static RootModel GetRandomWordByTier(int tier)
    {
        var tierWords = RootWords.rootWords.Where(x => x.tier == tier);
        if (tierWords.Any())
        {
            var random = new System.Random();
            var randomIndex = random.Next(0, tierWords.Count());
            return RootWords.rootWords[randomIndex];
        }

        return null;
    }

    public static void DoSomething()
    {

    }
}
