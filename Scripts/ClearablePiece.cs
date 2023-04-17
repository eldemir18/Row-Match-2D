using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearablePiece : MonoBehaviour
{
    public AnimationClip clearAnimation;

    private bool isBeingCleared = false;

    public bool IsBeingCleared
    {
        get{return isBeingCleared;}
    }

    protected GamePiece piece;

    void Awake()
    {
        piece = GetComponent<GamePiece>();
    }

    public void Clear() 
    {   
        isBeingCleared = true;
        piece.GridRef.gameInformation.OnPieceClear(piece);
        StartCoroutine(ClearCoroutine());
    }

    private IEnumerator ClearCoroutine()
    {
        Animator animator = GetComponent<Animator>();

        if(animator != null)
        {
            animator.Play(clearAnimation.name);

            yield return new WaitForSeconds(clearAnimation.length);

            Destroy(gameObject);
        }
    }
}
