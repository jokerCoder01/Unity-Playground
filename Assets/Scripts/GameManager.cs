/*
 * 
 */
 
using UnityEngine;

public class GameManager : MonoBehaviour {

	void Awake()
    {
        LocalizationManager.instance.DetectLanguage();
        LocalizationManager.instance.LoadLanguage(LocalizationManager.instance.Language.ToString());
    }
}