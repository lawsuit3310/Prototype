using Mono.Cecil;
using System;
using System.Collections.Generic;
using UnityEngine;
using VinChaud;


public class ConverseManager : IDisposable
{
    private bool disposed = false;
    private List<Dictionary<string, object>> _csvObject;

    public ConverseManager(string path)
    {
        //key - head
        //value - 실제 텍스트
        _csvObject = CSVReader.Read("csv/Converse");
    }

    public void Converse()
    {
        foreach (var VARIABLE in _csvObject)
        {
            foreach (var pair in VARIABLE)
            {
                Debug.Log(pair.Key + " " + pair.Value);
            }
        }
        Dispose();
    }

    public void Converse(int startIndex, int range)
    {
        var _converse = GameObject.FindWithTag("CONVERSEMANAGER").GetComponent<Converse>();
        for (int i = startIndex; i < startIndex + range; i++)
        {
            var line = _csvObject[i];
            _converse.showTextPanel(
                new AWord(
                    portrait: Resources.Load(line["PortraitPath"] + "", typeof(Sprite)) as Sprite,
                    speaker: line["Speaker"] + "",
                    talk: line["Talk"] + ""
                    )
                );
        }
        Dispose();
    }

    public void Dispose()
    {
        if (disposed) return;
        disposed = true;
        _csvObject = null;
        GC.SuppressFinalize(this);
    }
}
