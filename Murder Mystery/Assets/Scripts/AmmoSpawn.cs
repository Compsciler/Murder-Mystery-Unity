using UnityEngine;
using System.Collections;

public class AmmoSpawn : MonoBehaviour
{
    //Declare variables
    public GameObject ammo;
    public float rate;

    //Called when the game begins
    void Start()
    {
        //Call the function over and over again
        InvokeRepeating("Generate", 0, rate);
    }

    //This function creates a new food bit
    void Generate()
    {
        // The center of the camera view is 461 by 214 pixels
        int x = Random.Range(10, Camera.main.pixelWidth - 10);
        int y = Random.Range(10, Camera.main.pixelHeight - 10);

        //Turn into word space
        Vector3 target = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 0));
        target.z = 0;

        //Create food object from prefab
        Instantiate(ammo, target, Quaternion.identity);
    }
}