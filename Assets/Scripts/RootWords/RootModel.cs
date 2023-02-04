using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RootModel : MonoBehaviour
{
    public string root { get; set; }
    public string definition { get; set; }
    public List<string> examples { get; set; }
}
