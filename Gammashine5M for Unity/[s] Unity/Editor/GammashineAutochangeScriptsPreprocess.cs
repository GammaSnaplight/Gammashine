using System.IO;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;

using UnityEditor;
using UnityEditor.Callbacks;

/// <summary>
/// Спец.класс для создания готового шаблона на основе окончаний названия файла .cs 
/// </summary>
//public class GammashineAutochangeScriptsPreprocess : AssetModificationProcessor
//{
//    private const string _scriptTemplatesFolder = "Assets/Gammashine.3💛 [v61]/[s] Extra/ScriptTemplates/";
//    private const string _iconsFolder = "Assets/Gammashine.3💛 [v61]/[s] Extra/Icons/";

//    private static readonly List<string> suffixes;

//    static GammashineAutochangeScriptsPreprocess()
//    {
//        //---
//        suffixes = Directory.GetFiles(_scriptTemplatesFolder, "*.cs.txt", SearchOption.TopDirectoryOnly)
//            .Select(path => Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(path)))
//            .Distinct()
//            .OrderByDescending(s => s.Length)
//            .ToList();
//    }

//    private static void OnWillCreateAsset(string assetPath)
//    {
//        //---
//        if (!assetPath.EndsWith(".cs")) return;

//        //---
//        string fileName = Path.GetFileNameWithoutExtension(assetPath);

//        //---

//    }

//    private static string FindTemplate(string fileName)
//    {
//        //---
//        foreach (var suffix in suffixes)
//        {
//            if (fileName.EndsWith(suffix, System.StringComparison.InvariantCultureIgnoreCase))
//            {
//                return Path.Combine(_scriptTemplatesFolder, suffix + ".cs.txt");
//            }
//        }

//        //---
//        return null;
//    }

//    private static void FindIcon(string assetPath, string fileName)
//    {

//    }
//}
