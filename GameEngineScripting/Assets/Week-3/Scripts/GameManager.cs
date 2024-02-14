using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Battleship
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private int[,] grid = new int[,]
        {
            { 0,0,0,0,0 },
            { 0,0,0,0,0 },
            { 0,0,0,0,0 },
            { 0,0,0,0,0 },
            { 0,0,0,0,0 }
        };

        private bool[,] hits;

        private int nRows;
        private int nCols;

        private int row;
        private int col;

        private int score;

        private int time;

        [SerializeField] Transform gridRoot;
        [SerializeField] GameObject cellPrefab;
        [SerializeField] GameObject winLabel;
        [SerializeField] TextMeshProUGUI timeLabel;
        [SerializeField] TextMeshProUGUI scoreLabel;
        [SerializeField] GameObject[] buttons;

        private void Awake()
        {
            //Init
            //grid
            nRows = grid.GetLength(0);
            nCols = grid.GetLength(1);
            //bool
            hits = new bool[nRows, nCols];

            //prep the grid
            randomShips();

            //populate grid
            for (int i = 0; i < nRows * nCols; i++)
            {
                Instantiate(cellPrefab, gridRoot);
            }

            selectCurrentCell();
            InvokeRepeating("incrementTime", 1f, 1f);
        }

        private void randomShips()
        {
            for (int row = 0; row < nRows; row++)
            {
                for (int col = 0; col < nCols; col++)
                {
                    if (UnityEngine.Random.Range(0, 10) <= 5)
                    {
                        grid[row, col] = 1;
                    }
                    else
                    {
                        grid[row, col] = 0;
                    }
                }
            }
        }

        private Transform getCurrentCell()
        {
            int index = (row * nCols) + col;
            return gridRoot.transform.GetChild(index);
        }

        private void selectCurrentCell()
        {
            Transform cell = getCurrentCell();
            Transform cursor = cell.Find("Cursor");
            cursor.gameObject.SetActive(true);
        }

        private void unselectCurrentCell()
        {
            Transform cell = getCurrentCell();
            Transform cursor = cell.Find("Cursor");
            cursor.gameObject.SetActive(false);
        }

        public void MoveHorizontal(int amt)
        {
            unselectCurrentCell();

            col += amt;
            col = Mathf.Clamp(col, 0, nCols - 1);

            selectCurrentCell();
        }

        public void MoveVertical(int amt)
        {
            unselectCurrentCell();

            row += amt;
            row = Mathf.Clamp(row, 0, nRows - 1);

            selectCurrentCell();
        }

        private void showHit()
        {
            Transform cell = getCurrentCell();
            Transform hit = cell.Find("Hit");
            hit.gameObject.SetActive(true);
        }

        private void showMiss()
        {
            Transform cell = getCurrentCell();
            Transform miss = cell.Find("Miss");
            miss.gameObject.SetActive(true);
        }

        private void incrementScore()
        {
            score++;
            scoreLabel.text = $"Score: {score}";
        }

        public void Fire()
        {
            if (hits[row, col]) return;

            hits[row, col] = true;

            if (grid[row, col] == 1)
            {
                showHit();
                incrementScore();
                tryEndGame();
            }
            else
            {
                showMiss();
            }
        }

        private void tryEndGame()
        {
            for (int row = 0; row < nRows; row++)
            {
                for (int col = 0; col < nCols; col++)
                {
                    if (grid[row, col] == 1) continue;
                    if (hits[row, col] == false) return;
                }
                Debug.Log("made it through the loop");
            }

            Debug.Log("Win");
            winLabel.SetActive(true);
            foreach (GameObject button in buttons)
            {
                button.SetActive(false);
            }
            CancelInvoke("incrementTime");
        }

        private void incrementTime()
        {
            time++;
            timeLabel.text = $"{time / 60}:{(time % 60).ToString("00")}";
        }
    }
}
