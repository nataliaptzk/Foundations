using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    public string colour;
    public bool avaliableForWork = true;
    public CreateCharacter.Jobs job;
    public string location;
}

[System.Serializable]
public class PlayerManager : MonoBehaviour
{
    public List<Player> players;
    // Start is called before the first frame update
    void Start()
    {
        players = new List<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void AddNewPlayer(string hex, CreateCharacter.Jobs job, string location = "Unknown")
    {
        Player player = new Player();
        player.colour = hex;
        player.job = job;
        player.location = location;
        players.Add(player);
    }
}
