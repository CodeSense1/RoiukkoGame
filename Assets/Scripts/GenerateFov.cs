using UnityEngine;


[ExecuteInEditMode]
public class GenerateFov : MonoBehaviour {
    
    public Transform target;
    public Transform fowDir;
    public SpriteRenderer indicator;

    GenerateLevel levelgenerator;
    Transform parent;

    public bool isTargetVisible = false;

    //public MeshFilter meshFilter;
    Mesh viewMesh;
    
    public float fowRadius = 5f;
    public int density = 7;

    [Range(0, 255)]
    public byte r;
    [Range(0, 255)]
    public byte g;
    [Range(0, 255)]
    public byte b;
    [Range(0, 255)]
    public byte fovAlpha = 100;

    [Range(0, 360)]
    public float fowAngle;
    
    //Use this for initialization

    void Start()
    {

        viewMesh = new Mesh();
        viewMesh.name = "View mesh";
        GetComponent<MeshFilter>().mesh = viewMesh;
        levelgenerator = GetComponentInParent<GenerateLevel>();
        parent = GetComponentInParent<Boss>().gameObject.transform;

    }

    

    Vector3 rotateVector(Vector3 vecToRotate, float angle)
    {
        float x = vecToRotate.x;
        float y = vecToRotate.y;
        float sin = Mathf.Sin(angle * Mathf.Deg2Rad);
        float cos = Mathf.Cos(angle * Mathf.Deg2Rad);

        float newX = x * cos - y * sin;
        float newY = x * sin + y * cos;

        return new Vector3(newX, newY);

    }


    Vector3[] findVisibleVertices(Vector3 lookDirection)
    {
        // Find all colliders within the fowrange
        // If no colliders, just direction vector is added
        lookDirection.Normalize();
        float fii = fowAngle / density;

        // NOTE both arg- vertors, begin and end are calculated in visible vertices
        Vector3[] visibleVertices = new Vector3[density + 1];
        visibleVertices[0] = Vector3.zero;

        // Define startposition
        Vector3 fowStart = rotateVector(lookDirection, -fowAngle/2);

        for (int i = 1; i < density +1; i++)
        {

            // Cast rays relative to initial vector
            RaycastHit2D ray = Physics2D.Raycast(parent.position, rotateVector(fowStart, fii * i), fowRadius);

            // There is nothing in vision range
            if (ray.collider == null)
            {
                visibleVertices[i] = rotateVector(fowStart, fii * i) * fowRadius;
            } else {

                // Ray hit item, that should be hidden
                if (ray.collider.tag == "pushAndHide")
                {
                    levelgenerator.restart();
                }
                
                // Ray hit some collider
                
                visibleVertices[i] = (Vector3)ray.point - parent.position;
            }
        }
        
        
        return visibleVertices;
    }

    // Set colors for each vertice
    Color32[] setColors32(int length)
    {
        Color32[] cols32 = new Color32[length];

        for (int i = 0; i < length; i++)
        {
            cols32[i] = new Color32(r, g, b, fovAlpha);
        }

        return cols32;
    }

    void drawFov(float angle)
    {
        Vector3[] vertices = findVisibleVertices(rotateVector(fowDir.position, transform.rotation.z) - transform.position);
        

        int[] triangles = new int[(vertices.Length-2)*3];
        Color32[] colors32 = setColors32(vertices.Length);

        int vertexindex = density;

        // vertices are arranged from back of the array to the front
        
        for (int i = 0; i < triangles.Length-2; i++)
        {
            if (i % 3 == 0)
            {
                triangles[i] = 0;
                triangles[i + 1] = --vertexindex;
                vertexindex++;
                triangles[i + 2] = vertexindex;
                vertexindex--;
            }
        }
        

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.colors32 = colors32;

    }

    // Update is called once per frame

    void Test()
    {
        Vector3[] verts = new Vector3[3]
        {
            new Vector3(0,0,0),
            new Vector3(1,0,0),
            new Vector3(1,1,0),

        };

        int[] tris = new int[3]
        {
            0,1,2
        };

        viewMesh.Clear();

        viewMesh.vertices = verts;
        viewMesh.triangles = tris;

    }

    void LateUpdate()
    {

        
        drawFov(fowAngle);

        //Test();

        if (isTargetVisible)
        {
            indicator.color = Color.red;
        }
        else
            indicator.color = Color.green;
    }
}
