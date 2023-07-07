/*
Unity read + Arduino send example

* DisplayType 可選擇要顯示ASCII碼或翻譯成character

*/
using System;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;
using UnityEngine.UI;


public class SerialReceiver : MonoBehaviour {
  [SpaceAttribute(10)]
  [HeaderAttribute("                       -------   ASCII:0    ------- ")]
  [HeaderAttribute("                       ------- Character:1  ------- ")]
  [SpaceAttribute(10)]
  public int DisplayType; // 以ASCII碼或character展示             

  private SerialManager serialmanager;
  private int data;
  private int prev;
  private bool isOpen;

  public event EventHandler OnButtonClicked;
  public event EventHandler OnButtonHovered;
  public event EventHandler OnButtonUnhovered;

  private void Awake () {
    serialmanager = GetComponent<SerialManager>();

    data = 0;
    prev = 0;
    isOpen = false;
  }

  void Update() {
    serialReceive();
    CheckButtonStatus();
    LogCurrentGazeTarget();
  }

  private void serialReceive() {
    try {
      readOneByteAtATime(); // 一次只讀one byte         
    } 
    catch {}
  }

  private void readOneByteAtATime() { // 一次讀one byte
    prev = data;
    data = serialmanager.serialPort.ReadByte();                       
    // displayStyle(DisplayType, data);
  }

  private void displayStyle(int displayType, int data) {
    if (displayType == 0) { // display ASCII number
      Debug.Log(data);   
    } 
    else if (displayType == 1) { // display character
      char c = Convert.ToChar(data);                        
      Debug.Log(c);    
    }
  }

  private void CheckButtonStatus() {
    if (prev == 2 && data <= 1) {
      CoreServices.InputSystem.GazeProvider.GazeTarget.transform.parent.GetComponent<Button>().onClick.Invoke();
      // if (OnButtonClicked != null) OnButtonClicked(this, EventArgs.Empty);
    }

    if (data >= 1 && !isOpen) {
      if (OnButtonHovered != null) OnButtonHovered(this, EventArgs.Empty);

      isOpen = true;
    } else if (data == 0 && isOpen) {
      if (OnButtonUnhovered != null) OnButtonUnhovered(this, EventArgs.Empty);

      isOpen = false;
    }
  }

  void LogCurrentGazeTarget() {
    if (CoreServices.InputSystem.GazeProvider.GazeTarget)
    {
      // Debug.Log("User gaze is currently over game object: " + CoreServices.InputSystem.GazeProvider.GazeTarget);
      // CoreServices.InputSystem.GazeProvider.GazeTarget.transform.parent.GetComponent<Button>().onClick.Invoke();
    }
  }
}
