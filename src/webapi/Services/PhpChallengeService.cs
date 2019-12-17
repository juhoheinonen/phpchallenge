using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Services
{
    public class PhpChallengeService
    {
        Dictionary<string, int> _inputWordsAndLengths;
        HashSet<string> _foundWords;
        const int maxWordLength = 10;
        HashSet<string> _inputWordsTrie;
        int longestWordLength;

        public PhpChallengeService()
        {
        }

        public int Sanaruudukko(string rowsWithPipes, string wordsWithPipes)
        {
            _inputWordsAndLengths = new Dictionary<string, int>();
            _foundWords = new HashSet<string>();
            _inputWordsTrie = new HashSet<string>();

            // max words 20, mag width and height 10, word max length 10.
            //const int maxWords = 20;
            if (rowsWithPipes == null)
            {
                throw new Exception("rowsWithPipes == null");
            }

            if (wordsWithPipes == null)
            {
                throw new Exception("wordsWithPipes == null");
            }

            var rows = rowsWithPipes.Split("|");
            foreach (var word in wordsWithPipes.Split("|"))
            {
                _inputWordsAndLengths[word] = word.Length;
                for (int i = 1; i <= word.Length; i++)
                {
                    _inputWordsTrie.Add(word.Substring(0, i));
                    if (word.Length > longestWordLength)
                    {
                        longestWordLength = word.Length;
                    }
                }
            }

            int rowCount = rows.Length;
            int columnCount = rows.First().Length;

            var grid = BuildGrid(rows, rowCount, columnCount);

            for (int x = 0; x < columnCount; x++)
            {
                for (int y = 0; y < rowCount; y++)
                {
                    var current = grid[x, y].ToString();

                    if (_inputWordsTrie.Contains(current))
                    {
                        var alreadyTakenCoordinates = new HashSet<Point>();
                        //var alreadyTakenCoordinates = new HashSet<Point>() { new Point(x, y) };
                        FindWords(new Point(x, y), grid, current, alreadyTakenCoordinates);
                    }
                }
            }

            return _foundWords.Count;
        }

        private static char[,] BuildGrid(string[] rows, int rowCount, int columnCount)
        {
            char[,] grid = new char[columnCount, rowCount];

            for (int y = 0; y < rowCount; y++)
            {
                for (int x = 0; x < columnCount; x++)
                {
                    grid[x, y] = rows[y][x];
                    //Debug.Write(grid[x, y]);
                }
                //Debug.WriteLine("");
            }

            return grid;
        }

        private void FindWords(Point inputCoordinate, char[,] grid, string thisFar, HashSet<Point> alreadyTakenCoordinates)
        {
            if (thisFar.Length > longestWordLength)
            {
                return;
            }

            if (!_inputWordsTrie.Contains(thisFar))
            {
                return;
            }
            else if (_inputWordsAndLengths.ContainsKey(thisFar))
            {
                _foundWords.Add(thisFar);
            }

            // check left
            var coordinate = new Point(inputCoordinate.X - 1, inputCoordinate.Y);
            if (coordinate.X >= 0 && !alreadyTakenCoordinates.Contains(coordinate))
            {
                var left = grid[coordinate.X, coordinate.Y];
                var newAlreadyTaken = new HashSet<Point>(alreadyTakenCoordinates);
                //newAlreadyTaken.Add(coordinate);
                FindWords(coordinate, grid, thisFar + left, newAlreadyTaken);
            }

            // up
            coordinate = new Point(inputCoordinate.X, inputCoordinate.Y - 1);
            if (coordinate.Y >= 0 && !alreadyTakenCoordinates.Contains(coordinate))
            {
                var up = grid[coordinate.X, coordinate.Y];
                var newAlreadyTaken = new HashSet<Point>(alreadyTakenCoordinates);
                //newAlreadyTaken.Add(coordinate);
                FindWords(coordinate, grid, thisFar + up, newAlreadyTaken);
            }

            // right
            coordinate = new Point(inputCoordinate.X + 1, inputCoordinate.Y);
            if (coordinate.X < grid.GetLength(0) && !alreadyTakenCoordinates.Contains(coordinate))
            {
                var right = grid[coordinate.X, coordinate.Y];
                var newAlreadyTaken = new HashSet<Point>(alreadyTakenCoordinates);
                //newAlreadyTaken.Add(coordinate);
                FindWords(coordinate, grid, thisFar + right, newAlreadyTaken);
            }

            // down
            coordinate = new Point(inputCoordinate.X, inputCoordinate.Y + 1);
            if (coordinate.Y < grid.GetLength(1) && !alreadyTakenCoordinates.Contains(coordinate))
            {
                var down = grid[coordinate.X, coordinate.Y];
                var newAlreadyTaken = new HashSet<Point>(alreadyTakenCoordinates);
                //newAlreadyTaken.Add(coordinate);
                FindWords(coordinate, grid, thisFar + down, newAlreadyTaken);
            }
        }
    }
}
