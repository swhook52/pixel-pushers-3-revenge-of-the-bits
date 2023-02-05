using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TitleScreenUI : MonoBehaviour
{

    public AudioSource clickSound;

    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {  
    }

    private void OnEnable() {
        var root = GetComponent<UIDocument>().rootVisualElement;

        Button StartButton = root.Q<Button>("StartButton");
        Button QuitButton = root.Q<Button>("QuitButton");



        StartButton.RegisterCallback<ClickEvent>(evt => {
            SceneManager.LoadScene("IntroScene");
            clickSound.Play();
        });

        StartButton.RegisterCallback<MouseEnterEvent>(evt => {
            StartButton.style.opacity = 0.5f;
        });

        StartButton.RegisterCallback<MouseLeaveEvent>(evt => {
            StartButton.style.opacity = 1f;
        });



        QuitButton.RegisterCallback<ClickEvent>(evt => {
            clickSound.Play();
            Application.Quit();
        });

        QuitButton.RegisterCallback<MouseEnterEvent>(evt => {
            QuitButton.style.opacity = 0.5f;
        });

        QuitButton.RegisterCallback<MouseLeaveEvent>(evt => {
            QuitButton.style.opacity = 1f;
        });

    }
}
