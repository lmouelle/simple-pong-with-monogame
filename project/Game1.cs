using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
            
            _ballPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            _ballVelocity = new Vector2(150, 150);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _ballTexture = Content.Load<Texture2D>("ball");
            _rhsPlayerTexture = Content.Load<Texture2D>("red");
            _lhsPlayerTexture = Content.Load<Texture2D>("blue");
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

            int maxX = GraphicsDevice.Viewport.Width - _ballTexture.Width;
            int maxY = GraphicsDevice.Viewport.Height - _ballTexture.Height;

            if (_ballPosition.X > maxX || _ballPosition.X < 0)
            {
                // Ball hit one of the side goals, reset and score
                _ballPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
                _ballVelocity = new Vector2(150, 150);
            }

            if (_ballPosition.Y > maxY || _ballPosition.Y < 0)
            {
                _ballVelocity.Y *= -1;
            }

            // Detect collisions and respond
            var lhsRect = new Rectangle((int)_lhsPlayerPosition.X, (int)_lhsPlayerPosition.Y, _lhsPlayerTexture.Width, _lhsPlayerTexture.Height);
            var rhsRect = new Rectangle((int)_rhsPlayerPosition.X, (int)_rhsPlayerPosition.Y, _rhsPlayerTexture.Width, _rhsPlayerTexture.Height);
            var ballRect = new Rectangle((int)_ballPosition.X, (int)_ballPosition.Y, _ballTexture.Width, _ballTexture.Height);

            if (lhsRect.Intersects(ballRect))
            {
                _ballVelocity.Y -= 50;
                _ballVelocity.X += _ballVelocity.X < 0 ? -(50) : 50;
                _ballVelocity.X *= -1;
            }
            else if (rhsRect.Intersects(ballRect))
            {
                _ballVelocity.Y += 50;
                _ballVelocity.X += _ballVelocity.X < 0 ? -(50) : 50;
                _ballVelocity.X *= -1;
            }
            else if (ballRect.Y >= _graphics.PreferredBackBufferHeight)
            {
            }
            else if (ballRect.X >= _graphics.PreferredBackBufferWidth)
            {

            }

           
            base.Update(gameTime);
        }

        private void EnforceGameBounds(ref Vector2 position, Texture2D texture)
        {
            position.X = MathHelper.Clamp(position.X, 0 + (texture.Width / 2), _graphics.PreferredBackBufferWidth - texture.Width);
            position.Y = MathHelper.Clamp(position.Y, 0 + (texture.Height / 2), _graphics.PreferredBackBufferHeight - texture.Height);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            _spriteBatch.Draw(_ballTexture, _ballPosition, Color.White);
            _spriteBatch.Draw(_rhsPlayerTexture, _rhsPlayerPosition, Color.White);
            _spriteBatch.Draw(_lhsPlayerTexture, _lhsPlayerPosition, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
