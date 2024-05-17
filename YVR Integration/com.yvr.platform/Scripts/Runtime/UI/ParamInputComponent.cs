using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParamInputComponent : MonoBehaviour
{
    public InputField input;

    public string GetParamData()
    {
        if (input == null)
            return string.Empty;

        return input.text;
    }
}
