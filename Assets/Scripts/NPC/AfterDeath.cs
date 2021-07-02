using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterDeath : MonoBehaviour
{
    public GameObject[] items;
    public float radius;

    void OnDestroy() {
        Health health = GetComponent<Health>();
        if(health == null) return;
        if(health.current > 0) return;
        if(items == null) return;
        foreach(GameObject item in items)
        {
            if (item == null) continue;
            float x = Random.Range(-radius, radius);
            float maxY = Mathf.Sqrt(radius * radius - x * x);
            float y = Random.Range(-maxY, maxY);

            GameObject newItem = Instantiate(item, transform.position + new Vector3(x, y, 0), Quaternion.Euler(0, 0, Random.Range(-180, 180)));
        }
    }


}