using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ShowText.TextManager.OnCallText?.Invoke();
        }
    }
}
