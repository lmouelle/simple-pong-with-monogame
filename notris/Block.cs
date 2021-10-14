using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace notris
{
    public class Block
    {
        /// <summary>
        /// Example for L block:
        /// 0010
        /// 0010
        /// 0010
        /// 0011
        /// 
        /// Only ever 4x4 in size
        /// </summary>
        public readonly int[,] tileShape;

        public Vector2 positionOnBoard;

        public Block(int[,] tileShape)
        {
            this.tileShape = CopyTiles(tileShape);

            positionOnBoard = new Vector2(0, 0);
        }

        private int[,] CopyTiles(int[,] tiles)
        {
            int[,] newTiles = new int[4, 4];
            for (int j = 0; j < 4; j++)
                for (int i = 0; i < 4; i++)
                    newTiles[i, j] = tiles[i, j];
            return newTiles;
        }

        public void Fall()
        {
            positionOnBoard.Y++;
        }

        public void Rotate()
        {
            // temp array
            int[,] a = CopyTiles(tileShape);
            // rotate the array by swapping coordinates, like it was a matrix
            for (int y = 0; y < 4; y++)
                for (int x = 0; x < 4; x++)
                    tileShape[x, y] = a[y, 3 - x];
        }

        public void Draw(Vector2 border, SpriteBatch spriteBatch, Texture2D sprite)
        {
            for (int j = 0; j < 4; j++)
                for (int i = 0; i < 4; i++)
                    if (tileShape[i, j] > 0)
                        spriteBatch.Draw(sprite, new Vector2(border.X + (i + positionOnBoard.X) * sprite.Width,
                        border.Y + (j + positionOnBoard.Y) * sprite.Height), Color.White);
        }
    }
}
