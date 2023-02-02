using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialoguePopupScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;

    public void SetText(string text)
    {
        label.SetText(text);
    }
}
