using Assets.Script.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;
namespace Assets.Script.ResourceManagement
{
    public class AssetBundleManager : IAssetBundleManager, ICoroutineManager
    {
        private static AssetBundleManager _instance;
        public static AssetBundleManager Instance
        {
            set
            {
                if(_instance != null) { return; }
                _instance = value;
            }
            get { return _instance; }
        }
        private MonoBehaviour _monoBehaviour;
        public AssetBundleManager(MonoBehaviour monoBehaviour)
        {
            _monoBehaviour = monoBehaviour;
        }
        private Dictionary<string, AssetBundle> _loadedBundlePool = new Dictionary<string, AssetBundle>();
        private Dictionary<AssetInfo, Object> _loadedPool = new Dictionary<AssetInfo, Object>(AssetInfo.AssetInfoComparerInstance);
        private HashSet<string> _waitLoadBundleList = new HashSet<string>();


        public void LoadResource<T>(string asset, string bundle, T source, Action<Object, T> callback,Type type )
        {
            asset = asset.ToLower();
            bundle = bundle.ToLower();
#if UNITY_EDITOR
            Object objRes = LoadFromLocal(asset, bundle, type);
#else 
            Object objRes = LoadFromAssetBundle(asset, bundle, type);
#endif
            if (objRes is GameObject)
            {
                objRes = GameObject.Instantiate(objRes);
            }
            if (callback != null)
            {
                callback(objRes, source);
            }
        }

#if UNITY_EDITOR
        private Object LoadFromLocal(string asset, string bundle, Type type)
        {
            var pathArray = AssetDatabase.GetAssetPathsFromAssetBundleAndAssetName(bundle, asset);
            if(pathArray.Length > 0)
            {
                return AssetDatabase.LoadAssetAtPath(pathArray[0], type);
            }
            return null;
        }
#endif
        private Object LoadFromAssetBundle(string asset, string bundle, Type type)
        {
            var assetInfo = new AssetInfo { Asset = asset, Bundle = bundle };

            AssetBundle abRes;
            if (!_loadedBundlePool.TryGetValue(bundle, out abRes))
            {
                var path = GetPath(bundle);
                var ab = AssetBundle.LoadFromFile(GetPath(bundle));
                _loadedBundlePool[bundle] = ab;
                abRes = ab;
            }
            Object objRes;
            if (!_loadedPool.TryGetValue(assetInfo, out objRes))
            {
                var obj = abRes.LoadAsset(asset, type);
                objRes = obj;
                _loadedPool[assetInfo] = obj;
            }

            return objRes;
        }

        private string GetPath(string bundle)
        {
            var basePath = Application.streamingAssetsPath;
            string subPath;
#if UNITY_STANDALONE_WIN
            subPath = "/StandaloneWindows/";
#elif UNITY_ANDROID
            subPath = "/Android/";
            //todo
#elif UNITY_STANDALONE_OSX
            
#endif
            return basePath + subPath + bundle;
        }

        public Coroutine StartCoroutine(IEnumerator routine)
        {
            return _monoBehaviour.StartCoroutine(routine);
        }

        public void UnLoadResource(string asset, string bundle)
        {
        }

    }

}
