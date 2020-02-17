using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MouseControl : MonoBehaviour
{
    public float moveSpeed;

    public GameObject bullet;
    public float reloadTime;
    private float reloadTimeTimer;

    private AmmoCollection ammo_collection;
    private HealthDisplay health_display;
    public Text ammoDisplay;

    private void Start()
    {
        ammo_collection = GetComponent<AmmoCollection>();
    }
    void Update()
    {
        var mouse = Input.mousePosition;
        var screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
        var offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
        var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 180);

        //Converting mouse position into Vector3
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Fixing z-axis
        target.z = transform.position.z;

        //Moving player toward mouse
        float distance = Vector3.Distance(transform.position, target);
        if (distance > 0.5)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * (distance * 2) * Time.deltaTime);
        }
        // Debug.Log("D: " + distance);
        // Debug.Log("V: " + ((distance > 0.5)? 5 : (moveSpeed * (distance * 2))));

        if (Input.GetButton("FireMouse") && reloadTimeTimer <= 0 && ammo_collection.ammoCount > 0)
        {
            Instantiate(bullet, (transform.position - transform.right), transform.rotation);
            ammo_collection.ammoCount -= 1;
            ammoDisplay.text = "AMMO: " + ammo_collection.ammoCount;
            reloadTimeTimer = reloadTime;
        }
        reloadTimeTimer -= Time.deltaTime;

        if (health_display.health <= 0)
        {
            Destroy(gameObject);
        }
    }
}