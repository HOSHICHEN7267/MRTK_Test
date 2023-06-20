using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCube : MonoBehaviour {
  public SerialReceiver serialReceiver;
  public float speed = 0.5f;

  // Start is called before the first frame update
  void Start() {
  }

  // Update is called once per frame
  void Update() {
    if (serialReceiver.data == 1) {
      transform.position = new Vector3(
        transform.position.x,
        transform.position.y,
        transform.position.z + speed
      );
    }
    else if (serialReceiver.data == 2) {
        transform.position = new Vector3(
        transform.position.x,
        transform.position.y,
        transform.position.z - speed
      );
    }
  }
}
