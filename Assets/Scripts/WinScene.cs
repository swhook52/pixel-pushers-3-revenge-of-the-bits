using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenu()
    {
        GameManager.Instance.Tier = 1;
        GameManager.Instance.LockedUsers.Clear();
        GameManager.Instance.SuccessfulUsers.Clear();
        SceneManager.LoadScene("StartMenu");
    }
}
