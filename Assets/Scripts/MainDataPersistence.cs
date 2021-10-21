using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainDataPersistence : MonoBehaviour
{
    // Start is called before the first frame update

    public static MainDataPersistence instance;
    public int highScore;
    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(instance);

        highScore = MenuDataPersistence.instance.highPoint;
    }
}
