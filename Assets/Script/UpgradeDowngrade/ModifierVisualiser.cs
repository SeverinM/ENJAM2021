using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModifierVisualiser : MonoBehaviour
{
    [SerializeField]
    TMP_Text title;

    [SerializeField]
    Image icon;

    [SerializeField]
    TMP_Text descr1;

    [SerializeField]
    int id;

    private void OnEnable()
    {
        ModifierManager.Instance.OnUpdateSlot += UpdateDisplay;
    }

    private void OnDisable()
    {
        ModifierManager.Instance.OnUpdateSlot -= UpdateDisplay;
    }

    void UpdateDisplay(int _id, ModifierHolder.Modifier modifier)
    {
        if (_id != id)
            return;

        title.text = modifier.name;
        icon.sprite = modifier.icon;
        descr1.text = modifier.malusName;
    }
}
