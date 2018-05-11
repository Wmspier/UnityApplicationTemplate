using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetDatabase : MonoBehaviour
{

    public Dictionary<Type, Dictionary<string, ScriptableObject>> _assets = new Dictionary<Type, Dictionary<string, ScriptableObject>>();

    public static AssetDatabase instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        _assets[typeof(PopupAsset)] = new Dictionary<string, ScriptableObject>(); 
        foreach (PopupAsset popup in Resources.LoadAll("", typeof(PopupAsset)))
        {
            if (!_assets[typeof(PopupAsset)].ContainsKey(popup.Id))
                _assets[typeof(PopupAsset)].Add(popup.Id, popup);
        }
    }

    public T GetAsset<T>(string id) where T : ScriptableObject, new()
    {
        var dict = new Dictionary<string, ScriptableObject>();
        if (_assets.TryGetValue(typeof(T), out dict))
        {
            ScriptableObject asset;
            if (dict.TryGetValue(id, out asset))
                return asset as T;
        }
        return new T();
    }
}
