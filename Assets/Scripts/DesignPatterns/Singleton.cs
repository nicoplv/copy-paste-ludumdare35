using UnityEngine;
using System.Reflection;

namespace DesignPatterns
{
	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		#region Variables

		protected static T instance;
		protected static GameObject singletonGameObject;
		protected static object locked = new object();
		protected static float instancedAt = -1f;
		protected bool dontDestroyOnLoad = true;

		#endregion

		#region Unity Methods

		public void Awake()
		{
			if (dontDestroyOnLoad)
				DontDestroyOnLoad(Instance.gameObject);
			else
			{
				if (instance == null)
					instance = Instance;
			}
		}

		#endregion

		#region Singleton Methods

		public static void CreateInstance()
		{
			MethodInfo methodInfo = typeof(T).GetMethod("CreateInstance");
			if (methodInfo == null)
				instance = singletonGameObject.AddComponent<T>();
			else
				methodInfo.Invoke(null, null);
		}

		public static T Instance
		{
			get
			{
				if (instance == null)
				{
					lock (locked)
					{
						if (instance == null)
						{
							T[] instances = (T[])FindObjectsOfType(typeof(T));
							if (instances.Length >= 1)
							{
								instance = instances[0];
								for (int x = 1; x < instances.Length; x++)
									Destroy(instances[x].gameObject);
							}
							else
							{
								singletonGameObject = new GameObject();
								singletonGameObject.name = typeof(T).ToString();
								instancedAt = Time.time;
								CreateInstance();
							}
						}
						return instance;
					}
				}
				return instance;
			}
		}

		public static bool Exist()
		{
			if (instance == null)
				return false;
			return instancedAt != Time.time;
		}

		#endregion
	}
}