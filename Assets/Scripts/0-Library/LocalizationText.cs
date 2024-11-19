using UnityEngine;
using TMPro;
using Localization;
using System;
public class LocalizationText : MonoBehaviour
{
    public static Action OnUpdateLocalization;
    [SerializeField] string key; // A chave para o texto traduzido

   protected virtual void UpdateText()
    {
        TMP_Text textComponent = GetComponent<TMP_Text>();
        if (textComponent != null)
        {
           textComponent.text = LocalizationManager.Instance.GetLocalizedValue(key);
        }
    }

    private void OnEnable()
    {
        OnUpdateLocalization += UpdateText;
    }
    private void OnDisable()
    {
        OnUpdateLocalization -= UpdateText;
    }
}

