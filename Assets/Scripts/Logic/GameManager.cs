using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {

    PlayerLogic playerLogic;
    GenerateFov bossVision;
    Goal goalGo;
    
    public Text beerPointsText;
    public Text winText;


    private void Start()
    {
        // Assing ogject instances
        playerLogic = FindObjectOfType<PlayerLogic>();
        goalGo = FindObjectOfType<Goal>();
    }

    // Update is called once per frame
    void Update () {

        if (goalGo == null)
        {
            goalGo = FindObjectOfType<Goal>();
            return;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            // Restart level
            GetComponentInChildren<GenerateLevel>().Generate();
        }

        
        // Handle events after winning
        if (goalGo.HasWon)
        {
            winText.gameObject.SetActive(true);
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Advance to next level
                GetComponent<GenerateLevel>().map = GetComponentInChildren<SceneController>().GetNextMap();
                GetComponent<GenerateLevel>().Generate();
            }
        
        // Handle UI event when game is still running
        } else {
            winText.gameObject.SetActive(false);
            beerPointsText.text = "Beer points collected: " + playerLogic.beerPoints.ToString() + "/" + playerLogic.maxBeerPoints.ToString();
        }
    }
}
