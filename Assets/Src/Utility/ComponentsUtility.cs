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

            if (obj == null) return default;

            // まず親方向に LifetimeScope が無いか探す（GetComponentInParent は一般的で安全）
            LifetimeScope scope = null;
            
            try {
                scope = obj.GetComponentInParent<LifetimeScope>();
            }
            catch (Exception) {
                scope = null;
            }

            // 親方向に見つからなければルートの子供から探す（従来の挙動）
            if (scope == null) {
                try {
                    var root = obj.transform.root;
                    scope = root != null ? root.GetComponentInChildren<LifetimeScope>() : null;
                } catch (Exception) { scope = null; }
            }

            if (scope == null) {
                Debug.LogWarning($"GetComponentFromContainer: LifetimeScope が見つかりませんでした ({obj.name})");
                return default;
            }

            try {
                var instance = scope.Container.Resolve<T>();
                if (instance == null) {
                    Debug.LogWarning($"GetComponentFromContainer: Resolve は null を返しました 型={typeof(T)} 対象={obj.name}");
                }
                return instance;
            } catch (Exception ex) {
                Debug.LogWarning($"GetComponentFromContainer: Resolve に失敗しました 型={typeof(T)} 対象={obj.name} エラー={ex.Message}");
                return default;
            }

        }
    }
}