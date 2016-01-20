using UnityEngine;
using System.Collections;
using UnityTool.Libgame;

public class Level : MonoBehaviour {

    public GameObject self;
    public static int width = 10;
    public static int height = 10;
    int[,] data = new int[width, height];
    public Cell[,] cells = new Cell[width, height];

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
                cells[i / height, i % height] = self.transform.GetChild(i).GetComponent<Cell>();
            }
        }
    }
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

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
                data[i, y] = (int)CellType.WALL;
            }
        }
        for (int i = bottom; i < bottom + height && x != left + width - 1; i++)
        {
            if (t3 != i && t4 != i)
            {
                data[x, i] = (int)CellType.WALL;
            }
        }
        data[start_x, start_y] = (int)CellType.ROAD;
        data[end_x, end_y] = (int)CellType.ROAD;
        CreateRandomMazeTerrainData(start_x, start_y, t1, y - 1, left, x - left, bottom, y - bottom);
        CreateRandomMazeTerrainData(t1, y + 1, x - 1, t4, left, x - left, y + 1, bottom + height - y - 1);
        CreateRandomMazeTerrainData(x + 1, t4, end_x, end_y, x + 1, left + width - x - 1, y + 1, bottom + height - y - 1);
        CreateRandomMazeTerrainData(x + 1, t3, t2, y - 1, x + 1, left + width - x - 1, bottom, y - bottom);
    }

    public void CreateTerrainBasedOnData()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                cells[i, j] = ObjectPool.Instantiate(LoadResources.cell, new Vector2(i, j), Quaternion.identity, self.transform).GetComponent<Cell>();
                if (data[i, j] == (int)CellType.ROAD)
                {
                    cells[i, j].GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 1);
                }
                else if (data[i, j] == (int)CellType.WALL)
                {
                    cells[i, j].GetComponent<SpriteRenderer>().sprite = LoadResources.wall;
                }
                else if (data[i, j] == (int)CellType.up_floor)
                {
                    cells[i, j].GetComponent<SpriteRenderer>().sprite = LoadResources.up_floor;
                }
                else if (data[i, j] == (int)CellType.down_floor)
                {
                    cells[i, j].GetComponent<SpriteRenderer>().sprite = LoadResources.down_floor;
                }
                cells[i, j].id = data[i, j];
            }
        }
    }

    public void CreateRandomMaze()
    {
        ClearTerrain();
        CreateRandomMazeTerrainData(0, 0, width - 1, height - 1, 0, width, 0, height);
        data[0, 0] = (int)CellType.down_floor;
        data[width - 1, height - 1] = (int)CellType.up_floor;
        CreateTerrainBasedOnData();
    }

    public void ClearTerrain()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (cells[i, j])
                {
                    ObjectPool.Destroy(cells[i, j].gameObject);
                }
            }
        }
        for (int i = 0; i < self.transform.childCount;)
        {
            Object.DestroyImmediate(self.transform.GetChild(i).gameObject);
        }
        data = new int[width, height];
        cells = new Cell[width, height];
    }

    public void SetColor(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
    }
}
