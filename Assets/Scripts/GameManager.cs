using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector]
    public LoginWindow LoginWindowComponent;

    [HideInInspector]
    public int Tier = 1;

    [HideInInspector]
    public User User;

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
    }
}