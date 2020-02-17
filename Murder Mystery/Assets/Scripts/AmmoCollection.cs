using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AmmoCollection : MonoBehaviour
{
    public string eatTag;
    public int ammoCount;
    // public float increaseSize

    public Text display;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == eatTag || false /* Ammo has been instantiated */)
        {
            ammoCount += 1;
            // transform.localScale += new Vector3(increaseSize, increaseSize, increaseSize);
            Destroy(other.gameObject);

            display.text = "AMMO: " + ammoCount;

            /*
            public UnityEngine.UI.Text myText;
            // Get index of character.
            int charIndex = myText.text.IndexOf("S");
            // Replace text with color value for character.
            myText.text = myText.text.Replace(myText.text[charIndex].ToString(), "<color=#000000>" + myText.text[charIndex].ToString() + "</color>");
            */
        }
    }
}