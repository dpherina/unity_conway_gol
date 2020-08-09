using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid {
    private float xOrigin;
    private float yOrigin;
    private const float cellSize = 1; 

    private GameObject [,] cellGrid;
   
   public Grid(int width, int height, float xOrigin, float yOrigin) {
       this.xOrigin = xOrigin;
       this.yOrigin = yOrigin;
       this.cellGrid = new GameObject[width, height];
   }

   public void renderGrid(Sprite cellSprite) {
        for (int x = 0; x < cellGrid.GetLength(0); x++) {
            for (int y = 0; y < cellGrid.GetLength(1); y++) {
                cellGrid[x,y] = createCell(x, y, cellSprite);
           }
        }
   }

   private GameObject createCell(int x, int y, Sprite cellSprite) {
        GameObject cell = new GameObject(
            "Cell("+x.ToString()+","+y.ToString()+")",
             new System.Type[] {typeof(CellComponent), typeof(SpriteRenderer), typeof(BoxCollider2D)}
        );
        cell.transform.position = new Vector3(x + xOrigin + cellSize/2, y + yOrigin + cellSize/2);
        SpriteRenderer sprite = cell.GetComponent<SpriteRenderer>();
        sprite.sprite = cellSprite;
        BoxCollider2D collider = cell.GetComponent<BoxCollider2D>();
        collider.size = new Vector2 (1, 1);
        return cell;
   }

   public void caclulateCellNeighbors(){
       for (int x = 1; x < cellGrid.GetLength(0) - 1; x++) {
        for (int y = 1; y < cellGrid.GetLength(1) - 1 ; y++) {
                CellComponent cell = cellGrid[x,y].GetComponent<CellComponent>();
                cell.neighborNumber = countNeighborCells(x,y);
            }
        }
   }

   public void updateCells() {
       foreach (GameObject cell in cellGrid) {
           cell.GetComponent<CellComponent>().updateCell();
       }
   }

   private int countNeighborCells(int cellX, int cellY) {
       int neighborCount = 0;
       for (int x = -1; x <= 1; x++) {
           for (int y = -1; y <= 1; y++) {
               if (x == 0 && y == 0) {
                continue;
               }
               int cellCheckX = cellX+x;
               int cellCheckY = cellY+y;
               neighborCount += cellGrid[cellCheckX, cellCheckY].GetComponent<CellComponent>().isAlive ? 1 : 0;
            }
        }
        return neighborCount;
    }
}



