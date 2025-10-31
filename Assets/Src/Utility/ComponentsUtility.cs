using System;
using UnityEngine;
using VContainer.Unity;
using VContainer;

namespace Src.Utility {
    public static class ComponentsUtility {

        /// <summary>
        /// 指定したオブジェクトの親子全体から指定したコンポーネントを口寄せするメソッド
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetComponentFromWhole<T>(this GameObject obj) {
            var root = obj.transform.root;
            return root.GetComponentInChildren<T>();
        }

        /// <summary>
        /// 指定したオブジェクトの親子全体から指定したコンポーネント全てを口寄せするメソッド
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] GetComponentsFromWhole<T>(this GameObject obj) {
            
            var root = obj.transform.root;
            
            return root.GetComponentsInChildren<T>();
            
        }

        /// <summary>
        /// 指定したオブジェクトからコンテナを取得して、そこから指定したクラスを口寄せするメソッド
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetComponentFromContainer<T>(this GameObject obj) {

            var container = obj.GetComponentFromWhole<LifetimeScope>() ?? throw new ArgumentNullException();

            return container.Container.Resolve<T>() ?? throw new NullReferenceException();
            
        }
    }
}