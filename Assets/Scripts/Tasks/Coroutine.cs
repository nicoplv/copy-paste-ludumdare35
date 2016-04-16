using System.Collections;

namespace Tasks
{
    public class Coroutine : DesignPatterns.Singleton<Coroutine>
    {
        #region Singleton

        protected Coroutine() { dontDestroyOnLoad = true; }

        #endregion

        #region Class Methods

        public static void Start(IEnumerator _iEnumerator)
        {
            Instance.StartCoroutine(_iEnumerator);
        }

        public static void Stop(IEnumerator _iEnumerator)
        {
            Instance.StopCoroutine(_iEnumerator);
        }

        public static void StopAll()
        {
            Instance.StopAllCoroutines();
        }

        #endregion
    }
}