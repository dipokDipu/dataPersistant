using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{

    private void Awake()
    {
        if(MenuDataPersistence.instance.playerName!= null)
        {
            GetComponentInChildren<InputField>().text = MenuDataPersistence.instance.playerName;
        }
    }

    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        MenuDataPersistence.instance.currentPlayer = GetComponentInChildren<InputField>().text;
        SceneManager.LoadScene(1);
        //Debug.Log(playername);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
