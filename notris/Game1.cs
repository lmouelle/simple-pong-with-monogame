using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace notris
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

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

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            SetWindowSize();
            //IsMouseVisible = false;

            MenuRectangle = new Rectangle(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2, 200, 200);

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

            _board = Content.Load<Texture2D>(@"Sprites\Board\Board");
            _grayBlock = Content.Load<Texture2D>(@"Sprites\Board\BG_1");
            _blackBlock = Content.Load<Texture2D>(@"Sprites\Board\BG_2");

            fontCourierNew = Content.Load<SpriteFont>(@"Fonts\CourierNew");
            fontBlox = Content.Load<SpriteFont>(@"Fonts\Blox");
            fontBloxSmall = Content.Load<SpriteFont>(@"Fonts\BloxSmall");

            mainMusic = Content.Load<Song>(@"Sounds\425556__planetronik__rock-808-beat");

            //SetMusic();
        }

        private void SetMusic()
        {
            if (MediaPlayer.Queue.ActiveSong != mainMusic)
            {
                MediaPlayer.Play(mainMusic);
            }
            MediaPlayer.IsRepeating = true;
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

            _spriteBatch.Begin();

            // Draw figures
            _spriteBatch.Draw(_board, new Vector2(0, 0), Color.White);

            // Render fonts
            _spriteBatch.DrawString(fontBlox, "headline", new Vector2(100, 100), Color.Gray);

            var  mouseState = Mouse.GetState();
            if (MenuRectangle.Contains(mouseState.X, mouseState.Y) && mouseState.LeftButton == ButtonState.Pressed)
            {
                _spriteBatch.DrawString(fontBlox, "Click Me", new Vector2(300, 300), Color.Lime);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
