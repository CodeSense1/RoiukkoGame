using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {

    PlayerLogic playerLogic;
    playerMovement player;
    GenerateFov bossVision;
    SceneController sceneController; 
    Goal goalGo;
    
    public Text beerPointsText;
    public Text winText;


    private void Start()
    {
        // Assing ogject instances
        playerLogic = FindObjectOfType<PlayerLogic>();
        goalGo = FindObjectOfType<Goal>();
        player = FindObjectOfType<playerMovement>();
        sceneController = GetComponentInChildren<SceneController>();
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
            GetComponent<GenerateLevel>().Generate();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            player.Undo();
        }

        
        // Handle events after winning
        if (goalGo.HasWon)
        {
            winText.gameObject.SetActive(true);
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                print("Next map ahead!");
                // Advance to next level
                Texture2D map = sceneController.GetNextMap();
                GetComponent<GenerateLevel>().Generate(map);
            }
        
        // Handle UI event when game is still running
        } else {
            winText.gameObject.SetActive(false);
            beerPointsText.text = "Beer points collected: " + playerLogic.beerPoints.ToString() + "/" + playerLogic.maxBeerPoints.ToString();
        }
    }
}
