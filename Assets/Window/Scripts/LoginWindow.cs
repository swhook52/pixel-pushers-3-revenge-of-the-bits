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
    public Transform HintText;

    private TMP_InputField _machineNameInput;
    private TMP_InputField _usernameInput;
    private PasswordControl _passwordInput;
    private TextMeshProUGUI _hintText;

    // Start is called before the first frame update
    void Start()
    {
        _machineNameInput = MachineNameInput.GetComponent<TMP_InputField>();
        _usernameInput = UsernameInput.GetComponent<TMP_InputField>();
        _passwordInput = PasswordInput.GetComponent<PasswordControl>();
        _hintText = HintText.GetComponent<TextMeshProUGUI>();

        PasswordSolve.Instance.Tier = 1;
        InitializeLoginWindow(MachineName, Username);
    }

    public void InitializeLoginWindow(string machineName, string username)
    {
        PasswordSolve.Instance.GeneratePasswordSolve();

        _machineNameInput.text = machineName;
        _usernameInput.text = username;
        _passwordInput.GeneratePasswordWithMask(PasswordSolve.Instance.MaskedExample);
        _hintText.text = PasswordSolve.Instance.Word.definition;

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
        Debug.Log(_passwordInput.Text);
    }
}
