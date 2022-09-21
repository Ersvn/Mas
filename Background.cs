using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Astroider
{
    public class Background
    {
        public Texture2D texture, gameOver;
        public Vector2 bgPos1, bgPos2;
        public int speed;
        public Vector2 position;

        // Constructor
        public Background()
        {
            texture = null;
            bgPos1 = new Vector2(0, 0);
            bgPos2 = new Vector2(0, -600);
            speed = 4;
        }

        // Load Content
        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("Starfield");
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bgPos1, Color.White);
            spriteBatch.Draw(texture, bgPos2, Color.White);
            
        }

        // Update

        public void Update(GameTime gameTime)
        {
            //speed for background scroll
            bgPos1.Y = bgPos1.Y + speed;
            bgPos2.Y = bgPos2.Y + speed;
            //Scrolling background
            if(bgPos1.Y >= 600)
            {
                bgPos1.Y = -1;
                bgPos2.Y = -600;
            }
        }

    }
}
