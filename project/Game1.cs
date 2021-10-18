﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace project
{
    public class Game1 : Game
    {
        Texture2D _ballTexture;

        float _ballSpeed;
        Vector2 _ballPosition;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _ballPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            _ballSpeed = 100f;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _ballTexture = Content.Load<Texture2D>("ball");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            float AdjustedBallSpeed()
            {
                return _ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            var kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Up))
            {
                _ballPosition.Y -= AdjustedBallSpeed();
            }

            if (kstate.IsKeyDown(Keys.Down))
            {
                _ballPosition.Y += AdjustedBallSpeed();
            }

            if (kstate.IsKeyDown(Keys.Left))
            {
                _ballPosition.X -= AdjustedBallSpeed();
            }

            if (kstate.IsKeyDown(Keys.Right))
            {
                _ballPosition.X += AdjustedBallSpeed();
            }

            // Now enforce ball position in screen bounds
            EnforceGameBounds(ref _ballPosition, _ballTexture);

            base.Update(gameTime);
        }

        private void EnforceGameBounds(ref Vector2 position, Texture2D texture)
        {
            if (position.X > _graphics.PreferredBackBufferWidth - texture.Width / 2)
            {
                position.X = _graphics.PreferredBackBufferWidth - texture.Width / 2;
            }
            else if (position.X < texture.Width / 2)
            {
                position.X = texture.Width / 2;
            }

            if (position.Y > _graphics.PreferredBackBufferHeight - texture.Height / 2)
            {
                position.Y = _graphics.PreferredBackBufferHeight - texture.Height / 2;
            }
            else if (position.Y < texture.Height / 2)
            {
                position.Y = texture.Height / 2;
            }
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            _spriteBatch.Draw(_ballTexture,
                              _ballPosition,
                              null,
                              Color.White,
                              0f,
                              new Vector2(_ballTexture.Width / 2, _ballTexture.Height / 2),
                              Vector2.One,
                              SpriteEffects.None,
                              0f);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
