using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class RoleSelection : MonoBehaviour
{
    public int murdererTotal;
    internal int innocentTotal;

    List<GameObject> players = new List<GameObject>();  // Changed from array type
    GameObject currentPlayer;
    int[] indexes;
    public bool pairedPlayers;

    internal static bool isPregame = true;
    public float pregameTime;
    float pregameTimer;

    public bool displayMurdererOnDeath;

    internal static bool isGameOver = false;
    public Text gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        indexes = new int[murdererTotal];
        players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Innocent"));

        /*
        foreach (GameObject player in players)
        {
            Debug.Log(player);
        }
        */

        innocentTotal = players.Count - murdererTotal;

        if (pairedPlayers)
        {
            int playerTotal = players.Count;
            List<GameObject> playersConst = new List<GameObject>(players);

            for (int i = 1; i <= playerTotal / 2; i++)  // UNFINISHED
            {
                for (int j = 0; j < 2; j++)
                {
                    int randomIndex = Random.Range(0, players.Count);
                    players[randomIndex].tag = i.ToString();
                    players[randomIndex].GetComponent<KeyboardControl>().statusText.text = i.ToString();
                    Debug.Log("Team " + i + ": " + players[randomIndex]);
                    players.RemoveAt(randomIndex);
                }
            }
            int murdererPairNum = Random.Range(1, playerTotal / 2 + 1);
            Debug.Log(murdererPairNum);
            foreach (GameObject player in playersConst)
            {
                if (int.Parse(player.tag) == murdererPairNum)
                {
                    player.tag = "Murderer";
                    Debug.Log("Murd: " + player.name);
                }
                else
                {
                    player.tag = "Innocent";
                }
            }
        }
        else
        {
            for (int i = 0; i < murdererTotal; i++)
            {
                int randomIndex = Random.Range(0, players.Count);
                players[randomIndex].tag = "Murderer";
                Debug.Log("Murd:" + players[randomIndex]);
                players.RemoveAt(randomIndex);
            }
        }
        

        pregameTimer = pregameTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPregame)
        {
            if (pregameTimer < 0)
            {
                isPregame = false;
                Debug.Log("Pregame Ended");
            }
            pregameTimer -= Time.deltaTime;
        }

        if (murdererTotal <= 0 || innocentTotal <= 0)
        {
            isGameOver = true;

            if (murdererTotal <= 0){
                gameOverText.text = "Innocent\nWin";
            }
            if (innocentTotal <= 0)
            {
                gameOverText.text = "Murderer\nWin";
            }
            // Add player foreach loop to display murderers
        }
    }
}
