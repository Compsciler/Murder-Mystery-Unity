using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public float despawnTime;
    float despawnTimer;
    public float maxScale;
    public float increaseRate;

    public bool isProjectile;
    public float projectileSpeed;

    // Start is called before the first frame update
    void Start()
    {
        despawnTimer = despawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (gameObject.transform.localScale.x < maxScale)
        {
            gameObject.transform.localScale += new Vector3(increaseRate, increaseRate, 0) * Time.deltaTime;
        }
        */

        if (isProjectile)
        {
            transform.position += (transform.up * projectileSpeed * Time.deltaTime);  // Never sure when to use this, Translate, MoveTowards, or Invoke
        }

        despawnTimer -= Time.deltaTime;
        if (despawnTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
