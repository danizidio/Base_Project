using UnityEngine;
using TMPro;
using StateMachine;

public class ShowInfo : MonoBehaviour
{
    public delegate void _onShowUI(string s, GameObject obj);
    public static _onShowUI OnShowUI;

    [SerializeField] GameObject _uiToInstantiate;

    public void ShowUI(string s, GameObject obj)
    {
        if(obj == null)
        {
            obj = GameObject.FindGameObjectWithTag("MainCamera");
        }

        GameObject temp = Instantiate(_uiToInstantiate, new Vector3(obj.transform.position.x,
                                                                    obj.transform.position.y,
                                                                    obj.transform.position.z + 3),
                                                                    Quaternion.identity,
                                                                    obj.transform);

        temp.GetComponentInChildren<TMP_Text>().text = s;

        GamePlayBehaviour.OnNextGameState?.Invoke(GamePlayStates.SHOWINFO);
    }

    private void OnEnable()
    {
        OnShowUI = ShowUI;
    }

    private void OnDisable()
    {
        OnShowUI -= ShowUI;
    }
}
