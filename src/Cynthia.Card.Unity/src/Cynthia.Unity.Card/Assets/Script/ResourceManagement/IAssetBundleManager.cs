using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Script.ResourceManagement
{
    interface IAssetBundleManager
    {
        void LoadResource<T>(string asset, string bundle, T source,Action<Object, T> callback, Type type);
        void UnLoadResource(string asset, string bundle);
    }
}
