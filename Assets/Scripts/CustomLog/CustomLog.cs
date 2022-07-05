using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;
using System.Diagnostics;

namespace xPoke.CustomLog
{
    public static class CustomLog
    {
        public enum CustomLogType
        {
            GENERAL,
            SYSTEM,
            PLAYER,
            WEAPON,
            ANIMATION,
            ASSET,
            GAMEPLAY,
            SERIALISATION,
            VALIDATION,
            UI,
            SEQUENCING,
            EDITOR,
            TOOL,
            MENUS,
            AI,
            INPUT,
            ///etc, add to colour hash table if needed
        }

        //    --------------------     AVOID RED AND YELLOW!!!! (Error and Warning already exist!);
        private static Hashtable ColourValues = new Hashtable
        {
            { CustomLogType.GENERAL,           Color.white },
            { CustomLogType.SYSTEM,            Color.grey},            //grey
            { CustomLogType.PLAYER,            new Color( 0 , 254 , 111, 1 ) },          //green
            { CustomLogType.WEAPON,            new Color( 0 , 122 , 254, 1 ) },          //blue
            { CustomLogType.ANIMATION,         new Color( 0 , 201 , 254, 1 ) },          //aqua
            { CustomLogType.ASSET,             new Color( 60 , 0 , 254, 1 ) },           //navy
            { CustomLogType.GAMEPLAY,          new Color( 143 , 0 , 254, 1 ) },          //purple
            { CustomLogType.SERIALISATION,     new Color( 232 , 0 , 254, 1 ) },          //pink
            { CustomLogType.VALIDATION,        Color.grey },
            { CustomLogType.UI,                new Color( 100, 100, 100, 1) },           //dull grey
            { CustomLogType.SEQUENCING,        new Color( 250, 161, 100, 1 ) },          //orange
            { CustomLogType.EDITOR,            new Color( 254 , 224 , 0, 1 ) },          //yellow (avoid yellow grr)
            { CustomLogType.TOOL,              new Color( 166 , 254 , 0, 1 ) },          //lime
            { CustomLogType.MENUS,             new Color( 20, 12, 94, 1) },              // Lovecraftian?  some sort of purple green ?  apparently it comes out white... ?
            { CustomLogType.AI,                Color.cyan },
            { CustomLogType.INPUT,             Color.green },
        };

        public static void Log(CustomLogType _Type, string _message, GameObject obj = null)
        {
            Debug.Log(LogString(_Type, _message), obj);
        }

        public static void Log(string _message, GameObject obj = null)
        {
            CustomLog.Log(CustomLogType.GENERAL, _message, obj);
        }

        public static void LogError(CustomLogType _Type, string _message, GameObject obj = null)
        {
            Debug.LogError(LogString(_Type, _message), obj);
        }

        public static void LogWarning(CustomLogType _Type, string _message, GameObject obj = null)
        {
            Debug.LogWarning(LogString(_Type, _message), obj);
        }

        static string LogString(CustomLogType _Type, string _message)
        {
            string calledBy = GetCaller(new StackTrace().FrameCount);
            return "<color=#" + ColorUtility.ToHtmlStringRGB((Color)ColourValues[_Type]) + "> [" + _Type.ToString() + "] </color>- " +
                "<color=#" + ColorUtility.ToHtmlStringRGB((Color)ColourValues[_Type]) + ">" + calledBy + "</color> " + _message;
        }

        // Default level of two, will be 2 levels up from the GetCaller function
        private static string GetCaller(int level = 2)
        {
            var m = new StackTrace().GetFrame(level).GetMethod();

            // Get the class name
            var className = m.DeclaringType.FullName;

            // Get the method name
            var methodName = m.Name;

            // Returns a composite of class name and method name
            return "[" + className + " " + methodName + "]";
        }

    }
}
