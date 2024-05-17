using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Deserialized type from localizationFont json data
/// </summary>
[System.Serializable]
public class L10nFontData
{
    /// <summary>
    /// Localization font pack array
    /// </summary>
    public L10nLangPack[] fontPacks;
}

[System.Serializable]
public class L10nFontPack
{
    /// <summary>
    /// font pack id
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

