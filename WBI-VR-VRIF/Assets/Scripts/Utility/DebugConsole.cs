using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugConsole : GenericSingleton<DebugConsole>
{
    [SerializeField]
    private TextMeshProUGUI debugConsole;

    public void WriteToConsole(string log)
    {
        debugConsole.text += log + '\n';
    }
}
