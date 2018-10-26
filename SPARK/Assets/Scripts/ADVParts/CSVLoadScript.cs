using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

/*
 *  CSVを読み込み、次のスクリプトに渡す役割のスクリプト
 */

public class CSVLoadScript : MonoBehaviour
{
    //CSV等のリソース
    public TextAsset source;
    //読み込んだ文章のリスト
    private List<string[]> textSources = new List<string[]>();
    //

    //GetComponent出来ているかのテスト用
    public void Get_Test()
    {
        Debug.Log("test");
    }

    //textSourcesをreturnする関数
    public List<string[]> GetSources()
    {
        return textSources;
    }

    //textSourcesを作る関数
    public void Make_textSources()
    {
        string[] lines = source.text.Replace("\r\n", "\n").Split("\n"[0]);
        foreach (var line in lines)
        {
            if (line == "")
            {
                continue;
            }
            textSources.Add(line.Split(','));
        }
        textSources.RemoveAt(0);

        for(int y = 0; y < textSources.Count; y++)
        {
            string result = null;
            for(int x = 0; x < textSources[y].Length; x++)
            {
                if(x != 0)
                {
                    result += ":";
                }
                result += textSources[y][x];
            }
            Debug.Log(result);
        }

        Debug.Log("<color=red>-----END CSVLoad-----</color>");
    }
}
