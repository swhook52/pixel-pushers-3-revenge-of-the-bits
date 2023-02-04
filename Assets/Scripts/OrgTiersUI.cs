using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OrgTiersUI : MonoBehaviour
{
    int ActiveTier = 1;
    List<GroupBox> Tiers = new List<GroupBox>();
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {  
    }

    private void OnEnable() {
        var root = GetComponent<UIDocument>().rootVisualElement;

        GroupBox InternRow = root.Q<GroupBox>("intern");
        GroupBox AssociateRow = root.Q<GroupBox>("associate");
        GroupBox ManagerRow = root.Q<GroupBox>("manager");
        GroupBox DirectorRow = root.Q<GroupBox>("director");
        GroupBox CeoRow = root.Q<GroupBox>("ceo");
        
        Tiers.Add(InternRow);
        Tiers.Add(AssociateRow);
        Tiers.Add(ManagerRow);
        Tiers.Add(DirectorRow);
        Tiers.Add(CeoRow);

        // Debug.Log(ActiveTier);
        // Debug.Log(Tiers.Count);

        Tiers[ActiveTier].AddToClassList("active-row");
    }
}
