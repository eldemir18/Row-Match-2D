using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiece : MonoBehaviour
{

    private int x;
    private int y;
    private Grid.PieceType type;
    private Grid grid;
    private MoveablePiece moveablePiece;
    private ColorPiece colorPiece;
    private ClearablePiece clearablePiece;

    public int X
    {
        get{return x;}
        set{if(IsMoveable()) {x = value;}}
    }

    public int Y
    {
        get{return y;}
        set{if(moveablePiece) {y = value;}}
    }

    public Grid.PieceType Type
    {
        get{return type;}
    }

    public Grid GridRef
    {
        get{return grid;}
    }

    public MoveablePiece MoveablePieceRef
    {
        get{return moveablePiece;}
    }

    public ColorPiece ColorPieceRef
    {
        get{return colorPiece;}
    }

    public ClearablePiece ClearablePiece
    {
        get{return clearablePiece;}
    }

    void Awake()
    {
        moveablePiece = GetComponent<MoveablePiece>();
        colorPiece = GetComponent<ColorPiece>();
        clearablePiece = GetComponent<ClearablePiece>();
    }

    void OnMouseEnter()
    {
        grid.EnterPiece(this);
    }

    void OnMouseDown()
    {
        grid.PressPiece(this);
    }

    void OnMouseUp()
    {
        grid.ReleasePiece();
    }


    public void Init(int _x, int _y, Grid _grid, Grid.PieceType _type)
    {
        x = _x;
        y = _y;
        grid = _grid;
        type = _type;
    }

    public bool IsMoveable()
    {
        return moveablePiece != null;
    }

    public bool IsColored()
    {
        return colorPiece != null;
    }

    public bool IsClearable()
    {
        return clearablePiece != null;
    }
}
