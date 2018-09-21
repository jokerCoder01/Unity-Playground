/*
 * 
 */
 
using UnityEngine;
using UnityEngine.UI;

public class LocalizationText : MonoBehaviour {

    public string key;

    void Start()
    {
        Text text = GetComponent<Text>();
        text.text = LocalizationManager.instance.GetLocalizedValue(key);
    }
}