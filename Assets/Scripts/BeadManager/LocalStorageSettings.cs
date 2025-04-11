using BeadManager;
using UnityEngine;
using System.IO;

public class LocalStorageSettings : MonoBehaviour
{
    private BeadProperties bP;
    private GameObject floor;
    
    void Start()
    {
        bP = GetComponent<BeadProperties>();
        floor = GameObject.Find("Floor");
        //LoadLocalSettings(); // Load settings on start  
    }

    public void SaveLocalSettings()
    {
        // Only save if value can be found, otherwise skip that variable
        // Save bead locations
        // Save skybox color
        // Save floor game state (active or disabled)
        // Save blink speed
        // Save

        PlayerSettings settings = new PlayerSettings
        {
            redBeadPosition = bP.RedBead.transform.localPosition,
            yellowBeadPosition = bP.YellowBead.transform.localPosition,
            greenBeadPosition = bP.GreenBead.transform.localPosition,
            skyboxColor = Camera.main.backgroundColor, // Adjust as needed for your skybox  
            isFloorActive = floor.activeInHierarchy,
            //blinkSpeed = FindObjectOfType<BeadBlinker>().GetCurrentBlinkSpeed() // Assuming GetCurrentBlinkSpeed() returns the current speed  
        };
        Debug.Log(settings.isFloorActive);

        string json = JsonUtility.ToJson(settings);
        PlayerPrefs.SetString("PlayerSettings", json);
        PlayerPrefs.Save();
    }

    public void LoadLocalSettings()
    {
        // Reload scene with stored settings
        // If variables have no data, skip that variable
        if (PlayerPrefs.HasKey("PlayerSettings"))
        {
            Debug.Log("HasKey PlayerSettings");
            string json = PlayerPrefs.GetString("PlayerSettings");
            PlayerSettings settings = JsonUtility.FromJson<PlayerSettings>(json);

            if (settings != null)
            {
                // Load saved settings safely  
                if (bP.RedBead != null)
                    bP.RedBead.transform.localPosition = settings.redBeadPosition;
                if (bP.YellowBead != null)
                    bP.YellowBead.transform.localPosition = settings.yellowBeadPosition;
                if (bP.GreenBead != null)
                    bP.GreenBead.transform.localPosition = settings.greenBeadPosition;

                if (Camera.main != null)
                {
                    Camera.main.backgroundColor = settings.skyboxColor; // Adjust as needed
                }
                
                if (floor != null)
                    floor.SetActive(settings.isFloorActive);

                Debug.Log(settings.isFloorActive);

                //FindObjectOfType<BeadBlinker>()?.SetBlinkSpeed(settings.blinkSpeed); // Assuming you have a method to set the blink speed
            }
            else
            {
                Debug.LogError("Settings is null");
            }
        }
    }
}
