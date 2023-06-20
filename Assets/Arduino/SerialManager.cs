/*
Unity and Arduino serial port communication example

* Unity read + Arduino send 只需 activate SerialReceiver script
* Unity send + Arduino read 除了需要 SerialSenders script 也要 SerialReceiver 供Arduino回傳時接收檢查

*/
using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class SerialManager : MonoBehaviour {

    [SpaceAttribute(10)]
    [HeaderAttribute("                       ------- Port Settings ------- ")]
    [SpaceAttribute(10)]
    public string Port;
    public int BaudRate;

    public SerialPort serialPort;

    void Awake() {

        serialInitialize();

    }

    private void serialInitialize(){

        serialPort = new SerialPort(Port, BaudRate);
        serialPort.ReadTimeout = 20;
        serialPort.Open();

    }
}
