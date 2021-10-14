using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace notris
{
    public class NotrisGame : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;

        private const int _windowWidth = 720;
        private const int _windowHeight = 1280;

        private Texture2D _board;
        private Texture2D _grayBlock;
        private Texture2D _blackBlock;
        public SpriteFont fontCourierNew;
        public SpriteFont fontBlox;
        public SpriteFont fontBloxSmall;

        private Rectangle MenuRectangle;

        public Song mainMusic;

        public NotrisGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            var level = new LevelComponent(this);

            Components.Add(level);

            base.Initialize();
        }

        private void SetWindowSize()
        {
            _graphics.PreferredBackBufferWidth = _windowWidth;
            _graphics.PreferredBackBufferHeight = _windowHeight;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _grayBlock = Content.Load<Texture2D>(@"Sprites\Board\BG_1");
            _blackBlock = Content.Load<Texture2D>(@"Sprites\Board\BG_2");

            fontCourierNew = Content.Load<SpriteFont>(@"Fonts\CourierNew");
            fontBlox = Content.Load<SpriteFont>(@"Fonts\Blox");
            fontBloxSmall = Content.Load<SpriteFont>(@"Fonts\BloxSmall");

            mainMusic = Content.Load<Song>(@"Sounds\425556__planetronik__rock-808-beat");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }
    }
}
