using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace project
{
    public class BarComponent : DrawableGameComponent
    {
        private readonly string _textureName;
        private readonly PlayerKind _playerKind;
        private readonly IBallManager _ballLocationProvider;
        private Texture2D _playerTexture;
        private Vector2 _playerPosition;
        private Keys _playerUpKey;
        private Keys _playerDownKey;
        private SpriteBatch _spriteBatch;
        private const float PlayerMoveSpeed = 3f;

        public BarComponent(Game1 game, string textureName, PlayerKind playerKind, IBallManager ballLocationProvider, Vector2 startingPos) : base(game)
        {
            _textureName = textureName;
            _playerKind = playerKind;
            _ballLocationProvider = ballLocationProvider;
            _playerPosition = startingPos;
            if (playerKind == PlayerKind.Lhs)
            {
                _playerDownKey = Keys.Down;
                _playerUpKey = Keys.Up;
            }
            else
            {
                _playerDownKey = Keys.S;
                _playerUpKey = Keys.W;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_playerTexture, _playerPosition, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            // Update player position based on keypress
            if (Keyboard.GetState().IsKeyDown(_playerUpKey))
            {
                _playerPosition -= new Vector2(0, PlayerMoveSpeed);
            }
            else if (Keyboard.GetState().IsKeyDown(_playerDownKey))
            {
                _playerPosition += new Vector2(0, PlayerMoveSpeed);
            }

            int maxY = Game.Window.ClientBounds.Height - _playerTexture.Height;
            if (_playerPosition.Y > maxY)
            {
                _playerPosition = new Vector2(_playerPosition.X, maxY);
            }
            if (_playerPosition.Y < 0)
            {
                _playerPosition = new Vector2(_playerPosition.X, 0);
            }

            var lhsRect = new Rectangle((int)_playerPosition.X, (int)_playerPosition.Y, _playerTexture.Width, _playerTexture.Height);
            var ballRect = _ballLocationProvider.GetLocation();

            if (lhsRect.Intersects(ballRect))
            {
                _ballLocationProvider.Collide(_playerKind);
            }

            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            _playerTexture = Game.Content.Load<Texture2D>(_textureName);
            base.LoadContent();
        }
    }
}
