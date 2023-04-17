using UnityEngine;
using UnityEngine.InputSystem;
public class InputHandler : MonoBehaviour
{
    [SerializeField] PlayerInput _input;

    [SerializeField] Sprite _keyboardBtn, _controllerBtn;

    void Start()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Player");

        _input = obj.GetComponent<PlayerInput>();

        _input.controlsChangedEvent.AddListener(ChangeControlUI);

        _input.controlsChangedEvent?.Invoke(_input);
    }

    public void ChangeControlUI(PlayerInput _p)
    {
        if (_p.currentControlScheme.Equals("Controller"))
        {


        }
        if (_p.currentControlScheme.Equals("KeyBoard"))
        {

        }

        print(_p.currentControlScheme);
    }
}
