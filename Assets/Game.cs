using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    private Button _myButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SoundManager.instance.PlayMusic(SoundManager.instance.gameplayMusic);

        _myButton = GameObject.Find("MyButton").GetComponent<Button>();
        _myButton.onClick.AddListener(OnMyButtonClick);

        Invoke(nameof(CallSomething2), 2);
    }

    private void OnMyButtonClick()
    {
        Debug.Log("my button click");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CallSomething2()
    {
        Debug.Log("called");
    }

    public void OnButton1Click()
    {
        Debug.Log("button 1 click");
    }
}
