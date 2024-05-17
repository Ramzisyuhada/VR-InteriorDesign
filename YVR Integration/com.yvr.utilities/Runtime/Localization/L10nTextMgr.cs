using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton Manager used for text localization
/// </summary>
public class L10nTextMgr : Singleton<L10nTextMgr>
{
    private const string NOT_Found_TAG = "NOT_FOUND";

    private string fontPath = "Font";

    private L10nTextData l10nTextData = null;

    private L10nFontData l10NFontData = null;

    /// <summary>
    /// ID to target language text dictionary
    /// </summary>
    public Dictionary<string, string> id2TargetLangDict { get; private set; }

    /// <summary>
    /// ID to fallback language text dictionary
    /// </summary>
    public Dictionary<string, string> id2FallbackLangDict { get; private set; }

    /// <summary>
    /// Font name to font resource dictionary
    /// </summary>
    public Dictionary<string, Font> name2FontDict = null;

    /// <summary>
    /// ID to font name dictionary
    /// </summary>
    public Dictionary<string, string> id2FontNameDict = null;

    /// <summary>
    /// Target language type
    /// </summary>
    public L10nLangType targetLang { get; private set; }

    /// <summary>
    /// Fallback language type
    /// </summary>
    public L10nLangType fallbackLang { get; private set; }


    /// <summary>
    /// Init L10nTextMgr with json file path and target/fallback language type
    /// </summary>
    /// <param name="localizationTextPath"> localization json file path in the Resources folder</param>
    /// <param name="targetLang"> Target Language </param>
    /// <param name="fallbackLang"> Fallback Language </param>
    public void Init(string localizationTextPath, L10nLangType targetLang = L10nLangType.zh_Hans_CN, L10nLangType fallbackLang = L10nLangType.zh_Hans_CN)
    {
        TextAsset l10nTextAsset = Resources.Load<TextAsset>(localizationTextPath);
        if (l10nTextAsset == null) Debug.LogError("Error: Can not find target localization text: " + localizationTextPath);

        l10nTextData = JsonUtility.FromJson<L10nTextData>(l10nTextAsset.text);

        SetLangType(targetLang, fallbackLang);
        if (l10NFontData != null)
            id2FontNameDict = GenerateID2FontNameDict(this.targetLang);
    }

    /// <summary>
    ///  Init L10nFontNameDict with json file path by target Language
    /// </summary>
    /// <param name="localizationFontPath"> localization json file path relative to Resources folder </param>
    /// <param name="fontResourcePath">Font file path relative to Resources folder </param>
    /// <param name="targetLang"> Target Language </param>
    public void InitL10nFont(string localizationFontPath = null, string fontResourcePath = null)
    {
        if (localizationFontPath == null || fontResourcePath == null) return;
        fontPath = fontResourcePath;

        TextAsset l10nFontAsset = null;
        l10nFontAsset = Resources.Load<TextAsset>(localizationFontPath);
        if (l10nFontAsset == null)
            Debug.LogError("Error: Can not find target localization font: " + l10nFontAsset);
        l10NFontData = JsonUtility.FromJson<L10nFontData>(l10nFontAsset.text);

        id2FontNameDict = GenerateID2FontNameDict(this.targetLang);
        name2FontDict = new Dictionary<string, Font>();
    }

    /// <summary>
    /// Set target and fallback language type
    /// </summary>
    /// <param name="targetLang"> Target language type</param>
    /// <param name="fallbackLang"> Fallback language type </param>
    public void SetLangType(L10nLangType targetLang, L10nLangType fallbackLang = L10nLangType.Keep)
    {
        this.targetLang = targetLang;
        if (fallbackLang != L10nLangType.Keep)
            this.fallbackLang = fallbackLang;

        id2TargetLangDict = GenerateID2TextDict(this.targetLang);
        id2FallbackLangDict = GenerateID2TextDict(this.fallbackLang);
    }

    /// <summary>
    /// Get localized text
    /// </summary>
    /// <param name="key">Text Id</param>
    /// <param name="allowEmpty">Whether allow result to be empty string. If not and the result is empty, the result will be replaced with error msg</param>
    /// <returns></returns>
    public string GetTranslation(string key, bool allowEmpty = false)
    {
        string result = GetText(key, NOT_Found_TAG);
        if (result == NOT_Found_TAG)
            result = string.Format("Key: {0} with target {1} and fallback {1}", key, targetLang.ToString(), fallbackLang.ToString());

        return result;
    }

    private Dictionary<string, string> GenerateID2TextDict(L10nLangType langType)
    {
        Dictionary<string, string> result = new Dictionary<string, string>();

        if (langType == L10nLangType.zh_Hans_CN)
            result = l10nTextData.languagePacks.ToDictionary(pack => pack.id, pack => pack.zh_Hans_CN);
        else if (langType == L10nLangType.en_US)
            result = l10nTextData.languagePacks.ToDictionary(pack => pack.id, pack => pack.en_US);

        return result;
    }

    private Dictionary<string, string> GenerateID2FontNameDict(L10nLangType langType)
    {
        Dictionary<string, string> result = new Dictionary<string, string>();
        if (langType == L10nLangType.zh_Hans_CN)
            result = l10NFontData.fontPacks.ToDictionary(pack => pack.id, pack => pack.zh_Hans_CN);
        else if (langType == L10nLangType.en_US)
            result = l10NFontData.fontPacks.ToDictionary(pack => pack.id, pack => pack.en_US);

        return result;
    }

    private string GetText(string id, string fallback = "Not Found")
    {
        bool idExist = id2TargetLangDict.ContainsKey(id);
        if (!idExist) return fallback;

        return id2TargetLangDict[id] ?? (id2FallbackLangDict[id] ?? fallback);
    }

    /// <summary>
    /// Get font resource
    /// </summary>
    /// <returns></returns>
    public Font GetFont()
    {
        if (id2FontNameDict == null) return null;
        string fontName = id2FontNameDict["Font_HomeUI"];
        if (!name2FontDict.TryGetValue(fontName, out Font font))
        {
            font = Resources.Load<Font>(System.IO.Path.Combine(fontPath, fontName));
            if (font)
                name2FontDict.Add(fontName, font);
        }
        return font;
    }
}