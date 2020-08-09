using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class CellComponent : MonoBehaviour {
    public bool isAlive = false;

    public int neighborNumber  = 0;

    private SpriteRenderer sprite;

    private bool wasSetByClick = false;

    void Start() {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        double rand = new System.Random().NextDouble();
        //isAlive = rand > 0.5;
    }

    private void Update() {
        if (
            Input.GetMouseButton(0)
            && isMouseInCell() 
            && !EventSystem.current.IsPointerOverGameObject()
            && !wasSetByClick
        ) {
            isAlive = !this.isAlive;
            wasSetByClick = true;
        }
        sprite.color = isAlive ? Color.black : Color.white;
        
        if (Input.GetMouseButtonUp(0)) {
            wasSetByClick = false;
        }
    }

    public void updateCell() {
        isAlive = calculateIsAlive();
        wasSetByClick = false;
    }

    private bool calculateIsAlive() {
        if (isAlive && (neighborNumber == 2 || neighborNumber == 3)) {
            return true;
        } else if (!isAlive && neighborNumber == 3) {
            return true;
        } else {
            return false;
        }
    }

    //     if (isUnderpopulated() || isCrowded()) {
    //         return false;
    //     } else if (isBorn() || isContent()) {
    //         return true;
    //     } else return false;
    // }

    // private bool isUnderpopulated() {
    //     return neighborNumber < 2;
    // }

    // private bool isCrowded() {
    //     return neighborNumber > 3;
    // }

    // private bool isBorn() {
    //     return !isAlive && neighborNumber == 3;

    // }

    // private bool isContent() {
    //     return (neighborNumber == 2) || (neighborNumber == 3);
    // }

    private bool isMouseInCell() {
        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();
        Vector2 minBounds = collider.bounds.min;
        Vector2 maxBounds = collider.bounds.max;

        bool xInBounds = mouseWorldPoint[0] > minBounds[0] && mouseWorldPoint[0] < maxBounds[0];
        bool yInBounds = mouseWorldPoint[1] > minBounds[1] && mouseWorldPoint[1] < maxBounds[1];
        return xInBounds && yInBounds;
    } 
}
