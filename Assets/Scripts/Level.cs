using UnityEngine;
using System.Collections;
using UnityTool.Libgame;

public class Level : MonoBehaviour {

    public GameObject self;
    public static int width = 10;
    public static int height = 10;
    public int[,] terrainData = new int[width, height];
    public GameObject[] sprites = new GameObject[width * height];
    public static Sprite[] walls = Resources.LoadAll<Sprite>("Texture/wall");
    public static Sprite[] roads = Resources.LoadAll<Sprite>("Texture/road");
    public static Sprite[] pics = Resources.LoadAll<Sprite>("Texture/pics");
    public static GameObject cell = Resources.Load("Prefabs/Cell") as GameObject;
    public static GameObject up_floor = Resources.Load("Prefabs/up_floor") as GameObject;
    public static GameObject down_floor = Resources.Load("Prefabs/down_floor") as GameObject;
    public static int maxData = pics.Length;
    static bool isLoaded = false;
    static public Level Instance()
    {
        return Game.levels[Game.currentLevel]; 
    }
    void Awake()
    {
        SetTerrainData();
        //LoadResource();
    }

    void SetTerrainData()
    {
        for (int i = 0; i < self.transform.childCount; i++) 
        {
            if (self.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                terrainData[i / height, i % height] = self.transform.GetChild(i).GetComponent<Cell>().number;
            }
        }
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CreateRandomMazeTerrainData(int start_x, int start_y, int end_x, int end_y, int left, int width, int bottom, int height)
    {
        if (width <= 1 || height <= 1 || start_x < left || start_y < bottom || end_x > left + width || end_y > bottom + height)
        {
            return;
        }
        int x = Random.Range(left + 1, left + width - 1);
        int y = Random.Range(bottom + 1, bottom + height - 1);
        int t1 = Random.Range(left, x), t2 = Random.Range(x + 1, left + width);
        int t3 = Random.Range(bottom, y), t4 = Random.Range(y + 1, bottom + height);
        for (int i = left; i < left + width && y != bottom + height - 1; i++) 
        {
            if (t1 != i && t2 != i)
            {
                terrainData[i, y] = (int)CellType.WALL;
            }
        }
        for (int i = bottom; i < bottom + height && x != left + width - 1; i++) 
        {
            if (t3 != i && t4 != i)
            {
                terrainData[x, i] = (int)CellType.WALL;
            }
        }
        terrainData[start_x, start_y] = (int)CellType.ROAD;
        terrainData[end_x, end_y] = (int)CellType.ROAD;
        CreateRandomMazeTerrainData(start_x, start_y, t1, y - 1, left, x - left, bottom, y - bottom);
        CreateRandomMazeTerrainData(t1, y + 1, x - 1, t4, left, x - left, y + 1, bottom + height - y - 1);
        CreateRandomMazeTerrainData(x + 1, t4, end_x, end_y, x + 1, left + width - x - 1, y + 1, bottom + height - y - 1);
        CreateRandomMazeTerrainData(x + 1, t3, t2, y - 1, x + 1, left + width - x - 1, bottom, y - bottom);
    }

    public void CreateRandomTerrainData()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                terrainData[i,j] = Random.Range(0, 2);
            }
        }
    }

    public void CreateTerrainBasedOnData()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                sprites[i * width + j] = ObjectPool.Instantiate(cell, new Vector2(i, j), Quaternion.identity, self.transform);
                if (terrainData[i,j] == (int)CellType.WALL)
                {
                    sprites[i * width + j].GetComponent<SpriteRenderer>().sprite = walls[Random.Range(0, walls.Length)];    
                }
                else// if (terrainData[i, j] == (int)CellType.ROAD)
                {
                    sprites[i * width + j].GetComponent<SpriteRenderer>().sprite = roads[Random.Range(0, roads.Length)];
                }
                if (terrainData[i, j] == (int)CellType.UPSTAIRS)
                {
                    ObjectPool.Instantiate(up_floor, new Vector3(i, j, -0.5F), Quaternion.identity, sprites[i * width + j].transform);
                }
                else if (terrainData[i, j] == (int)CellType.DOWNSTAIRS)
                {
                    ObjectPool.Instantiate(down_floor, new Vector3(i, j, -0.5F), Quaternion.identity, sprites[i * width + j].transform);
                }
                sprites[i * width + j].GetComponent<Cell>().number = terrainData[i, j];
            }
        }
    }

    public void CreateRandomMaze()
    {
        //LoadResource();
        ClearTerrain();
        CreateRandomMazeTerrainData(0, 0, width-1, height-1, 0, width, 0, height);
        terrainData[0, 0] = (int)CellType.DOWNSTAIRS;
        terrainData[width - 1, height - 1] = (int)CellType.UPSTAIRS;
        CreateTerrainBasedOnData();
    }

    public void CreateRandomTerrain()
    {
        //LoadResource();
        ClearTerrain();
        CreateRandomTerrainData();
        CreateTerrainBasedOnData();
    }

    public static void LoadResource()
    {
        if (!isLoaded)
        {
            walls = Resources.LoadAll<Sprite>("Texture/wall");
            roads = Resources.LoadAll<Sprite>("Texture/road");
            pics = Resources.LoadAll<Sprite>("Texture/Pics");
            cell = Resources.Load("Prefabs/Cell") as GameObject;
            maxData = pics.Length;
            isLoaded = true;
        }
    }

    public void ClearTerrain()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (sprites[i * width + j])
                {
                    ObjectPool.Destroy(sprites[i * width + j]);
                }
            }
        }
        terrainData = new int[width, height];
    }

    public void DestoryTerrain()
    {
        sprites = new GameObject[width * height];
        for (int i = 0; i < self.transform.childCount; )
        {
            Object.DestroyImmediate(self.transform.GetChild(i).gameObject);
        }
    }
}
