using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Field : MonoBehaviour
{
    bool isPlanted = false;
    SpriteRenderer plant;
    Player player;
    CellMenu cellMenu;

    public Sprite[] plantStages;
    public bool isBlocked;

    int plantStage = 0;
    float timeBtwStages = 5f;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        if (!isBlocked)
        {
            plant = transform.GetChild(0).GetComponent<SpriteRenderer>();
        }
        player = GameObject.Find("PlayerInfo").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlanted && !isBlocked) 
        { 
            timer -= Time.deltaTime;
            if (timer < 0 && plantStage < plantStages.Length - 1)
            {
                timer = timeBtwStages;
                plantStage++;
                UpdatePlant();
            }
        }
    }
    private void OnMouseDown()
    {
        if (MenuManager.GameIsPaused) return;

        if (isBlocked)
        {
            print("EnlargeTheTerritory");
            return;
        }

        if (isPlanted)
        {
            if (plantStage == plantStages.Length - 1) Harvest();
            return;
        }

        OpenMenu();
    }

    private void Harvest()
    {
        isPlanted = false;
        plant.gameObject.SetActive(false);
        player.Transaction(20);
        player.GetExp(10);
    }

    public void Plant()
    {
        if (10 <= player.money)
        {
            isPlanted = true;
            plantStage = 0;
            UpdatePlant();
            timer = timeBtwStages;
            plant.gameObject.SetActive(true);
            player.Transaction(-10);
        }
    }

    private void UpdatePlant()
    {
        plant.sprite = plantStages[plantStage];
    }


    public void OpenMenu()
    {
        print(this);
        cellMenu.Open(this);
    }

    public void CloseMenu()
    {
        cellMenu.Close();
    }


}
