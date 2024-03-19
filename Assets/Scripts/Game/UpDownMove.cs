using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownMove : MonoBehaviour
{
      public GameObject item;
      public float speed;
      public float enemyUpPos;
  
      public void Update()
      {
          float y = Mathf.PingPong(Time.time * speed, 1.2f) * 3 +enemyUpPos;
          item.transform.position = new Vector3(item.transform.position.x, y, item.transform.position.z);
      }
}
