using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class OrgTiersUI : MonoBehaviour
{
    List<GroupBox> Tiers = new List<GroupBox>();

    private Dictionary<string, User> _users = new Dictionary<string, User>
    {
        { "I1", new User { Tier = 1, Username = "intern", MachineName = "ROAE_Intern_001" } },
        { "A1", new User { Tier = 2, Username = "associate", MachineName = "ROAE_Associate_001" } },
        { "M1", new User { Tier = 3, Username = "manager", MachineName = "ROAE_Manager_001" } },
        { "D1", new User { Tier = 4, Username = "director", MachineName = "ROAE_Director_001" } },
        { "CEO", new User { Tier = 5, Username = "ceo", MachineName = "ROAE_Ceo" } }
    };

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
        AllUsers.ForEach(userButton => {
            var user = _users.SingleOrDefault(p => p.Key == userButton.viewDataKey);
            if (user.Value == null)
            {
                return;
            }

            userButton.RegisterCallback<ClickEvent>(evt => {
                Debug.Log(user.Value.MachineName);
                GameManager.Instance.Tier = user.Value.Tier;
                GameManager.Instance.User = user.Value;
                SceneManager.LoadScene("MainScene");
            });
        });

        UpdateTier();
    }
    public void UpdateTier()
    {
        Debug.Log(GameManager.Instance.Tier);

        var activeTierIndex = GameManager.Instance.Tier - 1;
        if (activeTierIndex >= Tiers.Count) return;

        if (activeTierIndex > 0)
        {
            GroupBox OldTier = Tiers[activeTierIndex - 1];
            OldTier.RemoveFromClassList("active-row");
            OldTier.Q<Label>().RemoveFromClassList("active-label");
        }

        GroupBox NewTier = Tiers[activeTierIndex];
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
