using System;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Microsoft.MixedReality.Toolkit.Experimental.UI;

[RequireComponent(typeof(TMP_InputField))]
public class KeyBoardField : MonoBehaviour, IPointerDownHandler {
  /// <summary>
  /// This component links the NonNativeKeyboard to a TMP_InputField
  /// Put it on the TMP_InputField and assign the NonNativeKeyboard.prefab
  /// </summary>
  [SerializeField] private NonNativeKeyboard keyboard = null;
  [SerializeField] private SerialReceiver serialReceiver = null;

  public void OnPointerDown(PointerEventData eventData) {
    keyboard.PresentKeyboard();

    keyboard.OnClosed += DisableKeyboard;
    keyboard.OnTextSubmitted += DisableKeyboard;
    keyboard.OnTextUpdated += UpdateText;
  }

  private void UpdateText(string text) {
    GetComponent<TMP_InputField>().text = text;
  }

  private void DisableKeyboard(object sender, EventArgs e) {
    keyboard.OnTextUpdated -= UpdateText;
    keyboard.OnClosed -= DisableKeyboard;
    keyboard.OnTextSubmitted -= DisableKeyboard;

    keyboard.Close();
  }
}
