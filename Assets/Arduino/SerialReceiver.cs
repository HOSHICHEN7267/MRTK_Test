/*
Unity read + Arduino send example

* DisplayType 可選擇要顯示ASCII碼或翻譯成character

*/
using System;
using UnityEngine;

public class SerialReceiver : MonoBehaviour {
  [SpaceAttribute(10)]
  [HeaderAttribute("                       -------   ASCII:0    ------- ")]
  [HeaderAttribute("                       ------- Character:1  ------- ")]
  [SpaceAttribute(10)]
  public int DisplayType; // 以ASCII碼或character展示             

  private SerialManager serialmanager;
  public int data;

  private void Awake () {
    serialmanager = GetComponent<SerialManager>();
  }

  void Update() {
    serialReceive();
  }

  private void serialReceive() {
    try {
      readOneByteAtATime(); // 一次只讀one byte         
    } 
    catch {
    }
  }

  private void readOneByteAtATime() { // 一次讀one byte      
    data = serialmanager.serialPort.ReadByte();                             
    displayStyle(DisplayType, data);
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
}
