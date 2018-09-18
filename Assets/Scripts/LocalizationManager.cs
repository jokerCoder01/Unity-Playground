/*
 * 
 */
 
using UnityEngine;

public class LocalizationManager : MonoBehaviour {

    public TextAsset languageAsset;
    public string languageText;

    private enum LanguageList
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
    private LanguageList language = LanguageList.English;
    private string languageFilePath = "";

    #region Properties
    #endregion

    // Use this for initialization
    void Start()
    {
        DetectLanguage();
        LoadLanguage(languageFilePath);
    }

    private void DetectLanguage()
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

        languageFilePath = "Localization/" + language.ToString();
    }

    private void LoadLanguage(string filePath)
    {
        languageAsset = Resources.Load<TextAsset>(filePath);
        languageText = languageAsset.text;
    }
}