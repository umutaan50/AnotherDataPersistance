using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// Project name: AnotherDataPersistence
public class VolumeManager : MonoBehaviour
{

    [SerializeField] Slider volumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume")) 
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();

        }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    public void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }
    
    public void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
