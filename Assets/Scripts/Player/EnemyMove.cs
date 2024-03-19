using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
      public GameObject item;
      public float speedX;
      public float speedZ;
      public float enemyUpPos;
      public float enemyForPos = 2.5f;
      public float xSpeedMultiplayer = 3;
      public float zSpeedMultiplayer = 3;

      public void Update()
      {
          float e_x = Mathf.PingPong(Time.time * speedX, 2.5f) * xSpeedMultiplayer + enemyUpPos ;
          float e_z = Mathf.PingPong(Time.time * speedZ, 2.2f) * zSpeedMultiplayer +enemyForPos;
          item.transform.position = new Vector3(e_x, item.transform.position.y, item.transform.position.z);
        //   item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y, e_z);
        //   item.transform.position = new Vector3(e_x, item.transform.position.y, e_z);
          
      }
}
