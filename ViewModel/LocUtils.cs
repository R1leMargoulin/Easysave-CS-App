using Microsoft.Win32;
using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows;

namespace EasySave
{
    public static class LocUtils
    {
        ///   
        /// Get application name from an element  
        ///
        private static string getAppName(FrameworkElement element)
        {
            var elType = element.GetType().ToString();
            var elNames = elType.Split('.');
            return elNames[0];
        }
        ///  
        /// Generate a name from an element base on its class name  
        ///   
  
        private static string getElementName(FrameworkElement element)
        {
            var elType = element.GetType().ToString();
            var elNames = elType.Split('.');
            var elName = "";
            if (elNames.Length >= 2) elName = elNames[elNames.Length - 1];
            return elName;
        }
        ///   
        /// Get current culture info name base on previously saved setting if any,  
        /// otherwise get from OS language  
 
        public static string GetCurrentCultureName(FrameworkElement element)
        {
            RegistryKey curLocInfo = Registry.CurrentUser.OpenSubKey("GsmLib" + @"\" + getAppName(element), false);

            var cultureName = CultureInfo.CurrentUICulture.Name; //Console.WriteLine(cultureName);
            if (curLocInfo != null)
            {
                cultureName = curLocInfo.GetValue(getElementName(element) + ".localization", "en-US").ToString();
            }

            return cultureName;
        }
        ///  
        /// Set language based on previously save language setting,  
        /// otherwise set to OS lanaguage  
     
        public static void SetDefaultLanguage(FrameworkElement element)
        {
            SetLanguageResourceDictionary(element, GetLocXAMLFilePath(getElementName(element), GetCurrentCultureName(element)));
        }
        ///  
        /// Dynamically load a Localization ResourceDictionary from a file  
        ///  
        public static void SwitchLanguage(FrameworkElement element, string inFiveCharLang)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(inFiveCharLang);
            SetLanguageResourceDictionary(element, GetLocXAMLFilePath(getElementName(element), inFiveCharLang));
            // Save new culture info to registry  
            RegistryKey UserPrefs = Registry.CurrentUser.OpenSubKey("GsmLib" + @"\" + getAppName(element), true);  
                    if (UserPrefs == null)
            {
                // Value does not already exist so create it  
                RegistryKey newKey = Registry.CurrentUser.CreateSubKey("GsmLib");
                UserPrefs = newKey.CreateSubKey(getAppName(element));
            }
            UserPrefs.SetValue(getElementName(element) + ".localization", inFiveCharLang);
        }
        ///  
        /// Returns the path to the ResourceDictionary file based on the language character string.  
        ///  
  
        public static string GetLocXAMLFilePath(string element, string inFiveCharLang)
        {
            string locXamlFile = element + "." + inFiveCharLang + ".xaml";
            string directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            return Path.Combine(directory, "i18N", locXamlFile);
        }

        /// Sets or replaces the ResourceDictionary by dynamically loading  
        /// a Localization ResourceDictionary from the file path passed in.  


        private static void SetLanguageResourceDictionary(FrameworkElement element, String inFile)
        {
            if (File.Exists(inFile))
            {
                // Read in ResourceDictionary File  
                var languageDictionary = new ResourceDictionary();
                languageDictionary.Source = new Uri(inFile);
                // Remove any previous Localization dictionaries loaded  
                int langDictId = -1;
                for (int i = 0; i < element.Resources.MergedDictionaries.Count; i++)
                {
                    var md = element.Resources.MergedDictionaries[i];
                    // Make sure your Localization ResourceDictionarys have the ResourceDictionaryName  
                    // key and that it is set to a value starting with "Loc-".  
                    if (md.Contains("ResourceDictionaryName"))
                    {
                        if (md["ResourceDictionaryName"].ToString().StartsWith("Loc-"))
                        {
                            langDictId = i;
                            break;
                        }
                    }
                }
                if (langDictId == -1)
                {
                    // Add in newly loaded Resource Dictionary  
                    element.Resources.MergedDictionaries.Add(languageDictionary);
                }
                else
                {
                    // Replace the current langage dictionary with the new one  
                    element.Resources.MergedDictionaries[langDictId] = languageDictionary;
                }
            }
            else
            {
                MessageBox.Show("'" + inFile + "' not found.");
            }
        }
    }
}