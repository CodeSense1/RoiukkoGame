using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {

    PlayerLogic playerLogic;
    playerMovement player;
    SceneController sceneController;
    GenerateFov vision;
    Goal goalGo;
    
    public Text beerPointsText;
    public Text winText;
    public GameObject loseText;


    private void Start()
    {
        // Assing ogject instances
        findComponents();
        
    }


    public void findComponents()
    {
        playerLogic = FindObjectOfType<PlayerLogic>();
        goalGo = FindObjectOfType<Goal>();
        player = FindObjectOfType<playerMovement>();
        sceneController = GetComponentInChildren<SceneController>();
        vision = FindObjectOfType<GenerateFov>();
    }

    // Update is called once per frame
    void Update () {

        if (goalGo == null)
        {
            goalGo = FindObjectOfType<Goal>();
            return;
        }
        if (vision == null)
        {
            vision = FindObjectOfType<GenerateFov>();
        } else
        {
            if (vision.isTargetVisible)
            {
                loseText.SetActive(true);
                player.CanMove = false;
            }
            else
            {
                player.CanMove = true;
                loseText.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            // Restart level
            loseText.SetActive(false);
            player.ResetPlayer();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            loseText.SetActive(false);
            player.Undo();
        }
        
        // Handle events after winning
        if (goalGo.HasWon)
        {
            winText.gameObject.SetActive(true);
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Advance to next level
                player.ResetPlayer();
                Texture2D map = sceneController.GetNextMap();
                GetComponent<GenerateLevel>().Generate(map);
                findComponents();
            }
        
        // Handle UI event when game is still running
        } else {
            winText.gameObject.SetActive(false);
            beerPointsText.text = "Beer points collected: " + playerLogic.beerPoints.ToString() + "/" + playerLogic.maxBeerPoints.ToString();
        }
    }
}
