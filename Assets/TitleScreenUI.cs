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
        var main = root.Q<VisualElement>("Main");
        var credits = root.Q<VisualElement>("Credits");

        Button StartButton = root.Q<Button>("StartButton");
        Button QuitButton = root.Q<Button>("QuitButton");
        Button CreditsButton = root.Q<Button>("CreditsButton");
        Button CloseButton = root.Q<Button>("CloseButton");



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


        CreditsButton.RegisterCallback<ClickEvent>(evt => {
            main.AddToClassList("hidden");
            credits.RemoveFromClassList("hidden");
            clickSound.Play();
        });

        CreditsButton.RegisterCallback<MouseEnterEvent>(evt => {
            CreditsButton.style.opacity = 0.5f;
        });

        CreditsButton.RegisterCallback<MouseLeaveEvent>(evt => {
            CreditsButton.style.opacity = 1f;
        });



        CloseButton.RegisterCallback<ClickEvent>(evt => {
            main.RemoveFromClassList("hidden");
            credits.AddToClassList("hidden");
            clickSound.Play();
        });

        CloseButton.RegisterCallback<MouseEnterEvent>(evt => {
            QuitButton.style.opacity = 0.5f;
        });

        CloseButton.RegisterCallback<MouseLeaveEvent>(evt => {
            QuitButton.style.opacity = 1f;
        });

    }
}
