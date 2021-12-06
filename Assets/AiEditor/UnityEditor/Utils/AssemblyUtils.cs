using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace SerV112.UtilityAIEditor
{

    public static class AssemblyUtils
    {

        public static bool CheckIfTypeNameExisted(string typeName, string @namespace = null)
        {
            bool result = false;
            string typeNameFull;

            if (!string.IsNullOrEmpty(@namespace))           
                typeNameFull = @namespace + "." + typeName;          
            else typeNameFull = typeName;
           
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var type = assembly.GetType(typeNameFull);

                if (type != null)
                    return true;

            }

            return result;
        }
    }
}