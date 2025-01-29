using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisSpawner : MonoBehaviour
{
    //public List<GameObject> tetrominoes;
    public GameObject[] tetrominoePrefabs;
    private TetrisGrid grid;
    private GameObject nextPiece;

    // Start is called before the first frame update
    void Start()
    {
        grid = FindObjectOfType<TetrisGrid>();
        if (grid == null)
        {
            Debug.LogError("NO GRID AHHHHH");
            return;
        }
        SpawnPiece();
    }

    public void SpawnPiece() 
    {
        //calculate center of grid based on dimensions
        Vector3 spawnPosition = new Vector3(
            Mathf.Floor(grid.width / 2), //x
            grid.height,             //y
            0);                          //z
            
        if(nextPiece != null ) 
        {
            nextPiece.SetActive(true);
            nextPiece.transform.position = spawnPosition;
        }
        else
        {
            nextPiece = InstantiateRandomPiece();
            nextPiece.transform.position = spawnPosition;
        }

        nextPiece = InstantiateRandomPiece();
        nextPiece.SetActive(false);
    }

    private GameObject InstantiateRandomPiece()
    {
        int Index = Random.Range(0, tetrominoePrefabs.Length);
        return Instantiate(tetrominoePrefabs[Index]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
