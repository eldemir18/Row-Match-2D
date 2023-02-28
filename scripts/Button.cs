using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    public UnityEvent onClickEvent;


    private SpriteRenderer sprite;
    private bool isLocked;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void OnMouseDown()
    {
        if(isLocked) return;

        onClickEvent.Invoke();
        sprite.color = new Color(1.0f, 1.0f, 1.0f);
    }

    void OnMouseEnter()
    {
        if(isLocked) return;

        sprite.color = new Color(0.75f, 0.75f, 0.75f);
    }

    void OnMouseExit()
    {
        if(isLocked) return;

        sprite.color = new Color(1.0f, 1.0f, 1.0f);
    }

    public void LockButton()
    {
        isLocked = true;
        sprite.color = new Color(0.1f, 0.1f, 0.1f);
        
        foreach(Transform child in transform)
        {
            if(child.name == "PlayText")
            {
                child.gameObject.SetActive(false);
            }
            else
            {
                child.gameObject.SetActive(true);
            }
        }
    }

}
