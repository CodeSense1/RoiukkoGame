using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    public List<Texture2D> maps;
    public int currentMapId = 0;
    const int playSceneID = 1;
    const int menuScene = 0;

    public void GetToWork()
    {
        SceneManager.LoadScene(playSceneID);
    }

    public Texture2D GetNextMap()
    {
        print("GetNextMap called");
        if (currentMapId+1 < maps.Count)
        {
            currentMapId++;
            return maps[currentMapId];
        }

        // Return first map by default
        currentMapId = 0;
        return maps[currentMapId];

    }

    public Texture2D GetMap()
    {
        return maps[currentMapId];
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(menuScene);
    }
}
