using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterDeath : MonoBehaviour
{
    public GameObject[] items;
    public float radius;
    public GameObject miniCanvas;

    void OnDestroy() {
        Health health = GetComponent<Health>();
        if(health == null) return;
        if(health.current > 0) return;
        foreach(GameObject item in items)
        {
            float x = Random.Range(-radius, radius);
            float maxY = Mathf.Sqrt(radius * radius - x * x);
            float y = Random.Range(-maxY, maxY);

            GameObject newItem = Instantiate(item, transform.position + new Vector3(x, y, 0), Quaternion.Euler(0, 0, Random.Range(-180, 180)));
            LyingSpell lyingSpell = newItem.GetComponent<LyingSpell>();
            if (lyingSpell != null) lyingSpell.miniCanvas = miniCanvas;
            else
            {
                LyingWeapon lyingWeapon = newItem.GetComponent<LyingWeapon>();
                if(lyingWeapon != null) lyingWeapon.miniCanvas = miniCanvas;
            }
        }
    }


}