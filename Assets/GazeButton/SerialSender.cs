/*
Unity send + Arduino read example

* 因為Unity send時不能開Arduino serial port monitor檢查，所以這邊會回傳收到的data給unity供檢查

*/
using System.Text;  
using UnityEngine;
using System.Collections;

public class SerialSender : MonoBehaviour {

    private SerialManager serialmanager;
    private string TextToSend = "G456E"; 

    private void Awake () {

        serialmanager = GetComponent<SerialManager>();

    }

    private void Start () {

        StartCoroutine(ExampleCoroutine(TextToSend));

    }

    IEnumerator ExampleCoroutine(string text) {

        while(true) {
        
            serialmanager.serialPort.Write(Encoding.ASCII.GetBytes(text), 0, Encoding.ASCII.GetBytes(text).Length);    

            // yield return null;
            yield return new WaitForSeconds(1);         // delay 1秒

        }
    
    }
 
}
