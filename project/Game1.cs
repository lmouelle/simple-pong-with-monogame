using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace project
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager _graphics;

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

            var scoreComponent = new ScoreComponent(this);
            var ballComponent = new BallComponent(this, scoreComponent);
            var lhsComponent = new BarComponent(this, "red", PlayerKind.Lhs, ballComponent, new Vector2(leftCenterXPos, _graphics.PreferredBackBufferHeight / 2));
            var rhsComponent = new BarComponent(this, "blue", PlayerKind.Rhs, ballComponent, new Vector2(rightCenterXPos, _graphics.PreferredBackBufferHeight / 2));

            Components.Add(scoreComponent);
            Components.Add(ballComponent);
            Components.Add(lhsComponent);
            Components.Add(rhsComponent);

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
