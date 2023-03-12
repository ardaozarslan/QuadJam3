using UnityEngine;

public class Instanceton<T> : MonoBehaviour where T : Component
{
	private static T _instance;
	public static T Instance
	{
		get
		{
			if (_instance == null)
			{
				T[] objs = FindObjectsOfType<T>(true);
				if (objs.Length > 0)
				{
					// Debug.Log("found at least one instance of " + typeof(T).Name + " in the scene. Using the first one found.");
					T instance = objs[0];
					_instance = instance;
				}
				else
				{
					GameObject go = new GameObject();
					go.name = typeof(T).Name;
					_instance = go.AddComponent<T>();
				}
			}
			return _instance;
		}
	}

	public static bool IsInitialized
	{
		get
		{
			return _instance != null;
		}
	}
}
