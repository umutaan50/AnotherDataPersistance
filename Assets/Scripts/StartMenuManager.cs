using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    public Button myButton;
    public static StartMenuManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;

        }

        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void AssignOnClick()
    //{
    //    myButton = GameObject.Find("Start Button").GetComponent<Button>();
    //    myButton.onClick.AddListener(StartGame);
    //}

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        MainManager.Instance.Start();
        // Debug.Log("StartGame worked.");
    }
}
