using System.IO;
using UnityEngine;
using System;
using System.Collections.Generic;

public class AlphaMap : MonoBehaviour
{
    private readonly byte[] bytes;
    private readonly Sprite sprite;
    private readonly uint width;
    private readonly uint height;
    private static Dictionary<string, AlphaMap> cache;
    private AlphaMap(Sprite sprite)
    {
        this.sprite = sprite;
        var alphaBytes = Resources.Load<TextAsset>("AlphaMap/" + sprite.name).bytes;
        width = BitConverter.ToUInt32(alphaBytes, 0);
        height = BitConverter.ToUInt32(alphaBytes, 4);
        bytes = new byte[alphaBytes.Length - 8];
        Array.Copy(alphaBytes, 8, bytes, 0, bytes.Length);
    }

    static public AlphaMap Load(Sprite sprite)
    {
        cache = cache ?? new Dictionary<string, AlphaMap>();

        if (cache.ContainsKey(sprite.name))
            return cache[sprite.name];

        var map = new AlphaMap(sprite);
        cache.Add(sprite.name, map);
        return map;
    }

    public bool IsFlag(int x, int y)
    {
        var index = (int)(x + y * width) / 8;
        var flag = 1 << (int)((x + y * width) % 8);
        try
        {
            return bytes[index] > (bytes[index] ^ flag);
        }
        catch
        {
            return false;
        }
    }
}

