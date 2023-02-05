using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector]
    public LoginWindow LoginWindowComponent;

    [HideInInspector]
    public int Tier;

    [HideInInspector]
    public User User;

    public List<User> LockedUsers = new List<User>();

    void Awake()
    {
        if (GameManager.Instance != null && GameManager.Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Tier = 1;
    }

    public void RefreshPasswordControl()
    {
        if (LoginWindowComponent != null)
        {
            LoginWindowComponent.RefreshPasswordControl();
        }
    }
}
