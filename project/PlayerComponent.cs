using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace project
{
    public class PlayerComponent : GameComponent
    {
        public Keys DownKey { get; set; }
        public Keys UpKey { get; set; }
        public float Speed { get; set; } = 5f;
        public Vector2 Position { get; set; }
        
        public int TextureHeight { get; }
        public int ScreenHeight { get; }

        public PlayerComponent(Game game, int textureHeight, int screenHeight) : base(game)
        {
            TextureHeight = textureHeight;
            ScreenHeight = screenHeight;
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(UpKey))
            {
                var newYCoordinate = MathHelper.Clamp(Position.Y - Speed, 0, ScreenHeight - TextureHeight);
                Position += new Vector2(0, newYCoordinate);
            }
            else if (Keyboard.GetState().IsKeyDown(DownKey))
            {
                var newYCoordinate = MathHelper.Clamp(Position.Y + Speed, 0, ScreenHeight - TextureHeight);
                Position += new Vector2(0, newYCoordinate);
            }
            
            base.Update(gameTime);
        }
    }
}
