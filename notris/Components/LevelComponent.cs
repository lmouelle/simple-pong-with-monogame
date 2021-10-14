using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace notris
{
    public class LevelComponent : DrawableGameComponent
    {
        private Vector2 _gameBoardPosition;
        private Block _block;
        private Texture2D _backgroundSprite;
        private Texture2D _grayBlockSprite;
        private Texture2D _blackBlockSprite;

        public LevelComponent(NotrisGame game) : base(game)
        {
        }

        protected override void LoadContent()
        {
            _backgroundSprite = Game.Content.Load<Texture2D>(@"Sprites\Board\Board");
            _grayBlockSprite = Game.Content.Load<Texture2D>(@"Sprites\Board\BG_1");
            _blackBlockSprite = Game.Content.Load<Texture2D>(@"Sprites\Board\BG_2");

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            var spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            if (_backgroundSprite != null)
            {
                // Draw figures
                spriteBatch.Begin();
                spriteBatch.Draw(_backgroundSprite, new Vector2(0, 0), Color.White);
                _block.Draw(_gameBoardPosition, new SpriteBatch(Game.GraphicsDevice), _grayBlockSprite);
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            int[,] pattern = new int[4, 4];
            pattern[1, 0] = 1;
            pattern[2, 0] = 1;
            pattern[0, 1] = 1;
            pattern[1, 1] = 1;
            _block = new Block(pattern);

            _gameBoardPosition = new Vector2(366, 50);

            _grayBlockSprite = Game.Content.Load<Texture2D>(@"Sprites\Shape Blocks\S");
        }
    }
}
