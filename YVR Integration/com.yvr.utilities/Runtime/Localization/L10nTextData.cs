using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Deserialized type from localization json data
/// </summary>
[System.Serializable]
public class L10nTextData
{
    /// <summary>
    /// Localization language pack array
    /// </summary>
    public L10nLangPack[] languagePacks;
}

[System.Serializable]
public class L10nLangPack
{
    /// <summary>
    /// Language pack id
    /// </summary>
    public string id;
    /// <summary>
    /// Chinese (Simplified, China) 
    /// </summary>
    public string zh_Hans_CN;
    /// <summary>
    /// English (United States)
    /// </summary>
    public string en_US;
}

/// <summary>
/// Supported Language Type
/// </summary>
public enum L10nLangType
{
    Keep,
    zh_Hans_CN,
    en_US
}