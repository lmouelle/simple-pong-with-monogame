﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        
        private Texture2D _ballTexture;
        private Vector2 _ballPosition;
        private Vector2 _ballVelocity;

        private Texture2D _rhsPlayerTexture;
        private Vector2 _rhsPlayerPosition;
        private Keys _rhsPlayerUpKey;
        private Keys _rhsPlayerDownKey;

        private Texture2D _lhsPlayerTexture;
        private Vector2 _lhsPlayerPosition;
        private Keys _lhsPlayerUpKey;
        private Keys _lhsPlayerDownKey;

        SpriteFont _font;
        private int _rhsScore;
        private int _lhsScore;


        private const float PlayerMoveSpeed = 3f;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            var leftCenterXPos = _graphics.PreferredBackBufferWidth * 0.25f;
            var rightCenterXPos = _graphics.PreferredBackBufferWidth * 0.75f;

            _lhsPlayerPosition = new Vector2(leftCenterXPos, _graphics.PreferredBackBufferHeight / 2);
            _rhsPlayerPosition = new Vector2(rightCenterXPos, _graphics.PreferredBackBufferHeight / 2);

            _lhsPlayerDownKey = Keys.Down;
            _lhsPlayerUpKey = Keys.Up;

            _rhsPlayerUpKey = Keys.W;
            _rhsPlayerDownKey = Keys.S;

            ResetBallPosition();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _ballTexture = Content.Load<Texture2D>("ball");
            _rhsPlayerTexture = Content.Load<Texture2D>("red");
            _lhsPlayerTexture = Content.Load<Texture2D>("blue");
            _font = Content.Load<SpriteFont>("Font");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Update player position based on keypress
            if (Keyboard.GetState().IsKeyDown(_lhsPlayerUpKey))
            {
                _lhsPlayerPosition -= new Vector2(0, PlayerMoveSpeed);
            }
            else if (Keyboard.GetState().IsKeyDown(_lhsPlayerDownKey))
            {
                _lhsPlayerPosition += new Vector2(0, PlayerMoveSpeed);
            }

            if (Keyboard.GetState().IsKeyDown(_rhsPlayerUpKey))
            {
                _rhsPlayerPosition -= new Vector2(0, PlayerMoveSpeed);
            }
            else if (Keyboard.GetState().IsKeyDown(_rhsPlayerDownKey))
            {
                _rhsPlayerPosition += new Vector2(0, PlayerMoveSpeed);
            }

            // Bounce the ball
            _ballPosition += (float)gameTime.ElapsedGameTime.TotalSeconds * _ballVelocity;
            
            // TODO: Diffence between this and preferred back buffer?
            int maxX = GraphicsDevice.Viewport.Width - _ballTexture.Width;
            int maxY = GraphicsDevice.Viewport.Height - _ballTexture.Height;

            // Ball hit one of the side goals, reset and score
            if (_ballPosition.X > maxX)
            {
                _lhsScore++;
                ResetBallPosition();
            }
            else if (_ballPosition.X < 0)
            {
                _rhsScore++;
                ResetBallPosition();
            }

            if (_ballPosition.Y > maxY || _ballPosition.Y < 0)
            {
                _ballVelocity.Y *= -1;
            }

            // Detect collisions and respond
            var lhsRect = new Rectangle((int)_lhsPlayerPosition.X, (int)_lhsPlayerPosition.Y, _lhsPlayerTexture.Width, _lhsPlayerTexture.Height);
            var rhsRect = new Rectangle((int)_rhsPlayerPosition.X, (int)_rhsPlayerPosition.Y, _rhsPlayerTexture.Width, _rhsPlayerTexture.Height);
            var ballRect = new Rectangle((int)_ballPosition.X, (int)_ballPosition.Y, _ballTexture.Width, _ballTexture.Height);

            if (lhsRect.Intersects(ballRect) && _ballVelocity.X < 0)
            {
                _ballVelocity.Y -= 50;
                _ballVelocity.X += _ballVelocity.X < 0 ? -(50) : 50;
                _ballVelocity.X *= -1;
            }
            else if (rhsRect.Intersects(ballRect) && _ballVelocity.X > 0)
            {
                _ballVelocity.Y += 50;
                _ballVelocity.X += _ballVelocity.X < 0 ? -(50) : 50;
                _ballVelocity.X *= -1;
            }
           
            base.Update(gameTime);
        }

        private void ResetBallPosition()
        {
            Random random = new Random();
            bool movesLeft = random.Next() % 2 == 0;
            int xPos = movesLeft ? -150 : 150;

            bool movesUp = random.Next() % 2 == 0;
            int yPos = movesUp ? 150 : -150;

            _ballPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            _ballVelocity = new Vector2(xPos, yPos);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            _spriteBatch.Draw(_ballTexture, _ballPosition, Color.White);
            _spriteBatch.Draw(_rhsPlayerTexture, _rhsPlayerPosition, Color.White);
            _spriteBatch.Draw(_lhsPlayerTexture, _lhsPlayerPosition, Color.White);

            Vector2 lhsScorePosition = new Vector2(0, 0);
            _spriteBatch.DrawString(_font, _lhsScore.ToString(), lhsScorePosition, Color.White);

            Vector2 rhsScorePosition = new Vector2(Window.ClientBounds.Width - _font.MeasureString(_rhsScore.ToString()).X, 0);
            _spriteBatch.DrawString(_font, _rhsScore.ToString(), rhsScorePosition, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
