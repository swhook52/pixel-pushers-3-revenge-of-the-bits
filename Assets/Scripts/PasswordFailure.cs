using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PasswordFailure : MonoBehaviour
{
    public Text attemptsText;
    public Text resetText;
    PasswordSolve ps;
    public int maxAttempts = 3;

    // Start is called before the first frame update
    void Start()
    {
        attemptsText.text = "Attempts Remaining: " + (maxAttempts - 3);         //set this to (maxAttempts - PasswordSolve.Attempt)
        if(maxAttempts - 3 == 0)                                                //set this to (maxAttempts - PasswordSolve.Attempt)
        {
            resetText.text = "Locking Account...";
        }
        else
        {
            resetText.text = "Resetting Password...";
        }
    }
}
