using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 字典扩展类,对Dictionary的扩展
/// </summary>
public static class DictionaryExtension
{

    /// <summary>
    /// 尝试根据key得到value，得到了直接返回value，没有得到直接返回null
    /// dict表示我们要操作的字典对象
    /// </summary>
    public static Tvalue TryGet<Tkey, Tvalue>(this Dictionary<Tkey, Tvalue> dict, Tkey key)
    {
        Tvalue value;
        dict.TryGetValue(key, out value);
        return value;
    }
}