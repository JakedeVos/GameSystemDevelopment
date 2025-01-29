using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisGrid : MonoBehaviour
{
    public int width = 10;
    public int height = 20; 

    public Transform[,] grid;
    public Transform[,] debugrid;

    private TetrisManager tetrisManager;

    // Start is called before the first frame update
    void Start()
    {
        tetrisManager = FindObjectOfType<TetrisManager>();
        grid = new Transform[width, height + 3];
        debugrid = new Transform[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject cell = new GameObject($"cell ({i},{j})");
                cell.transform.position = new Vector3(i, j, 0);
                debugrid[i, j] = cell.transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsCellOccupied(Vector2Int position)
    {
        if(position.x < 0 || position.x >= width || position.y < 0 || position.y >= height + 3)
        {
            return true;
        }
        return grid[position.x, position.y] != null;
    }

    public bool IsLineFull(int y)
    {
        for(int x = 0; x < width; x++)
        {
            if (grid[x, y] == null)
            {
                return false;
            }
        }
        return true;
    }

    public void ClearFullLines()
    {
        int linesCleared = 0;
        for (int y = 0; y < height; y++) 
        { 
            if(IsLineFull(y))
            {
                ClearLine(y);
                ShiftRowsDown(y);
                y--;
                linesCleared++;
            }
        }
        if (linesCleared > 0)
        {
            tetrisManager.CalculateScore(linesCleared);
        }
    }

    public void ClearLine(int y)
    {
        for (int x = 0; x < width; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    public void ShiftRowsDown(int clearedRow)
    {
        for(int y = clearedRow; y < height - 1; y++)
        {
            for(int x = 0; x < width; x++) 
            {
                grid[x, y] = grid[x, y + 1];

                if (grid[x, y] != null)
                {
                    grid[x, y].position += Vector3.down;
                }
                grid[x, y + 1] = null;
            }
        }
    }

    public void AddBlockToGrid(Transform block, Vector2Int position)
    {
        grid[position.x, position.y] = block;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        if (debugrid != null) 
        { 
        for (int i = 0; i < width; i++) 
            { 
            for (int j = 0;j < height; j++)
                {
                    Gizmos.DrawWireCube(debugrid[i, j].position, Vector3.one);
                }
            }
        }
    }
}
