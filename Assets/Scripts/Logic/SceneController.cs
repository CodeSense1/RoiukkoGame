using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    public Texture2D[] maps;
    public int currentMapId = 0;
    int playSceneID = 1;
    

    public void GetToWork()
    {
        SceneManager.LoadScene(playSceneID);
        
    }

    public Texture2D GetNextMap()
    {
        Texture2D map = null;
        if (currentMapId > maps.Length)
        {
             map = maps[currentMapId];
            currentMapId++;
        }
        
        if ( map == null ) {
            map = maps[0];
        }
        return map;
        

    }
}
