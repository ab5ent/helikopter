using UnityEngine;

namespace Common
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField]
        protected bool enableDontDestroyOnLoad;

        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance)
                {
                    return _instance;
                }

                T instanceInScene = FindObjectOfType<T>();

                if (instanceInScene != null)
                {
                    SetInstance(instanceInScene);
                    return _instance;
                }

                T instancePrefab = Resources.Load<T>($"{typeof(T).Name}");

                if (instancePrefab != null)
                {
                    SetInstance(Instantiate(instancePrefab));
                    return _instance;
                }

                GameObject newInstance = new GameObject();
                SetInstance(newInstance.AddComponent<T>());
                return _instance;
            }
        }

        private static void SetInstance(T instance)
        {
            _instance = instance;
            _instance.name = $"<Singleton>{typeof(T).Name}";
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                SetInstance(this as T);
            }
            else if (_instance != this)
            {
                Destroy(this);
                return;
            }

            if (enableDontDestroyOnLoad)
            {
                DontDestroyOnLoad(this);
            }
        }
    }
}