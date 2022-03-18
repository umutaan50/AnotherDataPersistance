using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
// Project name: AnotherDataPersistence
public class StartManager : MonoBehaviour
{
   // You've heard that smaller scripts are better that one script that does everything.


    public string previousPName;
    public int previousPScore;
    public string previousTotalText;
    public string theName;
    public TMP_InputField inputField;
    public TextMeshProUGUI previousSession;

    

    // 3.17.22, 11:55
    public static StartManager Instance;

    public void StoreName()
    {
        theName = inputField.text;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;

        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadScore();


    }

    // Start is called before the first frame update
    void Start()
    {
        
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }


    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public int playerScore;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.playerName = theName;
        data.playerScore = MainManager.Instance.m_Points;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            Debug.Log("File exists!");
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            if (data.playerScore > previousPScore)
            {

                previousPName = data.playerName;
                previousPScore = data.playerScore;
            }


            previousTotalText = "Highscore: " + previousPName + ", " + previousPScore;

            previousSession.text = previousTotalText;

        }
    }
}
