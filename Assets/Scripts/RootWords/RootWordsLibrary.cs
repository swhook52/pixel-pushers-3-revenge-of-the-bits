using System;
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
        var tierWords = RootWords.rootWords.Where(word => word.tier == tier).ToArray();
        
        if (tierWords.Length > 0)
        {
            var random = new System.Random();
            var randomIndex = random.Next(0, tierWords.Length);
            return tierWords[randomIndex];
        }

        return null;
    }

    public static void DoSomething()
    {

    }
}
