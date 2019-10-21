using UnityEngine;

public class GenerateLevel : MonoBehaviour {
    

    public ColorToPrefab[] colormappings;
    public Texture2D map;
    public Transform level;

    bool restarting = false;
    
    Camera mainCam;
	// Use this for initialization
	void Awake () {

        mainCam = GetComponentInChildren<Camera>();

        mainCam.orthographicSize = map.height / 2 + 2;
        mainCam.transform.position = new Vector3(map.width / 2, map.height / 2 + 1, -10);

        Generate();
        

    }

    private void Update()
    {
        if (restarting)
        {
            Generate();
            restarting = false;
        }
        
    }

    void Generate()
    {
        for (int x = 0; x < map.width; x++)
            for (int y = 0; y < map.height; y++)
            {

                Color current = map.GetPixel(x, y);

                foreach (ColorToPrefab colormap in colormappings)
                {
                    if (colormap.color == current)
                    {
                        // Modify boss' vision
                        if (colormap.prefab.GetComponentInChildren<GenerateFov>() != null)
                        {
                            // Modify fovAngle with alpha color?
                            colormap.prefab.GetComponentInChildren<GenerateFov>().fowRadius = map.width;
                        }
                        
                        Instantiate(colormap.prefab, new Vector3(x, y, 0), Quaternion.identity, level);
                    }
                }
            }
    }

    public void restart()
    {

        restarting = true;

        foreach( Transform child in transform.GetChild(0))
        {
            if (child.tag == "MainCamera")
                continue;

            Destroy(child.gameObject); // this will take effect only after this frame!
        }
        

    }

}
