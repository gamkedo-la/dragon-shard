using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddingPlayers : MonoBehaviour
{
    public Players players;

    public List<GameObject> PlayerInfoContainers = new List<GameObject>();

    public GameObject PIC;

    public int NumPlayers = 0;

    public int spacing;

    public Material[] PlayerColors;

    public List<Material> InactiveColors = new List<Material>();

    public List<int> ColorsBeingUsed = new List<int>();

    public GameObject ColorsMenu;

    public GameObject Active;

    public Transform Holder;

    public Dropdown APforUnitPlacement;

    List<string> playernums = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        NumPlayers = 0;

        AddPlayer();

        //foreach(GameObject C in PlayerInfoContainers)
        //{
        //    Active = C;
        //}

        //SelectColor(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddPlayer()
    {

        if (NumPlayers < 4)
        {
            GameObject tempGO = Instantiate(PIC, Holder);
            RectTransform rtTemp = tempGO.GetComponent<RectTransform>();
            rtTemp.localPosition = new Vector2(((rtTemp.rect.width + spacing) * NumPlayers) + 10, -10);
            //Debug.Log("setting player number " + NumPlayers);
            tempGO.GetComponent<PlayerInfoContainer>().SetNumber(NumPlayers);
            tempGO.GetComponent<PlayerInfoContainer>().players = players;
            tempGO.GetComponent<PlayerInfoContainer>().AP = GetComponent<AddingPlayers>();

            NumPlayers++;
            PlayerInfoContainers.Add(tempGO);
            players.AddPlayer();
            tempGO.GetComponent<PlayerInfoContainer>().SetAlliance(1);

            APforUnitPlacement.ClearOptions();
            playernums.Clear();

            for (int j = 0; j < NumPlayers; j++)
            {
                playernums.Add("Player " + (j + 1));
            }

            Active = tempGO;

            for (int k = 0; k < NumPlayers; k++)
            {
                if(ColorsBeingUsed.Contains(k) == false)
                {
                    SelectColor(k);
                    break;
                }
            }

            APforUnitPlacement.AddOptions(playernums);
            //APforUnitPlacement.itemImage.color = Color.green;

        }        
        else
        {
            Debug.Log("Maximum Players");
        }
    }

    public void DeletePlayer(int P, GameObject F)
    {
        if (NumPlayers > 1)
        {
            PlayerInfoContainers.Remove(F);

            foreach (GameObject G in PlayerInfoContainers)
            {
                if (G.GetComponent<PlayerInfoContainer>().PlayerRef > P)
                {
                    G.GetComponent<PlayerInfoContainer>().SetNumber(G.GetComponent<PlayerInfoContainer>().PlayerRef - 1);
                    RectTransform rtTemp = G.GetComponent<RectTransform>();
                    rtTemp.localPosition = new Vector2(rtTemp.localPosition.x - rtTemp.rect.width - spacing, -10);
                }
            }
            players.DeletePlayer(P);
            NumPlayers--;
        }
        else
        {
            Debug.Log("can't delete last player");
        }
    }

    public void OpenColorsMenu(GameObject A)
    {
        ColorsMenu.SetActive(true);
        Active = A;
    }

    public void SelectColor(int c)
    {

        Active.GetComponent<PlayerInfoContainer>().SetColor(PlayerColors[c]);

        players.SetColor(Active.GetComponent<PlayerInfoContainer>().PlayerRef, PlayerColors[c]);

        ColorsBeingUsed.Add(c);

        Active = null;
        ColorsMenu.SetActive(false);

    }
}
