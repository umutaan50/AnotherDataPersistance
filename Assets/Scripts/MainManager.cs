using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
// Project name: AnotherDataPersistence
public class MainManager : MonoBehaviour
{
    public StartManager startManager;

    public void SaveMethodFromStartManager()
    {
        startManager.SaveScore();
    }
    // Trial

    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text previouslyScore;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    public int m_Points;
    
    private bool m_GameOver = false;

    public string playerName;
    public Text playerNameText;

    // Copied from previous project:
    public static MainManager Instance;

    public Button myButton; 

    private void Awake()
    {

        startManager = GameObject.Find("Start Manager").GetComponent<StartManager>();
        if (Instance != null)
        {
            DestroyObject(gameObject);
            return;

        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        AssignTexts();
        //previouslyScore.text = StartManager.Instance.previousSession.text;
    }

    public void AssignTexts()
    {
        playerNameText.text = StartManager.Instance.theName;
        Debug.Log(StartManager.Instance.previousTotalText);
        previouslyScore.text = StartManager.Instance.previousTotalText;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }


    // Start is called before the first frame update
    public void Start()
    {
        BuildBricks();
        
    }

    public void BuildBricks()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.gameObject.tag = "Brick";
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        // Trial
        

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
                previouslyScore.text = StartManager.Instance.previousSession.text;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

                Awake();
                //Start();
                //Debug.Log("Çalıştım ben!");

            }
        }
    }


    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        StartManager.Instance.previousTotalText = previouslyScore.text;
        //StartManager.Instance.AssignOnClick();
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        if (StartManager.Instance.previousPScore < m_Points)
        {

            StartManager.Instance.SaveScore();

            previouslyScore.text = "New highscore: " + StartManager.Instance.theName + " " + m_Points + "!";
            // Or we could use playerNameText.text above.
        }

        StartManager.Instance.LoadScore();
        m_GameOver = true;
        GameOverText.SetActive(true);
    }
}

// Previous effort:

    //[System.Serializable]
    //class SaveData
    //{
    //    // A small class that only contains the specific data that I want to save.      
    //    public string playerName;


    //}

    //public void SaveSomething()
    //{
    //    SaveData data = new SaveData();
    //    data.playerName = playerName;

    //    string json = JsonUtility.ToJson(data);

    //    File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    //}

    //public void LoadSomething()
    //{
    //    string path = Application.persistentDataPath + "/savefile.json";
    //    if (File.Exists(path))
    //    {
    //        string json = File.ReadAllText(path);
    //        SaveData data = JsonUtility.FromJson<SaveData>(json);

    //        playerName = data.playerName;

    //        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

    //    }
    //}