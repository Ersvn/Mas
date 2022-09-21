using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Astroider
{
    internal class spaceCraft
    {
        public Texture2D SpaceCraftTexture;
        public Vector2 Position;
        public Vector2 Speed;
        public float Scale;

        public spaceCraft(Texture2D SpaceCraftTexture, Vector2 Position, Vector2 Speed, float scale)
        {
            this.SpaceCraftTexture = SpaceCraftTexture;
            this.Position = Position;
            this.Speed = Speed;
            this.Scale = scale;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(SpaceCraftTexture, Position, null, Color.White, 0f,
                new Vector2(SpaceCraftTexture.Width / 2, SpaceCraftTexture.Height / 2), 
                Scale, SpriteEffects.None, 0f);

        }

    }
}
