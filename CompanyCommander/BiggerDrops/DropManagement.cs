﻿using Harmony;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace DropManagement
{
    public class DropManagement
    {
        internal static string ModDirectory;
        public static Settings settings;
        public static int baysAlreadyAdded = 0;
        public static void Init(string directory, string settingsJSON)
        {
            DropManagement.ModDirectory = directory;
            Logger.BaseDirectory = directory;
            try
            {
                settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Path.Combine(directory, "settings.json")));
                Logger.InitLog();
                Logger.M.TWL(0, "BiggerDrop log inited...", true);
            }
            catch (Exception e)
            {
                settings = new Settings();
                Logger.InitLog();
                Logger.M.TWL(0, string.Format("BiggerDrop log init exception. Looking for: {0}{1}{2}" ,directory,Environment.NewLine, e.ToString()), true);
            }
            try
            {
                var harmony = HarmonyInstance.Create("en.CompanyCommander.DropManagement");
                harmony.PatchAll(Assembly.GetExecutingAssembly());
                ModDirectory = directory;
            }
            catch (Exception e)
            {
                Logger.M.TWL(0, string.Format("Error patching Assembly: {0}{1}",Environment.NewLine,e.ToString()));
            }
        }
    }
}
