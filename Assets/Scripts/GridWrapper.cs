using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridWrapper : MonoBehaviour {
    public int width = 0;
    public int height = 0;

    public int orignX = 0;
    public int originY = 0;

    public bool shouldUpdate = true;

    public float updateFrequency = 1f;

    public Sprite cellSprite;
    private Grid grid;
    void Start() {
        grid = new Grid(width, height, orignX, originY);
        grid.renderGrid(cellSprite);
        StartCoroutine("calculateCellNeighors");
        StartCoroutine("updateGridCells");
    }

    public void toggleShouldUpdate() {
        shouldUpdate = !shouldUpdate;
    }

    public void changeUpdateFrequency(float speed) {
        updateFrequency = 1f/speed;
    }

    IEnumerator calculateCellNeighors() {
        for (;;) {
        grid.caclulateCellNeighbors();
        yield return new WaitForSeconds(updateFrequency);
        }
    }

    IEnumerator updateGridCells() {
    for(;;) {
        if (shouldUpdate) {
            grid.updateCells();
        }
        yield return new WaitForSeconds(updateFrequency);
    }
}
}
