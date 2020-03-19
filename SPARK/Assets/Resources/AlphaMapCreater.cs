#if UNITY_EDITOR

using System.IO;
using UnityEngine;
using UnityEditor;

public static class AlphaMapCreater
{
    public static void Create(Texture2D texture)
    {
        var map = texture.GetRawTextureData();
        using (Stream stream = File.OpenWrite("Assets/Resources/AlphaMap/" + texture.name + ".bytes")) {
            using (var writer = new BinaryWriter(stream)) {
                writer.Write((uint)texture.width);
                writer.Write((uint)texture.height);
                for (var i = 0; i < map.Length; i += 4 * 8)
                {
                    int j = i + 4, k = i + 8, l = i + 12, m = i + 16, n = i + 20, o = i + 24, p = i + 28;
                    byte b = (byte)(
                        128 * Bit(map, p) +
                        64  * Bit(map, o) +
                        32  * Bit(map, n) +
                        16  * Bit(map, m) +
                        8   * Bit(map, l) +
                        4   * Bit(map, k) +
                        2   * Bit(map, j) +
                        1   * Bit(map, i));
                    writer.Write(b);
                }
            }
        }
    }

    private static int Bit(byte[] map, int x)
    {
        if (map.Length <= x)
            return 0;
        return map[x] > 1? 1 : 0;
    }

}

#endif
