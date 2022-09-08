using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
	protected static T _instance;
	protected static bool _isApplicationQuitting;

	public static T Instance
	{
		get
		{
			if (_instance == null)
			{
				if (_isApplicationQuitting) return null;

				_instance = FindObjectOfType<T>();
				if (_instance == null)
				{
					GameObject obj = new GameObject();
					_instance = obj.AddComponent<T>();
				}
			}
			return _instance;
		}
	}

	protected virtual void Awake()
	{
		_instance = this as T;

		_isApplicationQuitting = false;
	}

	/// <summary>
	/// Avoid issues with delegates
	/// </summary>
	protected virtual void OnDestroy()
	{
		_isApplicationQuitting = true;
	}

}
