
#if UNITY_EDITOR

using System;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.Sprites;
using System.Collections.Generic;

class ReadablePackingPolicy : UnityEditor.Sprites.IPackerPolicy
{
    protected class Entry
    {
        public Sprite            sprite;
        public AtlasSettings     settings;
        public string            atlasName;
        public SpritePackingMode packingMode;
        public int               anisoLevel;
    }

    private const uint kDefaultPaddingPower = 2; // Good for base and two mip levels.

    public virtual int GetVersion() { return 1; }

    protected virtual string TagPrefix { get { return "[TIGHT]"; } }
    protected virtual bool AllowTightWhenTagged { get { return true; } }

    public void OnGroupAtlases(BuildTarget target, PackerJob job, int[] textureImporterInstanceIDs)
    {
        var entries = new List<Entry>();

        foreach (int instanceID in textureImporterInstanceIDs)
        {
            var ti = EditorUtility.InstanceIDToObject(instanceID) as TextureImporter;
            TextureFormat textureFormat;
            ColorSpace colorSpace;
            int compressionQuality;
            if (ti == null)
                continue;
            ti.ReadTextureImportInstructions(target, out textureFormat, out colorSpace, out compressionQuality);

            var tis = new TextureImporterSettings();
            ti.ReadTextureSettings(tis);
            ti.isReadable = true;
            tis.readable = true;

            Sprite[] sprites = AssetDatabase.LoadAllAssetRepresentationsAtPath(ti.assetPath).Select(x => x as Sprite).Where(x => x != null).ToArray();

            foreach (Sprite sprite in sprites)
            {
                var entry = new Entry();
                entry.sprite = sprite;
                entry.settings.format = textureFormat;
                entry.settings.colorSpace = colorSpace;
                entry.settings.compressionQuality = compressionQuality;
                entry.settings.filterMode = Enum.IsDefined(typeof(FilterMode), ti.filterMode) ? ti.filterMode : FilterMode.Bilinear;
                entry.settings.maxWidth = 2048;
                entry.settings.maxHeight = 2048;
                entry.settings.generateMipMaps = ti.mipmapEnabled;
                if (ti.mipmapEnabled)
                    entry.settings.paddingPower = kDefaultPaddingPower;
                entry.atlasName = ParseAtlasName(ti.spritePackingTag);
                entry.packingMode = GetPackingMode(ti.spritePackingTag, tis.spriteMeshType);
                entry.anisoLevel = ti.anisoLevel;

                entries.Add(entry);
            }

            Resources.UnloadAsset(ti);

            foreach (var sprite in sprites)
            {
                var m_tex = sprite.texture;
                AlphaMapCreater.Create(m_tex);
            }
        }

        // First split sprites into groups based on atlas name
        var atlasGroups =
            from e in entries
            group e by e.atlasName;
        foreach (var atlasGroup in atlasGroups)
        {
            int page = 0;
            // Then split those groups into smaller groups based on texture settings
            var settingsGroups =
                from t in atlasGroup
                group t by t.settings;
            foreach (var settingsGroup in settingsGroups)
            {
                string atlasName = atlasGroup.Key;
                if (settingsGroups.Count() > 1)
                    atlasName += string.Format(" (Group {0})", page);

                AtlasSettings settings = settingsGroup.Key;
                settings.anisoLevel = 1;

                // Use the highest aniso level from all entries in this atlas
                if (settings.generateMipMaps)
                    foreach (Entry entry in settingsGroup)
                        if (entry.anisoLevel > settings.anisoLevel)
                            settings.anisoLevel = entry.anisoLevel;

                job.AddAtlas(atlasName, settings);

                foreach (Entry entry in settingsGroup)
                {
                    job.AssignToAtlas(atlasName, entry.sprite, entry.packingMode, SpritePackingRotation.None);
                }

                ++page;
            }

        }
    }

    protected bool IsTagPrefixed(string packingTag)
    {
        packingTag = packingTag.Trim();
        if (packingTag.Length < TagPrefix.Length)
            return false;
        return (packingTag.Substring(0, TagPrefix.Length) == TagPrefix);
    }

    private string ParseAtlasName(string packingTag)
    {
        string name = packingTag.Trim();
        if (IsTagPrefixed(name))
            name = name.Substring(TagPrefix.Length).Trim();
        return (name.Length == 0) ? "(unnamed)" : name;
    }

    private SpritePackingMode GetPackingMode(string packingTag, SpriteMeshType meshType)
    {
        if (meshType == SpriteMeshType.Tight)
            if (IsTagPrefixed(packingTag) == AllowTightWhenTagged)
                return SpritePackingMode.Tight;
        return SpritePackingMode.Rectangle;
    }
}

#endif