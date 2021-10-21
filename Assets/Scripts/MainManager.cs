using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount,perLine;
    public Rigidbody Ball;

    public Text ScoreText , highestScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        LineCount = Mathf.Clamp(MenuDataPersistence.instance.rowNo,2,5);
        perLine = Mathf.Clamp(MenuDataPersistence.instance.coloumNo,2,5);
        //Debug.Log(LineCount+ ": "+ perLine);
        highestScoreText.text = $"Score : {MenuDataPersistence.instance.playerName} : {MainDataPersistence.instance.highScore}";
        //const float step = 0.6f;
        //int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};

        float y = 3.55f/(perLine-1);

        //Debug.Log(y+""+ Screen.width);

        for (int i = 0; i < LineCount; ++i)
        {
            float valueOfX = -1.85f;
            for (int x = 0; x < perLine; x++)
            {
                
                Vector3 position = new Vector3( valueOfX + x*y , 2.5f + i * 0.3f, 0);

                //Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                Debug.Log("hello "+position.x);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
                valueOfX = -1.55f;
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                //highestScoreText.text = $"Score : {MenuDataPersistence.instance.playerName} : {MainDataPersistence.instance.highScore}";
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        if(m_Points>MainDataPersistence.instance.highScore)
        {
            MainDataPersistence.instance.highScore = m_Points;
            MenuDataPersistence.instance.playerName = MenuDataPersistence.instance.currentPlayer;
            MenuDataPersistence.instance.SaveName();
        }
        MenuDataPersistence.instance.SaveTiles();
    }
}
