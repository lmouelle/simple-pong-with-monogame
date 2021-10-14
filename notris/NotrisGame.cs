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

        private Texture2D _blocksBackground;
        private Texture2D _grayBlock;
        private Texture2D _blackBlock;
        public SpriteFont _fontCourierNew;
        public SpriteFont _fontBlox;
        public SpriteFont _fontBloxSmall;

        public Song _mainMusic;

        public KeyboardState keyboardState;
        public KeyboardState prevKeyboardState;

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

            SetWindowSize();

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
            _blocksBackground = Content.Load<Texture2D>(@"Sprites\Board\Board");

            _fontCourierNew = Content.Load<SpriteFont>(@"Fonts\CourierNew");
            _fontBlox = Content.Load<SpriteFont>(@"Fonts\Blox");
            _fontBloxSmall = Content.Load<SpriteFont>(@"Fonts\BloxSmall");

            _mainMusic = Content.Load<Song>(@"Sounds\425556__planetronik__rock-808-beat");
            MediaPlayer.IsRepeating = true;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            prevKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_blocksBackground, new Vector2(0, 0), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
