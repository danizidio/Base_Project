using System.Collections;
using StateMachine;
using UnityEngine;
using Unity.Cinemachine;

[RequireComponent(typeof(CinemachineBrain))]
[RequireComponent(typeof(CinemachineCamera))]
public class CameraBehaviour : MonoBehaviour
{
    public delegate void _onSearchingPlayer();
    public static _onSearchingPlayer OnSearchingPlayer;

    public delegate void _onGetFocus(GameObject item);
    public static _onGetFocus OnGetFocus;

    [SerializeField] float _cameraSizeMinimum;
    [SerializeField] float _cameraSizeMaximum;
    [SerializeField] float _maxTimeOnFocus;

    GameObject _p;

    void FindPlayer()
    {
        StartCoroutine(CorroutineFindPlayer());
    }

    IEnumerator CorroutineFindPlayer()
    {
        _p = GameObject.FindGameObjectWithTag("Player");

        yield return new WaitForSeconds(.02f);

        if (_p != null)
        {
            GetComponent<CinemachineCamera>().Follow = _p.transform;

            GameManager.OnNextGameState?.Invoke(GamePlayStates.START);

            StopCoroutine(CorroutineFindPlayer());
        }
        else
        {
            yield return new WaitForSeconds(.02f);
        }
    }

    void ObjectToFocus(GameObject item)
    {
        if (GetComponent<CinemachineCamera>().Follow != _p)
        {
            StopCoroutine("CorroutineObjectToFocus");

            GetComponent<CinemachineCamera>().Follow = null;

            StartCoroutine(CorroutineObjectToFocus(item));
        }
        else
        {
            StartCoroutine(CorroutineObjectToFocus(item));
        }
    }

    IEnumerator CorroutineObjectToFocus(GameObject item)
    {
        GetComponent<CinemachineCamera>().Follow = item.transform;

        GetComponent<CinemachineCamera>().Lens.OrthographicSize = _cameraSizeMinimum;

        yield return new WaitForSeconds(_maxTimeOnFocus);

        GetComponent<CinemachineCamera>().Follow = _p.transform;

        GetComponent<CinemachineCamera>().Lens.OrthographicSize = _cameraSizeMaximum;
    }
    private void OnEnable()
    {
        OnSearchingPlayer += FindPlayer;
        OnGetFocus = ObjectToFocus;
    }
    private void OnDisable()
    {
        OnSearchingPlayer -= FindPlayer;
        OnGetFocus = null;
    }
}
