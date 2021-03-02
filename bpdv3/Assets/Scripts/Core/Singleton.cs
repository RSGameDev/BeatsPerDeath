namespace Assets.Scripts.Core 
{
    using UnityEngine;

    public abstract class Singleton<T> : MonoBehaviour where T : Component
    {
        #region Private & Const variables

        private static T _instance;

        #endregion

        #region Public & Protected variables

        public static T Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    var gameObject = new GameObject();
                    gameObject.name = typeof(T).Name;
                    _instance = gameObject.AddComponent<T>();
                }

                return _instance;
            }
        }

        #endregion

        #region Constructors

        #endregion

        #region Private methods

        #endregion

        #region Public & Protected methods      

        protected void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion
    }

}

