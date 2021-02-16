using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddingPlayers : MonoBehaviour
{
    public GameObject[] PlayerInfoContainers;
    public Players players;
    public Transform G;

    public int bookmark = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddPlayer()
    {
        if (bookmark < 4)
        {
            PlayerInfoContainers[bookmark].SetActive(true);
            PlayerInfoContainers[bookmark].GetComponent<PlayerInfoContainer>().SetNumber(bookmark);
            PlayerInfoContainers[bookmark].GetComponent<PlayerInfoContainer>().players = players;
            PlayerInfoContainers[bookmark].GetComponent<PlayerInfoContainer>().G = G;
            bookmark++;

            players.AddPlayer();
        }
        else
        {
            Debug.Log("Maximum Players");
        }
    }

    //public void DeletePlayer(int P)
    //{
    //    foreach(GameObject G in PlayerInfoContainers)
    //    {
    //        if(G.GetComponent<PlayerInfoContainer>().PlayerRef > P)
    //        {
    //            G.GetComponent<PlayerInfoContainer>().PlayerRef -= 1;

    //        }

    //    }
    //    bookmark -= 1;
    //}
}
