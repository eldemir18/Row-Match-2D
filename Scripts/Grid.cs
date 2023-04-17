using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Grid : MonoBehaviour
{
    public enum PieceType 
    {
        FINISHED,
        NORMAL,
        COUNT,
    };

    [System.Serializable]
    public struct PiecePrefab 
    {
        public PieceType type;
        public GameObject prefab;
    };

    private int xDim;
    private int yDim;
    private int[,] gridVals;

    public float animTime;
    public GameInformation gameInformation;

    public PiecePrefab[] piecePrefabs;
    public GameObject backgroundPrefab;

    private Dictionary<PieceType, GameObject> piecePrefabDict;

    private GamePiece[,] pieces;

    private GamePiece pressedPiece;
    private GamePiece enteredPiece;

    private bool gameOver;

    void Awake()
    {
        xDim = StateInformation.xDim;
        yDim = StateInformation.yDim;
        gridVals = StateInformation.gridVals;
    }

    void OnEnable()
    {
        gameInformation.onGameOver += GameOver;    
    }

    void OnDisable()
    {
        gameInformation.onGameOver -= GameOver; 
    }

    void Start()
    {
        piecePrefabDict = new Dictionary<PieceType, GameObject>();

        for (int i = 0; i < piecePrefabs.Length; i++)
        {
            if (!piecePrefabDict.ContainsKey(piecePrefabs[i].type))
            {
                piecePrefabDict.Add(piecePrefabs[i].type, piecePrefabs[i].prefab);
            }
        }

        // Creating background grid
        for (int x = 0; x < xDim; x++)
        {
            for (int y = 0; y < yDim; y++)
            {
                GameObject background = Instantiate(backgroundPrefab, GetWorldPosition(x, y), Quaternion.identity, transform);

                // Check if background is on the left edge
                if (x == 0)
                {
                    Transform leftBorder = background.transform.GetChild(0); 
                    leftBorder.gameObject.SetActive(true);
                }

                // Check if background is on the right edge
                if (x == xDim - 1)
                {
                    Transform rightBorder = background.transform.GetChild(1); 
                    rightBorder.gameObject.SetActive(true);
                }

                // Check if background is on the top edge
                if (y == yDim - 1)
                {
                    Transform topBorder = background.transform.GetChild(2);
                    topBorder.gameObject.SetActive(true);
                }

                // Check if background is on the bottom edge
                if (y == 0)
                {
                    Transform bottomBorder = background.transform.GetChild(3);
                    bottomBorder.gameObject.SetActive(true);
                }
            }
        }

        // Creating pieces
        pieces = new GamePiece[xDim,yDim];
        for (int x = 0; x < xDim; x++)
        {
            for (int y = 0; y < yDim; y++)
            {
                // Make new piece
                SpawnNewPiece(x, y, PieceType.NORMAL);
          
                // If piece is colorable randomly color it
                if(pieces[x, y].IsColored())
                {
                    // Color from file
                    pieces[x, y].ColorPieceRef.SetColor((ColorPiece.ColorType)gridVals[x, y]);
                }
            }
        }
    }

    public Vector2 GetWorldPosition (int x, int y)
    {
        // Set offsets if x or y is odd
        float offsetX = 0.5f;
        float offsetY = 0.5f;

        if (xDim % 2 == 1) offsetX = 0f;
        
        // Center grid
        return new Vector2(transform.position.x - xDim / 2 + x + offsetX, transform.position.y - yDim / 2 + y - offsetY);
    } 

    public GamePiece SpawnNewPiece(int x, int y, PieceType type)
    {
        GameObject newPiece = Instantiate(piecePrefabDict[type], GetWorldPosition(x, y), Quaternion.identity);
        newPiece.transform.parent = transform;

        pieces[x, y] = newPiece.GetComponent<GamePiece>();
        pieces[x, y].Init(x, y, this, type);

        return pieces[x, y];
    }

    public bool IsAdjacent(GamePiece piece1, GamePiece piece2)
    {
        return (piece1.X == piece2.X && (int)Mathf.Abs(piece1.Y - piece2.Y) == 1)
            || (piece1.Y == piece2.Y && (int)Mathf.Abs(piece1.X - piece2.X) == 1);
    }

    public void SwapPieces(GamePiece piece1, GamePiece piece2)
    {
        // Swap locations in pieces array
        pieces[piece1.X, piece1.Y] = piece2;
        pieces[piece2.X, piece2.Y] = piece1;

        // Temporary position hold
        int piece1X = piece1.X;
        int piece1Y = piece1.Y;

        // Move two pieces
        piece1.MoveablePieceRef.Move(piece2.X, piece2.Y, animTime);
        piece2.MoveablePieceRef.Move(piece1X, piece1Y, animTime);   
    }

    public void PressPiece(GamePiece piece)
    {
        pressedPiece = piece;
    }

    public void EnterPiece(GamePiece piece)
    {
        enteredPiece = piece;
    }

    public void ReleasePiece()
    {
        // If game over not move
        if(gameOver) return;

        // If piece not moveable not move
        if(!enteredPiece.IsMoveable() || !pressedPiece.IsMoveable()) return;

        // If pieces not adjacent not move
        if(!IsAdjacent(pressedPiece, enteredPiece)) return;
        
        // Swap pieces
        SwapPieces(pressedPiece, enteredPiece);
        
        // Look for a match
        LookMatch(pressedPiece, enteredPiece);

        // Call on move
        gameInformation.OnMove();

        // Check for further matches
        if(!AreThereFurtherMatches()) gameInformation.GameOver();
    }

    public void LookMatch(GamePiece piece1, GamePiece piece2)
    {
        // If there is match in piece1 row clear all row
        if(CheckMatch(piece1)) ClearPieces(piece1.Y);
        
        // If there is match in piece2 row clear all row
        if(CheckMatch(piece2)) ClearPieces(piece2.Y);
    }

    private bool CheckMatch(GamePiece piece) 
    {
        // If all row colors are not same return false
        for (int x = 0; x < xDim; x++) 
        {
            if(pieces[x, piece.Y].ColorPieceRef.Color != piece.ColorPieceRef.Color) return false;
        }

        return true;
    }

    private void ClearPieces(int y) 
    {
        // Clears all row
        for (int x = 0; x < xDim; x++) 
        {
            ClearPiece(x, y);
        }
    }

    public bool ClearPiece(int x, int y)
    {
        if(pieces[x, y].IsClearable() && !pieces[x, y].ClearablePiece.IsBeingCleared)
        {
            pieces[x, y].ClearablePiece.Clear();
            SpawnNewPiece(x, y, PieceType.FINISHED);
            
            return true;
        }

        return false;
    }

    public void GameOver()
    {
        gameOver = true;
    }

    public bool AreThereFurtherMatches()
    {
        Dictionary<ColorPiece.ColorType, int> colorCountDict = new Dictionary<ColorPiece.ColorType, int>();

        for (int y = 0; y < yDim; y++)
        {
            for (int x = 0; x < xDim; x++)
            {
                // If it is finished line clear dictionary
                if(!pieces[x, y].IsColored())
                {
                    colorCountDict.Clear();
                    break;
                }

                // Add color to dict
                ColorPiece.ColorType color = pieces[x, y].ColorPieceRef.Color;
                colorCountDict[color] = colorCountDict.ContainsKey(color) ? colorCountDict[color] + 1 : 1;

                // If there is match
                if (colorCountDict[pieces[x, y].ColorPieceRef.Color] == xDim) return true;
            }
        }
        
        return false;
    }
}
