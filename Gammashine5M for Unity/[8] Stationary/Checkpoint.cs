using System;
using System.IO;

using UnityEngine;

namespace Snaplight.Stationary
{
    public static class Checkpoint
    {
        public static void Collection<T>(T json, string path)
        {
            string data = JsonUtility.ToJson(json);
            File.WriteAllText(path, data);
        }

        public static T Gathering<T>(string path)
        {
            if (!Exists(path))
                throw new NullReferenceException($"File not found - {path}");

            string data = File.ReadAllText(path);
            return JsonUtility.FromJson<T>(data);
        }

        public static void Elimination(string path)
        {
            if (!Exists(path))
                throw new NullReferenceException($"File not found - {path}");

            File.Delete(path);
        }

        public static void Rename(string oldPath, string newPath)
        {
            if (!Exists(oldPath))
                throw new NullReferenceException($"File not found - {oldPath}");

            if (Exists(newPath))
                throw new IOException($"File already exists at the new path - {newPath}");

            File.Move(oldPath, newPath);
        }

        public static bool Exists(string path) 
            => File.Exists(path);
    }
}
