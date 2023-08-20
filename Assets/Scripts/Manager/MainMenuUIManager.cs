using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    public delegate void OnButtonMainDelegate(string _sceneName);

    public OnButtonMainDelegate OnButtonMainCallback
    {
        set => onButtonMainCallback = value;
    }


    private void Awake()
    {
        buttonMain = FindAnyObjectByType<ButtonMain>();
    }

    private void Start()
    {
        buttonMain.GetComponent<Button>().onClick.AddListener(
            () =>
            {
                onButtonMainCallback?.Invoke("Stage");
            }
            );
    }

    private ButtonMain buttonMain = null;
    private OnButtonMainDelegate onButtonMainCallback = null;
}
