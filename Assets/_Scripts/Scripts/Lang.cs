#pragma warning disable IDE0044, IDE1006

using UnityEngine;
using System.Collections.Generic; // list and dictionary
#if UNITY_EDITOR
using UnityEditor;
using System.IO; // created file in editor
#endif

public class Lang
{

    private const string Path = "/Resources/"; // path to resources folder
    private const string FileName = "Language"; // file name with phrases

    private static int NumberOfLanguage; // variable to store the index of the current language 
    private static List<SystemLanguage> languages = new List<SystemLanguage>(); // having languages in game
    private static Dictionary<string, string> Phrases = new Dictionary<string, string>(); // keys and values
    private static SystemLanguage language; // current language
#if UNITY_EDITOR
    public static void Starting(SystemLanguage _language, SystemLanguage default_language, params SystemLanguage[] _languages) // write languages without main language, it self added
#else
    public static void Starting(SystemLanguage _language = SystemLanguage.English) // main language - only for compilation
#endif
    {
#if UNITY_EDITOR
        if (!File.Exists(Application.dataPath + Path + FileName + ".csv")) // if file wasn't created
        {
            File.Create(Application.dataPath + "/Resources/" + FileName + ".csv").Dispose(); // create and lose link
            File.WriteAllText(Application.dataPath + "/Resources/" + FileName + ".csv", SetLanguage(default_language, _languages)); // write default text with index
        }
#endif
        string[] PhrasesArr = Resources.Load<TextAsset>(FileName).text.Split('\n'); // temp var for write in dicrionary

        string[] string_languages = PhrasesArr[0].Split(';'); // string with using languages
        int _length = string_languages.Length - 1;
        for (int i = 0; i < _length; i++)
        {
            languages.Add(SystemLanguageParse(string_languages[i])); // string language to SystemLanguage
        }

        NumberOfLanguage = FindIndexLanguage(_language); // index with current language
        for (int i = 0; i < PhrasesArr.Length; i++) // add keys and value
        {
            string[] temp_string = PhrasesArr[i].Split(';');
            if (temp_string.Length > NumberOfLanguage)
                Phrases.Add(temp_string[0], temp_string[NumberOfLanguage]);
            else Phrases.Add(temp_string[0], temp_string[0]);
        }
        isStarting = true;
    }

    public static bool isStarting // bool for check starting
    {
        get; private set;
    }

    public static SystemLanguage Language // return current language
    {
        get { return language; }
    }

    public static string Phrase(string DefaultPhrase, params object[] args) // translate phrase, args use to formating string
    {
#if UNITY_EDITOR
        if (!isStarting) // if not starting
        {
            throw new System.Exception("Forgot initialization.Use Lang.Starting(...)"); // throw exception
        }
#endif
        string temp_EnglishPhrase = DefaultPhrase; // temp variable for try get value

        if (Phrases.TryGetValue(DefaultPhrase, out DefaultPhrase)) // if value has been found
        {
            if (args.Length == 0)
                return DefaultPhrase;
            return string.Format(DefaultPhrase, args);
        }
#if UNITY_EDITOR
        Add(temp_EnglishPhrase); // add phrase if value hasn't been found
#endif
        if (args.Length == 0)
            return temp_EnglishPhrase;
        return string.Format(temp_EnglishPhrase, args);
    }

    public static void ChangeLanguage(SystemLanguage _language) // change language
    {
        string[] PhrasesArr = Resources.Load<TextAsset>(FileName).text.Split('\n'); // load all text from file

        NumberOfLanguage = FindIndexLanguage(_language);
        Phrases.Clear(); // clear dictionary with phrases
        for (int i = 1; i < PhrasesArr.Length; i++)
        {
            string[] temp_string = PhrasesArr[i].Split(';');
            if (temp_string.Length > NumberOfLanguage)
                Phrases.Add(temp_string[0], temp_string[NumberOfLanguage]);
            else Phrases.Add(temp_string[0], temp_string[0]);
        }
    }

    private static int FindIndexLanguage(SystemLanguage _language) // finding index or current language
    {
        int _index = languages.IndexOf(_language);
        if (_index == -1) // if language not found
            return 0; // return main language
        return _index;
    }

#if UNITY_EDITOR
    private static void Add(string AddString) // add phrases only form editor
    {
        File.AppendAllText(Application.dataPath + "/Resources/" + FileName + ".csv", AddString + "\n"); // rewrite text to file
        Phrases.Add(AddString, AddString); // add phrase to dicrionary

        AssetDatabase.Refresh(); // refresh file 
    }
#endif

#if UNITY_EDITOR
    private static string SetLanguage(SystemLanguage default_language, params SystemLanguage[] _languages) // set first string to file
    {
        string ret_string = "";
        ret_string += default_language + ";";
        foreach (SystemLanguage _language in _languages)
        {
            ret_string += _language + ";";
        }
        return ret_string + "!@#$%\n"; // for last index
    }
#endif
    private static SystemLanguage SystemLanguageParse(string _language) // just parse from string to SystemLanguage
    {
        switch (_language)
        {
            case "English": return SystemLanguage.English;
            case "Russian": return SystemLanguage.Russian;
            case "Ukrainian": return SystemLanguage.Ukrainian;
            case "Polish": return SystemLanguage.Polish;
            case "French": return SystemLanguage.French;
            case "Japanese": return SystemLanguage.Japanese;
            case "Chinese": return SystemLanguage.Chinese;
            case "Afrikaans": return SystemLanguage.Afrikaans;
            case "Arabic": return SystemLanguage.Arabic;
            case "Basque": return SystemLanguage.Basque;
            case "Belarusian": return SystemLanguage.Belarusian;
            case "Bulgarian": return SystemLanguage.Bulgarian;
            case "ChineseSimplified": return SystemLanguage.ChineseSimplified;
            case "ChineseTraditional": return SystemLanguage.ChineseTraditional;
            case "Czech": return SystemLanguage.Czech;
            case "Danish": return SystemLanguage.Danish;
            case "Dutch": return SystemLanguage.Dutch;
            case "Estonian": return SystemLanguage.Estonian;
            case "Faroese": return SystemLanguage.Faroese;
            case "Finnish": return SystemLanguage.Finnish;
            case "German": return SystemLanguage.German;
            case "Greek": return SystemLanguage.Greek;
            case "Hebrew": return SystemLanguage.Hebrew;
            case "Hungarian": return SystemLanguage.Hungarian;
            case "Icelandic": return SystemLanguage.Icelandic;
            case "Indonesian": return SystemLanguage.Indonesian;
            case "Italian": return SystemLanguage.Italian;
            case "Korean": return SystemLanguage.Korean;
            case "Latvian": return SystemLanguage.Latvian;
            case "Lithuanian": return SystemLanguage.Lithuanian;
            case "Norwegian": return SystemLanguage.Norwegian;
            case "Portuguese": return SystemLanguage.Portuguese;
            case "Romanian": return SystemLanguage.Romanian;
            case "SerboCroatian": return SystemLanguage.SerboCroatian;
            case "Slovak": return SystemLanguage.Slovak;
            case "Slovenian": return SystemLanguage.Slovenian;
            case "Spanish": return SystemLanguage.Spanish;
            case "Swedish": return SystemLanguage.Swedish;
            case "Thai": return SystemLanguage.Thai;
            case "Turkish": return SystemLanguage.Turkish;
            case "Vietnamese": return SystemLanguage.Vietnamese;
        }
        return SystemLanguage.Unknown;
    }
}
