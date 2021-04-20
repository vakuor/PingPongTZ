using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class PercentageUpdator : MonoBehaviour
{
    TextMeshProUGUI textMeshPro;
    private void Start() {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }
    private void Update() {
        textMeshPro.text = GameManager.instance.Loader.Progress.ToString();
    }
}
