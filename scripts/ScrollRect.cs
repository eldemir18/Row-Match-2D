using UnityEngine;
using System.Collections;

public class ScrollRect : MonoBehaviour
{
    public float speed = 0.5f;
    public Transform rangeTransform;
    public float damping = 0.1f;

    private Vector3 startPosition;
    private float scrollAmount;
    private bool isDragging;
    private float startDragPosY;
    private float rangeMin;
    private float rangeMax;
    private float contentMin;
    private float contentMax;

    void Start()
    {
        isDragging = false;
        startPosition = transform.position;
        rangeMin = rangeTransform.position.y - rangeTransform.lossyScale.y / 2;
        rangeMax = rangeTransform.position.y + rangeTransform.lossyScale.y / 2;
        contentMin = startPosition.y - transform.lossyScale.y / 2;
        contentMax = startPosition.y + transform.lossyScale.y / 2;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            startDragPosY = Input.mousePosition.y;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            float dragAmount = (Input.mousePosition.y - startDragPosY) * speed * Time.deltaTime;
            scrollAmount -= dragAmount;
            float targetY = Mathf.Clamp(startPosition.y - scrollAmount, 
                                        rangeMin + transform.lossyScale.y / 2, 
                                        rangeMax - transform.lossyScale.y / 2);
            transform.position = Vector3.Lerp(transform.position, new Vector3(startPosition.x, targetY, startPosition.z), damping);
        }

        CheckVisibility();
    }

    private void CheckVisibility()
    {
        foreach (Transform child in transform)
        {
            float childMin = child.position.y - child.lossyScale.y / 2;
            float childMax = child.position.y + child.lossyScale.y / 2;
            float visibleHeight = Mathf.Max(0, Mathf.Min(childMax, rangeMax) - Mathf.Max(childMin, rangeMin));
            Renderer renderer = child.gameObject.GetComponent<Renderer>();

            if (visibleHeight < child.lossyScale.y / 2)
            {
                renderer.enabled = false;
                
                foreach (Transform grandChild in child)
                {
                    grandChild.gameObject.SetActive(false);
                }
            }
            else
            {
                renderer.enabled = true;
                
                foreach (Transform grandChild in child)
                {
                    grandChild.gameObject.SetActive(true);
                }
            }
        }
    }

}

    