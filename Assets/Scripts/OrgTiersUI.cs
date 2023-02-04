using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OrgTiersUI : MonoBehaviour
{
    public int ActiveTier = 0;
    List<GroupBox> Tiers = new List<GroupBox>();

    Button ActiveUser = null;
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

        List<Button> AllUsers = root.Query<Button>().ToList();
        AllUsers.ForEach(user => user.RegisterCallback<ClickEvent>(evt => ActiveUser = user));

        UpdateTier(ActiveTier);
    }
    public void UpdateTier(int tier)
    {
        if (tier < 0 || tier >= Tiers.Count) return;

        GroupBox OldTier = Tiers[ActiveTier];
        OldTier.RemoveFromClassList("active-row");
        OldTier.Q<Label>().RemoveFromClassList("active-label");

        ActiveTier = tier;

        GroupBox NewTier = Tiers[ActiveTier];
        NewTier.AddToClassList("active-row");
        NewTier.Q<Label>().AddToClassList("active-label");
    }

    public void DisableUser(Button userButton)
    {
        userButton.style.unityBackgroundImageTintColor = Color.red;
        userButton.SetEnabled(false);
        userButton.AddToClassList("disabled-user");
    }

}
