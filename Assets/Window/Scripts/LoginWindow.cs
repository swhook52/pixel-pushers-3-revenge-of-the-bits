using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginWindow : MonoBehaviour
{
    public Transform MachineNameInput;
    public Transform UsernameInput;
    public Transform PasswordInput;
    public Transform HintText;

    public Transform AccessGrantedUi;
    public Transform AccessDeniedUi;

    private string _machineName;
    private string _username;

    private TMP_InputField _machineNameInput;
    private TMP_InputField _usernameInput;
    private PasswordControl _passwordInput;
    private TextMeshProUGUI _hintText;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.LoginWindowComponent = this;
        var user = GameManager.Instance.User;
        if (user != null)
        {
            _machineName = user.MachineName;
            _username = user.Username;
        }

        _machineNameInput = MachineNameInput.GetComponent<TMP_InputField>();
        _usernameInput = UsernameInput.GetComponent<TMP_InputField>();
        _passwordInput = PasswordInput.GetComponent<PasswordControl>();
        _hintText = HintText.GetComponent<TextMeshProUGUI>();

        InitializeLoginWindow(_machineName, _username);
    }

    public void InitializeLoginWindow(string machineName, string username)
    {
        PasswordSolve.Instance.GeneratePasswordSolve();

        _machineNameInput.text = machineName;
        _usernameInput.text = username;
        RefreshPasswordControl();

        if (!string.IsNullOrEmpty(_machineNameInput.text))
        {
            _machineNameInput.readOnly = true;
        }

        if (!string.IsNullOrEmpty(_usernameInput.text))
        {
            _usernameInput.readOnly = true;
        }

    }

    public void RefreshPasswordControl()
    {
        _passwordInput.GeneratePasswordWithMask(PasswordSolve.Instance.MaskedExample);
        _hintText.text = PasswordSolve.Instance.Hint;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SolveWord()
    {
        AccessGrantedUi.gameObject.SetActive(false);
        AccessDeniedUi.gameObject.SetActive(false);

        var correct = PasswordSolve.Instance.CheckAnswer(_passwordInput.Text);
        if (correct)
        {
            AccessGrantedUi.gameObject.SetActive(true);
            GameManager.Instance.Tier++;
            Invoke("HackUser", 4);
        }
        else
        {
            AccessDeniedUi.gameObject.SetActive(true);
            if (PasswordSolve.Instance.OutOfAttempts)
            {
                Invoke("LockUser", 4);
            }
            else
            {
                RefreshPasswordControl();
            }
        }
    }

    public void LockUser()
    {
        Debug.Log("Out of attempts");
        GameManager.Instance.LockedUsers.Add(GameManager.Instance.User);
        SceneManager.LoadScene("OrgTiers");
    }

    public void HackUser()
    {
        GameManager.Instance.Tier++;
        GameManager.Instance.SuccessfulUsers.Add(GameManager.Instance.User);
        if (GameManager.Instance.Tier <= 5)
            SceneManager.LoadScene("OrgTiers");
        else
            SceneManager.LoadScene("WinScene");
    }
}
