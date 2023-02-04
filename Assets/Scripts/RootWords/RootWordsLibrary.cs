using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;

public static class RootWordsLibrary
{
    public static RootWordsModel RootWords { get; set; }

    public static void LoadRootWords()
    {
        using (StreamReader r = new StreamReader("rootWordsData.json"))
        {
            string json = r.ReadToEnd();
            RootWords = JsonUtility.FromJson<RootWordsModel>(json);
        }
    }

    public static void DoSomething()
    {

    }
}
