using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

namespace _Scripts.Units
{
    public class LevelGenerator : StaticInstance<LevelGenerator>
    {
        public Tile blueTile;
        public Tile redTile;
        public float gapX;
        public float gapZ;
        private int columns = 8;
        private int rows = 4;

        private Dictionary<Vector2, Tile> _grid;
        public void GenerateGrid()
        {
            ClearGrid();
            _grid = new Dictionary<Vector2, Tile>();
            

            var currentTile = blueTile;
            for (int i = 0; i < columns; i++)
            {
                if (i >= 4) currentTile = redTile;
                for (int j = 0; j < rows; j++)
                {
                    Vector3 position = new Vector3(i * gapX, 0.0f, -j * gapZ) + transform.position;
                    var tile = Instantiate(currentTile,position,quaternion.identity, transform);
                    _grid[new Vector2(i, j)] = tile;
                }
            }
            GenerateReferenceBetweenTiles();
        }

        private void GenerateReferenceBetweenTiles()
        {
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    var tile = _grid[new Vector2(i, j)];
                    if (j > 0) tile.top = _grid[new Vector2(i, j-1)];
                    if (i > 0) tile.left = _grid[new Vector2(i - 1, j)];
                    if (i < columns - 1) tile.right = _grid[new Vector2(i + 1, j)];
                    if (j < rows - 1) tile.bottom = _grid[new Vector2(i, j + 1)];
                }
            }
        }

        public Tile GetTileFromGrid(Vector2 position)
        {
            return _grid[position];
        }

        private void ClearGrid()
        {
            if (_grid == null) return;
            foreach (var tile in _grid)
            {
                if (tile.Value) DestroyImmediate(tile.Value.gameObject);
                else break;
            }
            
        }
        
    }

    [CustomEditor(typeof(LevelGenerator))]
    public class LevelGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var level = (LevelGenerator)target;

            if (GUILayout.Button("Generate Grid"))
            {
                level.GenerateGrid();
            }

        }
    }


}