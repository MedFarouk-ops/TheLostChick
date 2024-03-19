using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float enemySpeed = 20f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * enemySpeed);
        }
    }
}
