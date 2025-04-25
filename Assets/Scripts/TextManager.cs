using System.Collections.Generic;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public TMP_Text targetText;
    public Button nextButton;
    public Button previousButton;

    // Dictionary to hold text sections  
    private Dictionary<string, string> textMap = new Dictionary<string, string>();
    private List<string> keys;
    private int currentIndex = 0;

    void Start()
    {
        StartCoroutine(LoadTextData());
        nextButton.onClick.AddListener(NextSection);
        previousButton.onClick.AddListener(PreviousSection);
    }

    private IEnumerator LoadTextData()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "textData.json");

        // For WebGL, use UnityWebRequest to load the file  
        using (UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequest.Get(filePath))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityEngine.Networking.UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to load text data: " + www.error);
            }
            else
            {
                string category = DetermineCurrentCategory();
                ProcessJSON(www.downloadHandler.text, category);
            }
        }
    }

    private string DetermineCurrentCategory()
    {
        string currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        if (currentScene == "InformationScene")
        {
            return "information";
        }
        else if (currentScene == "InstructionsScene")
        {
            return "instructions";
        }

        return "information"; // Default category  
    }

    private void ProcessJSON(string json, string category)
    {
        TextData data = JsonUtility.FromJson<TextData>(json);

        // Clear the dictionary before adding new entries  
        textMap.Clear();

        // Depending on the category, populate the dictionary  
        if (category == "information")
        {
            foreach (var entry in data.information)
            {
                textMap[entry.key] = entry.value;
            }
        }
        else if (category == "instructions")
        {
            foreach (var entry in data.instructions)
            {
                textMap[entry.key] = entry.value;
            }
        }

        keys = new List<string>(textMap.Keys);
        UpdateText();
        UpdateButtons();
    }

    private void UpdateText()
    {
        if (keys.Count > 0) // Safeguard against accessing an empty list  
            targetText.text = textMap[keys[currentIndex]];
        //imageDisplay.sprite = imageMap[keys[currentIndex]]; // Update image based on current text section  
    }

    private void UpdateButtons()
    {
        previousButton.interactable = currentIndex > 0;
        nextButton.interactable = currentIndex < keys.Count - 1;
    }

    private void NextSection()
    {
        if (currentIndex < keys.Count - 1)
        {
            currentIndex++;
            UpdateText();
            UpdateButtons();
        }
    }

    private void PreviousSection()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateText();
            UpdateButtons();
        }
    }

    [System.Serializable]
    public class TextEntry
    {
        public string key;
        public string value;
    }

    [System.Serializable]
    public class TextData
    {
        public List<TextEntry> information;
        public List<TextEntry> instructions;
    }
}
