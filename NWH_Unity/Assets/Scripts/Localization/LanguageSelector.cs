using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using TMPro;
using UnityEngine.Localization.Settings;
using System.Collections.Generic;

public class LanguageSelector : MonoBehaviour
{
    public string LanguageKey = "SelectedLanguage";

    public GameObject selectedLanguage = null;

    [SerializeField] private List<GameObject> _languageList;

    private ToggleUI _toggleUI;

    private void Awake() 
    {
        _toggleUI = GetComponent<ToggleUI>();
       for (int i = 0; i < _languageList.Count; i++)
       {
            selectedLanguage = _languageList[i];
       }
    }

    private void OnDestroy()
    {

    }

    private void Update() 
    {
        GetCurrentLanguageIndex();
    }

  private void GetCurrentLanguageIndex()
  {
    //int currentLanguageIndex = _toggleUI.CurrentIndex;
    SelectLanguage(_toggleUI.CurrentIndex);
  }
    public void SelectLanguage(int language)
    {
        switch (language)
        {
             case 0:
                SetLanguage("en"); // English
                break;
            case 1:
                SetLanguage("es-ES"); // Spanish
                break;
            case 2:
                SetLanguage("fr"); // France
                break;
            case 3:
                SetLanguage("pt-BR"); // Portuguese
                break;
            case 4:
                SetLanguage("de"); // German
                break;
            case 5:
                SetLanguage("pl"); // Polish
                break;
            case 6:
                SetLanguage("ko"); // Korean
                break;
            case 7:
                SetLanguage("ja"); // Japanese
                break;
            case 8:
                SetLanguage("ru"); // Russian
                break;
            case 9:
                SetLanguage("tr"); // Turkish
                break;
             case 10:
                SetLanguage("zh-Hans"); // Chinese
                break;
            default:
                SetLanguage("en"); // Default to English if out of range
                break;
        }
    }

    private void SetLanguage(string localeCode)
    {
        Locale locale = LocalizationSettings.AvailableLocales.GetLocale(localeCode);
        if (locale != null)
        {
            LocalizationSettings.SelectedLocale = locale;
        }
        else
        {
            Debug.LogWarning("Locale not found: " + localeCode);
        }
    }

     private void SaveLanguage(int language)
    {
        PlayerPrefs.SetInt(LanguageKey, language);
        PlayerPrefs.Save();
    }

    private void LoadLanguage()
    {
        int savedLanguage = PlayerPrefs.GetInt(LanguageKey, 0); // Default to 0 (English) if no language saved
       // _languageList.value = savedLanguage;
        SelectLanguage(savedLanguage);
    }
}
