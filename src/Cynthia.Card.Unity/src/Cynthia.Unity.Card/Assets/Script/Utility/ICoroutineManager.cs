using System.Collections;
using UnityEngine;

namespace Assets.Script.Utility
{
    public interface ICoroutineManager
    {
        Coroutine StartCoroutine(IEnumerator routine);
    }

}
