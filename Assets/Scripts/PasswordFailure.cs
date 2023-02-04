using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PasswordFailure : MonoBehaviour
{
    public Text attemptsText;
    PasswordSolve ps;
    int maxAttempts = 3;

    // Start is called before the first frame update
    void Start()
    {
        attemptsText.text = "Attempts Remaining: " + (0); //set this to (maxAttempts - PasswordSolve.Attempt)
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
