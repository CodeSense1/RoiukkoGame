using UnityEngine;

public class GenerateLevel : MonoBehaviour {
    

    public ColorToPrefab[] colormappings;
    public Transform level;

    bool restarting = false;

    SceneController sc;

    [HideInInspector]
    public Texture2D map;
    Camera mainCam;


	// Use this for initialization
	void Awake () {

        sc = FindObjectOfType<SceneController>();

        map = sc.GetMap();

        

        Generate(map);
        Debug.Log("Scene created");

    }

    private void CenterCamera()
    {
        mainCam = GetComponentInChildren<Camera>();
        mainCam.orthographicSize = map.height / 2 + 2;
        mainCam.transform.position = new Vector3(map.width / 2, map.height / 2 + 1, -10);
    }

    private void Update()
    {
        if (restarting)
        {
            Generate();
            restarting = false;
        }
        
    }

    public void Generate(Texture2D newMap)
    {
        map = newMap;
        Generate();
    }

    public void Generate()
    {
        CenterCamera();
        restarting = false;

        foreach (Transform child in transform.GetChild(0))
        {
            if (child.tag == "MainCamera")
                continue;

            Destroy(child.gameObject); // this will take effect only after this frame!
        }

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
    
}
