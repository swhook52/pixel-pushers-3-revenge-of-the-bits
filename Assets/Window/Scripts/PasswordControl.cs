using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PasswordControl : MonoBehaviour
{
    public char maskedCharacter = '*';
    public GameObject InputPrefab;
    public GameObject LabelPrefab;
    public Button SubmitButton;

    private List<TMP_InputField> _inputControls = new List<TMP_InputField>();
    private int inputIndex = 0;

    private KeyCode[] _letters = new KeyCode[] {
        KeyCode.A,
        KeyCode.B,
        KeyCode.C,
        KeyCode.D,
        KeyCode.E,
        KeyCode.F,
        KeyCode.G,
        KeyCode.H,
        KeyCode.I,
        KeyCode.J,
        KeyCode.K,
        KeyCode.L,
        KeyCode.M,
        KeyCode.N,
        KeyCode.O,
        KeyCode.P,
        KeyCode.Q,
        KeyCode.R,
        KeyCode.S,
        KeyCode.T,
        KeyCode.U,
        KeyCode.V,
        KeyCode.W,
        KeyCode.X,
        KeyCode.Y,
        KeyCode.Z,
    };

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.None))
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (SubmitButton != null)
            {
                SubmitButton.onClick.Invoke();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveToPreviousInput();
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("tabbing");
            if (SubmitButton != null)
            {
                SubmitButton.Select();
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveToNextInput();
        }
        else
        {
            // Look at all letters to determine if the user pressed a letter
            foreach (var letter in _letters)
            {
                if (Input.GetKeyDown(letter))
                {
                    MoveToNextInput();
                    return;
                }
            }
        }
    }

    private void MoveToPreviousInput()
    {
        if (inputIndex <= 0)
        {
            inputIndex = _inputControls.Count;
        }
        inputIndex--;
        _inputControls[inputIndex].Select();
    }

    private void MoveToNextInput()
    {
        if (_inputControls.Count <= inputIndex + 1)
        {
            inputIndex = -1;
        }
        inputIndex++;
        _inputControls[inputIndex].Select();
    }

    /// <summary>
    /// Returns the root word that was provided into the textboxes
    /// </summary>
    public string Text
    {
        get
        {
            return _inputControls.Select(p => p.text).Aggregate((a, b) => a + b);
        }
    }

    public void GeneratePasswordWithMask(string maskedWord)
    {
        for (int i = 0; i < maskedWord.Length; i++)
        {
            char character = maskedWord[i];
            if (character == maskedCharacter)
            {
                var control = Instantiate(InputPrefab, new Vector3(0, 0, 0), Quaternion.identity, transform);
                _inputControls.Add(control.GetComponent<TMP_InputField>());
            }
            else
            {
                var control = Instantiate(LabelPrefab, new Vector3(0, 0, 0), Quaternion.identity, this.transform);
                control.transform.GetComponent<TextMeshProUGUI>().text = maskedWord[i].ToString();
            }
        }

        if (_inputControls.Count > 0)
        {
            _inputControls[0].Select();
        }
    }
}