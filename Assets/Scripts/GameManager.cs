using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [HideInInspector]
    public LoginWindow LoginWindowComponent;

    [HideInInspector]
    public int Tier;

    [HideInInspector]
    public User User;

    public List<User> LockedUsers = new List<User>();
    public List<User> SuccessfulUsers = new List<User>();

    void Awake()
    {
        Debug.Log("GameManager Awake");
        if (GameManager.Instance != null && GameManager.Instance != this)
        {
            Debug.Log("Duplicate GameManager found. Removing other GameManager");
            Destroy(this);
            return;
        }
        else
        {
            Debug.Log("Creating game manager instance");
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

    public void SolveWord()
    {
        if (LoginWindowComponent != null)
        {
            LoginWindowComponent.SolveWord();
        }
    }
}
