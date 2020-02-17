using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

/*
[System.Serializable]
public class Player
{
    public Image status;
    public Text text;
}

public class GameController : MonoBehaviour  // Combine with KeyboardControl/RoleSelection?  EDIT: MERGED WITH KeyboardControl
{
    public Player[] players;
    public bool displayMurdererOnDeath;

    public float deathColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayerDeath(Player player)
    {
        if (displayMurdererOnDeath)
        {
            player.text.text = "M";
        }
        Color tempColor = player.status.color;
        tempColor.a = deathColor;
        player.status.color = tempColor;
    }
}
*/