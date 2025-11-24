using Localization;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{

    private void Awake()
    {
        Time.timeScale = 1;

#if UNITY_EDITOR
        {
            NavigationData n = NavigationData.Instance;
            LocalizationManager l = LocalizationManager.Instance;

            if (n == null)
            {
                n = Instantiate(Resources.Load<NavigationData>("NavigationData"));
            }
            if (l == null)
            {
                l = Instantiate(Resources.Load<LocalizationManager>("LocalizationManager"));
            }
        }
#endif
    }

    void Start()
    {
        NavigationData.OnLoading?.Invoke();
    }
}
