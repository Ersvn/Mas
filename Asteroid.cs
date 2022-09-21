using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Astroider
{
    internal class Asteroid
    {
        
        public Rectangle boundingBox;
        public Texture2D texture;
        public Vector2 position;
        public Vector2 origin;
        public float rotationAngle;
        public Vector2 velocity;
        public float scale;
        
        public bool isAlive;
        Random random = new Random();
        public float randX, randY;
        



        public Asteroid(Texture2D newTexture, Vector2 newPosition, Vector2 position, float scale)
        {
            this.position = newPosition; 
            this.texture = newTexture;
            this.velocity = new Vector2(random.Next(-5, 5), random.Next(-5, 5));
            this.scale = scale;
            
            isAlive = true;
            
            
            position = new Vector2(randX, randY);
        }

        
        public void Update()
        {
            //Update(GameTime gameTime) krävs för rotationer**

            // BoundingBox for collision / popping
            boundingBox = new Rectangle((int)(position.X), (int)(position.Y), texture.Width, texture.Height);
            //Astroids movement/speed!
            position += velocity;


            

            /*Rotate asteroids
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            rotationAngle += elapsed;
            float circle = MathHelper.Pi * 2;
            rotationAngle = rotationAngle % circle;*/
            AddAsteroids();

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, position, null, Color.White);
        }

        public bool IsDead(int x, int y)
        {
            // method for when meteor dies and 
            bool isDead = false;
            Rectangle deadRect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            if (deadRect.Contains(x, y))
            {
                isDead = true;
                isAlive = false;
            }
            return isDead;
        }
        public void AddAsteroids()
        {
            // defines which position meteor has when x,y is beyond boundary
            if (position.X < Globals.GameArea.Left)
            {
                position = new Vector2(Globals.GameArea.Right, position.Y);
            }
            if (position.X > Globals.GameArea.Right)
            {
                position = new Vector2(Globals.GameArea.Left, position.Y);
            }
            if (position.Y < Globals.GameArea.Top)
            {
                position = new Vector2(position.X, Globals.GameArea.Bottom);
            }
            if (position.Y > Globals.GameArea.Bottom)
            {
                position = new Vector2(position.X, Globals.GameArea.Top);
            }
        }

        
    }
}