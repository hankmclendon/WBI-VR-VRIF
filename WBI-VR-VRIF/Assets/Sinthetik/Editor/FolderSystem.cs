using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

public class FolderSystem : Editor
{
    [MenuItem("Sinthetik/Folders/Standard Folders", false, 1)]
    static void StandardFolderSystem()
    {
        if (!CheckForAsset("Assets/Materials")) { AssetDatabase.CreateFolder("Assets", "Materials"); }
        if (!CheckForAsset("Assets/Scripts")) { AssetDatabase.CreateFolder("Assets", "Scripts"); }
        if (!CheckForAsset("Assets/Shaders")) { AssetDatabase.CreateFolder("Assets", "Shaders"); }
        if (!CheckForAsset("Assets/Sprites")) { AssetDatabase.CreateFolder("Assets", "Sprites"); }
        if (!CheckForAsset("Assets/Textures")) { AssetDatabase.CreateFolder("Assets", "Textures"); }
        if (!CheckForAsset("Assets/Scenes")) { AssetDatabase.CreateFolder("Assets", "Scenes"); }
        if (!CheckForAsset("Assets/Animations")) { AssetDatabase.CreateFolder("Assets", "Animations"); }

        bool CheckForAsset(string asset)
        {
            bool exists = false;
            var results = AssetDatabase.FindAssets("t:folder", new[] { "Assets" });
            foreach (string guid in results)
            {
                if (AssetDatabase.GUIDToAssetPath(guid) == asset)
                {
                    exists = true;
                }
            }
            return exists;
        }
    }
}
