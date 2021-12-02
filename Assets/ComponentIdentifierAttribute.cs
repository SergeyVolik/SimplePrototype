//using System;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;

//[System.AttributeUsage(System.AttributeTargets.Class, AllowMultiple = false)]
//public class ComponentIdentifierAttribute : Attribute
//{
//    public string Name { get; set; }
//}

//public static class ComponentCache
//{
//    public static Dictionary<string, Type> Registrations = new Dictionary<string, Type>();

//    [UnityEditor.Callbacks.DidReloadScripts]
//    private static void OnScriptsReloaded()
//    {
//        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
//        {
//            foreach (var currentType in assembly.GetTypes().Where(_ => typeof(MonoBehaviour).IsAssignableFrom(_))
//            {
//                var attributes = currentType.GetCustomAttributes(typeof(ComponentIdentifierAttribute), false);
//                if (attributes.Length > 0)
//                {
//                    var targetAttribute = attributes.First() as ComponentIdentifierAttribute;
//                    ComponentCache.Registrations.Add(targetAttribute.Name, currentType);
//                }
//            }
//        }
//    }
//}