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
    public class Gameover
    {
        public Texture2D texture, gameOver;
        public Vector2 bgPos1, bgPos2;
        public int speed;
        public Vector2 position;

        // Constructor
        public Gameover()
        {
            gameOver = null;
            position = new Vector2(-241, -20);
            
            speed = 4;

        }

        // Load Content
        public void LoadContent(ContentManager Content)
        {
            
            texture = Content.Load<Texture2D>("gameoverThis");
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
           
            spriteBatch.Draw(texture, position, Color.White);
            

        }

        // Update

        public void Update(GameTime gameTime)
        {
            //speed for background scroll
            bgPos1.Y = bgPos1.Y + speed;
            bgPos2.Y = bgPos2.Y + speed;
            //Scrolling background
            if (bgPos1.Y >= 600)
            {
                bgPos1.Y = -1;
                bgPos2.Y = -2;
            }
        }

    }
}