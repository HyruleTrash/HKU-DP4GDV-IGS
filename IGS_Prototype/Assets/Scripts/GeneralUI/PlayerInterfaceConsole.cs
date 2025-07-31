
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// A class for handling the in game console
/// </summary>
[CreateAssetMenu(fileName = "PlayerInterfaceConsole", menuName = "Console/PlayerInterfaceConsole")]
public class PlayerInterfaceConsole : ScriptableObjectSingleton<PlayerInterfaceConsole>
{
    [SerializeField] private TextPrefab textHolderPrefab;
    [SerializeField] private int consoleSize;
    private List<string> consoleHistory;

    public void AddToConsole(string text)
    {
        consoleHistory.Add(text);
        textHolderPrefab.instanceTextComponent.text = GetConsoleHistory();
        CheckHistorySize();
    }

    private void CheckTextHolder()
    {
        if (textHolderPrefab.instance)
            return;
        textHolderPrefab.Instantiate(Game.instance.gameInterface.transform);
    }

    private void CheckHistorySize()
    {
        while (consoleHistory.Count > consoleSize)
        {
            consoleHistory.RemoveAt(0);
            if (consoleHistory.Count <= consoleSize)
                return;
        }
    }

    private string GetConsoleHistory()
    {
        StringBuilder result = new();
        foreach (var text in consoleHistory)
            result.AppendLine(text);
        return result.ToString();
    }

    public void Setup()
    {
        CheckTextHolder();
        consoleHistory = new ();
        textHolderPrefab.instanceTextComponent.text = "";
    }
}