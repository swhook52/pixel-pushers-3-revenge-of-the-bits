using TMPro;
using UnityEngine;

public class LoginWindow : MonoBehaviour
{
    public string MachineName;
    public string Username;
    public string Tier;

    public Transform MachineNameInput;
    public Transform UsernameInput;
    public Transform PasswordInput;

    private TMP_InputField _machineNameInput;
    private TMP_InputField _usernameInput;
    private TMP_InputField _passwordInput;

    // Start is called before the first frame update
    void Start()
    {
        _machineNameInput = MachineNameInput.GetComponent<TMP_InputField>();
        _usernameInput = UsernameInput.GetComponent<TMP_InputField>();
        _passwordInput = PasswordInput.GetComponent<TMP_InputField>();

        ResetLoginWindow(MachineName, Username, "");
    }

    public void ResetLoginWindow(string machineName, string username, string password)
    {
        _machineNameInput.text = machineName;
        _usernameInput.text = username;
        _passwordInput.text = "";

        if (!string.IsNullOrEmpty(_machineNameInput.text))
        {
            _machineNameInput.readOnly = true;
        }

        if (!string.IsNullOrEmpty(_usernameInput.text))
        {
            _usernameInput.readOnly = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SolveWord()
    {
        Debug.Log(_passwordInput.text);
    }
}
