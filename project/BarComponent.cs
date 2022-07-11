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
        private Texture2D _lhsPlayerTexture;
        private Vector2 _lhsPlayerPosition;
        private Keys _lhsPlayerUpKey;
        private Keys _lhsPlayerDownKey;
        private SpriteBatch _spriteBatch;
        private const float PlayerMoveSpeed = 3f;

        public BarComponent(Game1 game, string textureName, PlayerKind playerKind, IBallManager ballLocationProvider, Vector2 startingPos) : base(game)
        {
            _textureName = textureName;
            _playerKind = playerKind;
            _ballLocationProvider = ballLocationProvider;
            _lhsPlayerPosition = startingPos;
            if (playerKind == PlayerKind.Lhs)
            {
                _lhsPlayerDownKey = Keys.Down;
                _lhsPlayerUpKey = Keys.Up;
            }
            else
            {
                _lhsPlayerDownKey = Keys.S;
                _lhsPlayerUpKey = Keys.W;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_lhsPlayerTexture, _lhsPlayerPosition, Color.White);
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
            if (Keyboard.GetState().IsKeyDown(_lhsPlayerUpKey))
            {
                _lhsPlayerPosition -= new Vector2(0, PlayerMoveSpeed);
            }
            else if (Keyboard.GetState().IsKeyDown(_lhsPlayerDownKey))
            {
                _lhsPlayerPosition += new Vector2(0, PlayerMoveSpeed);
            }

            int maxY = Game.Window.ClientBounds.Height - _lhsPlayerTexture.Height;
            if (_lhsPlayerPosition.Y > maxY)
            {
                _lhsPlayerPosition = new Vector2(_lhsPlayerPosition.X, maxY);
            }
            if (_lhsPlayerPosition.Y < 0)
            {
                _lhsPlayerPosition = new Vector2(_lhsPlayerPosition.X, 0);
            }

            var lhsRect = new Rectangle((int)_lhsPlayerPosition.X, (int)_lhsPlayerPosition.Y, _lhsPlayerTexture.Width, _lhsPlayerTexture.Height);
            var ballRect = _ballLocationProvider.GetLocation();

            if (lhsRect.Intersects(ballRect))
            {
                _ballLocationProvider.Collide(_playerKind);
            }

            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            _lhsPlayerTexture = Game.Content.Load<Texture2D>(_textureName);
            base.LoadContent();
        }
    }
}
