/*
 * 
 */

using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalizationManager : MonoBehaviour {

    public static LocalizationManager instance;
    public enum LanguageList
    {
        English,
        Turkish,
        ChinesSimplified,
        Arabic,
        French,
        Italian,
        German,
        Spanish,
        Russian,
        Portuguese,
        Japanese,
        Korean,
    }

    private Dictionary<string, string> localizedText;
    private LanguageList language = LanguageList.English;
    private string missingTextString = "找不到文字！";

    public LanguageList Language { get { return language; } set { language = value; } }

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    } 

    public void DetectLanguage()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.English:
                language = LanguageList.English;
                break;
            case SystemLanguage.Turkish:
                language = LanguageList.Turkish;
                break;
            case SystemLanguage.ChineseSimplified:
                language = LanguageList.ChinesSimplified;
                break;
            case SystemLanguage.Arabic:
                language = LanguageList.Arabic;
                break;
            case SystemLanguage.French:
                language = LanguageList.French;
                break;
            case SystemLanguage.Italian:
                language = LanguageList.Italian;
                break;
            case SystemLanguage.German:
                language = LanguageList.German;
                break;
            case SystemLanguage.Spanish:
                language = LanguageList.Spanish;
                break;
            case SystemLanguage.Russian:
                language = LanguageList.Russian;
                break;
            case SystemLanguage.Portuguese:
                language = LanguageList.Portuguese;
                break;
            case SystemLanguage.Japanese:
                language = LanguageList.Japanese;
                break;
            case SystemLanguage.Korean:
                language = LanguageList.Korean;
                break;
            default:
                language = LanguageList.English;
                break;
        }
    }

    public void LoadLanguage(string fileName)
    {
        localizedText = new Dictionary<string, string>();
        string filePath = Application.streamingAssetsPath + @"/" + fileName + ".json";

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

            for (int i = 0; i < loadedData.items.Length; i++)
            {
                localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
            }

            Debug.Log("Data loaded, dictionary contains: " + localizedText.Count + " entries");
        }
        else
        {
            Debug.LogError("Cannot find file!");
        }

    }

    public string GetLocalizedValue(string key)
    {
        string result = missingTextString;
        if (localizedText.ContainsKey(key))
            result = localizedText[key];

        return result;
    }
}