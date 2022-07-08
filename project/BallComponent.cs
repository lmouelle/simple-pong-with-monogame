using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace project
{
    public class BallComponent : DrawableGameComponent, IBallManager
    {
        private readonly Game1 _game1;
        private readonly IScoreNotificationSink _scoreNotificationSink;
        private Texture2D _ballTexture;
        private Vector2 _ballPosition;
        private Vector2 _ballVelocity;
        private SpriteBatch _spriteBatch;

        public BallComponent(Game1 game, IScoreNotificationSink scoreNotificationSink) : base(game)
        {
            _game1 = game;
            _scoreNotificationSink = scoreNotificationSink;
        }

        public void Collide(PlayerKind playerKind)
        {
            if (playerKind == PlayerKind.Lhs && _ballVelocity.X < 0)
            {
                _ballVelocity.Y -= 50;
                _ballVelocity.X += _ballVelocity.X < 0 ? -(50) : 50;
                _ballVelocity.X *= -1;
            }
            else if (playerKind == PlayerKind.Rhs && _ballVelocity.X > 0)
            {
                _ballVelocity.Y += 50;
                _ballVelocity.X += _ballVelocity.X < 0 ? -(50) : 50;
                _ballVelocity.X *= -1;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_ballTexture, _ballPosition, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public Rectangle GetLocation()
        {
            var ballRect = new Rectangle((int)_ballPosition.X, (int)_ballPosition.Y, _ballTexture.Width, _ballTexture.Height);
            return ballRect;
        }

        public override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ResetBallPosition();

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            // Bounce the ball
            _ballPosition += (float)gameTime.ElapsedGameTime.TotalSeconds * _ballVelocity;

            // TODO: Diffence between this and preferred back buffer?
            int maxX = _game1._graphics.PreferredBackBufferWidth - _ballTexture.Width;
            int maxY = _game1._graphics.PreferredBackBufferHeight - _ballTexture.Height;

            // Ball hit one of the side goals, reset and score
            if (_ballPosition.X > maxX)
            {
                _scoreNotificationSink.IncrementScore(PlayerKind.Lhs);
                ResetBallPosition();
            }
            else if (_ballPosition.X < 0)
            {
                _scoreNotificationSink.IncrementScore(PlayerKind.Rhs);
                ResetBallPosition();
            }

            if (_ballPosition.Y > maxY || _ballPosition.Y < 0)
            {
                _ballVelocity.Y *= -1;
            }

            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            _ballTexture = Game.Content.Load<Texture2D>("ball");
            base.LoadContent();
        }

        private void ResetBallPosition()
        {
            Random random = new Random();
            bool movesLeft = random.Next() % 2 == 0;
            int xPos = movesLeft ? -150 : 150;

            bool movesUp = random.Next() % 2 == 0;
            int yPos = movesUp ? 150 : -150;

            _ballPosition = new Vector2(_game1._graphics.PreferredBackBufferWidth / 2, _game1._graphics.PreferredBackBufferHeight / 2);
            _ballVelocity = new Vector2(xPos, yPos);
        }
    }
}
