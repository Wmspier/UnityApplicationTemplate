using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameobjectExtensions
{

    public static T GetOrAddComponent<T>(this GameObject gameobject) where T : MonoBehaviour
    {
        if (gameobject.GetComponent<T>() == null)
        {
            gameobject.AddComponent<T>();
        }
        return gameobject.GetComponent<T>();
    }

}
