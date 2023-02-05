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
        { "I1", new User { Tier = 1, Username = "Alani Wilfried", MachineName = "RoAE_Intern_K51A3" } },
        { "I2", new User { Tier = 1, Username = "Melina Lettice", MachineName = "RoAE_Intern_U87A8" } },
        { "I3", new User { Tier = 1, Username = "Aither Fingal", MachineName = "RoAE_Intern_U87A8" } },
        { "I4", new User { Tier = 1, Username = "Willow Ardith", MachineName = "RoAE_Intern_6V04F" } },
        { "I5", new User { Tier = 1, Username = "Cade Hailie", MachineName = "RoAE_Intern_Z0595" } },
        { "A1", new User { Tier = 2, Username = "Gregory Leroy", MachineName = "RoAE_Associate_7648G" } },
        { "A2", new User { Tier = 2, Username = "Thom Bryn", MachineName = "RoAE_Associate_E5T30" } },
        { "A3", new User { Tier = 2, Username = "Sharleen Justy", MachineName = "RoAE_Associate_8O99Y" } },
        { "A4", new User { Tier = 2, Username = "Reba Lorine", MachineName = "RoAE_Associate_0JD32" } },
        { "M1", new User { Tier = 3, Username = "Raylene Caiden", MachineName = "RoAE_Manager_724RM" } },
        { "M2", new User { Tier = 3, Username = "Gage Keitha", MachineName = "RoAE_Manager_28TN2" } },
        { "M3", new User { Tier = 3, Username = "Galilea Snow", MachineName = "RoAE_Manager_874S8" } },
        { "D1", new User { Tier = 4, Username = "Amoura Estella", MachineName = "RoAE_Director_H869M" } },
        { "D2", new User { Tier = 4, Username = "Lise Cedar", MachineName = "RoAE_Director_358XG" } },
        { "CEO", new User { Tier = 5, Username = "Beff Jezos", MachineName = "RoAE_Bo$$Man" } }
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
            userButton.SetEnabled(false);
        });

        UpdateTier();

        GameManager.Instance.LockedUsers.ForEach(user => {
            DisableUser(user);
        });

        GameManager.Instance.SuccessfulUsers.ForEach(user => {
            MarkUserAsSuccessful(user);
        });
    }

    public void UpdateTier()
    {
        Debug.Log("Updating Tier" + GameManager.Instance == null ? "No GameManager" : "HAVE GameManager");
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
        NewTier.RemoveFromClassList("locked-row");
        NewTier.Q<Label>().AddToClassList("active-label");

        RevealUserNames();
    }

    private void RevealUserNames() {

        int currentTier = GameManager.Instance.Tier - 1;
        while (currentTier >= 0)
        {
            List<Button> UserButtonInTier = Tiers[currentTier].Query<Button>().ToList();

            UserButtonInTier.ForEach(userButton => {
                if (currentTier + 1 == GameManager.Instance.Tier) {
                    userButton.SetEnabled(true);
                }
                var user = _users.SingleOrDefault(p => p.Key == userButton.viewDataKey);
                if (user.Value == null)
                {
                    return;
                }
                userButton.Q<Label>().text = user.Value.Username;
            });
            Tiers[currentTier].RemoveFromClassList("locked-row");
            currentTier--;
        }
    }

    public void DisableUser(User user)
    {
        var tierGroup = Tiers[user.Tier - 1];
        var tierGroupLabels = tierGroup.Query<Label>().ToList();
        var userLabel = tierGroupLabels.Find(x => x.text == user.Username);

        var userButton = userLabel.parent;

        List<string> UserClasses = new List<string> { "intern", "associate", "manager", "director" };
        UserClasses.ForEach(userClass => {
            userButton.RemoveFromClassList(userClass);
        });
        userButton.AddToClassList("locked");
        userButton.AddToClassList("disabled-user");
        userButton.style.unityBackgroundImageTintColor = Color.red;

        CheckIfGameIsOver(user);
    }

    public void MarkUserAsSuccessful(User user)
    {
        var tierGroup = Tiers[user.Tier - 1];
        var tierGroupLabels = tierGroup.Query<Label>().ToList();
        var userLabel = tierGroupLabels.Find(x => x.text == user.Username);

        var userButton = userLabel.parent;

        userLabel.text = "* HACKED *<br>" + user.Username;
        userButton.AddToClassList("success");
        userButton.style.unityBackgroundImageTintColor = Color.green;
    }

    public void CheckIfGameIsOver(User user)
    {
        var lockedUserCount = GameManager.Instance.LockedUsers.Count;
        var currentTier = CurrentTier(user);
        int numLockedInTier = 0;

        foreach (User tempUser in GameManager.Instance.LockedUsers)
        {
            if (tempUser.Tier == currentTier) numLockedInTier++;
        }

        var countOfUsersByTier = GameManager.Instance.LockedUsers.FindAll(x => x.Tier == user.Tier).Count;

        if (currentTier == 1)
        {
            if (numLockedInTier==5) CallGameOver();
        }

        if (currentTier == 2)
        {
            if (numLockedInTier==4) CallGameOver();
        }

        if (currentTier == 3)
        {
            if (numLockedInTier==3) CallGameOver();
        }

        if (currentTier == 4)
        {
            if (numLockedInTier==2) CallGameOver();
        }

        if (currentTier == 5)
        {
            if (numLockedInTier==1) CallGameOver();
        }
    }

    public int CurrentTier(User user)
    {
        var tierGroup = Tiers[user.Tier - 1];
        var count = tierGroup.Query<Label>().ToList().Count - 1;

        if (count == 5) return 1;
        if (count == 4) return 2;
        if (count == 3) return 3;
        if (count == 2) return 4;
        if (count == 1) return 5;

        return 0;
    }

    public void CallGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
