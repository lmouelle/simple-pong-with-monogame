using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace project
{
    internal class ScoreComponent : DrawableGameComponent, IScoreNotificationSink
    {
        SpriteFont _font;
        private int _rhsScore;
        private int _lhsScore;
        private SpriteBatch _spriteBatch;

        public ScoreComponent(Game1 game) : base(game)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            Vector2 lhsScorePosition = new Vector2(0, 0);
            _spriteBatch.DrawString(_font, _lhsScore.ToString(), lhsScorePosition, Color.White);

            Vector2 rhsScorePosition = new Vector2(Game.Window.ClientBounds.Width - _font.MeasureString(_rhsScore.ToString()).X, 0);
            _spriteBatch.DrawString(_font, _rhsScore.ToString(), rhsScorePosition, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void IncrementScore(PlayerKind playerKind)
        {
            if (playerKind == PlayerKind.Lhs)
            {
                _lhsScore++;
            }
            else
            {
                _rhsScore++;
            }
        }

        public override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _font = Game.Content.Load<SpriteFont>("Font");
            base.LoadContent();
        }
    }
}
