using UnityEngine;

namespace Src.Utility {
    public static class ComponentsUtility {

        public static T GetComponentsFromWhole<T>(GameObject obj) {
            var root = obj.transform.root;
            return root.GetComponentInChildren<T>();
        }
        
    }
}