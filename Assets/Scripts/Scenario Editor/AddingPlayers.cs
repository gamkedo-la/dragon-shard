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
    public Image[] ColorSelectButtons;

    public List<Material> InactiveColors = new List<Material>();

    public List<int> ColorsBeingUsed = new List<int>();

    public GameObject ColorsMenu;

    public GameObject Active;

    public Transform Holder;

    public Dropdown APforUnitPlacement;
    public Dropdown selectedowner;

    List<string> playernums = new List<string>();

    public bool N;

    // Start is called before the first frame update
    void Start()
    {

        players = Camera.main.GetComponent<Players>();
        NumPlayers = 0;

        AddPlayer();
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
            players.AddPlayer(NumPlayers - 1);
            tempGO.GetComponent<PlayerInfoContainer>().SetAlliance(NumPlayers - 1);

            APforUnitPlacement.ClearOptions();
            selectedowner.ClearOptions();
            playernums.Clear();

            for (int j = 0; j < NumPlayers; j++)
            {
                playernums.Add("Player " + (j + 1));
            }

            Active = tempGO;
            N = true;

            for (int k = 0; k < NumPlayers; k++)
            {
                if(ColorsBeingUsed.Contains(k) == false)
                {
                    SelectColor(k);
                    break;
                }
            }

            APforUnitPlacement.AddOptions(playernums);
            selectedowner.AddOptions(playernums);
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

            Color temp = ColorSelectButtons[F.GetComponent<PlayerInfoContainer>().CurrentColor].color;
            temp.a = 1;
            ColorSelectButtons[F.GetComponent<PlayerInfoContainer>().CurrentColor].color = temp;

            ColorsBeingUsed.Remove(F.GetComponent<PlayerInfoContainer>().CurrentColor);

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

    public void CancelChangeColor()
    {
        ColorsMenu.SetActive(false);
        Active = null;
    }

    public void NewPlayer(bool b)
    {
        N = b;
    }

    public void SelectColor(int c)
    {
        if (ColorsBeingUsed.Contains(c) == false)
        {
            Color temp;
            if (N == false)
            {
                temp = ColorSelectButtons[Active.GetComponent<PlayerInfoContainer>().CurrentColor].color;
                temp.a = 1;
                ColorSelectButtons[Active.GetComponent<PlayerInfoContainer>().CurrentColor].color = temp;

                ColorsBeingUsed.Remove(Active.GetComponent<PlayerInfoContainer>().CurrentColor);
            }

            Active.GetComponent<PlayerInfoContainer>().SetColor(PlayerColors[c], c);

            players.SetColor(Active.GetComponent<PlayerInfoContainer>().PlayerRef, PlayerColors[c]);

            ColorsBeingUsed.Add(c);

            temp = ColorSelectButtons[Active.GetComponent<PlayerInfoContainer>().CurrentColor].color;
            temp.a = 0.2f;
            ColorSelectButtons[Active.GetComponent<PlayerInfoContainer>().CurrentColor].color = temp;

            Active = null;
            ColorsMenu.SetActive(false);
        }
    }
}
