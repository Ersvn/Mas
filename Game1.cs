using Astroider.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;





namespace Astroider
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        Player spacey = new Player();
        Background bg = new Background();
        Texture2D AsteroidTex;
        Texture2D spaceCraftTex;
        Gameover go = new Gameover();
        Vector2 position;
        Vector2 velocity;
        Asteroid asteroid;
        Asteroid asteroidAdd;
        List<Asteroid> asteroids;
        SpaceShip[] CraftArray;
        SpaceShip PositionCraft;
        MouseState mouseState, previousMouseState;
        private List<Sprite> _sprites;
        private SpriteFont _font;
        Texture2D crosshairNormalTex;
        Rectangle crosshairRect;
        bool isPressed;
        bool isReleased;
        bool isDead;
        int score;
        SpriteFont HeadsUpDisplay;
        Vector2 HUDpos;
        public int ScreenWidth;
        public int ScreenHeight;
        SpaceShip posship;
        Random random = new Random();
        float scale;
        int lives;
        Random RandomCraft;
        Random RandomCraftScale;
        float ScaleCraft;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            this.Window.Title = "Asteroid Game";
            graphics.IsFullScreen = false;
            
        }


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            score = 0;
            lives = 5;
            HUDpos.X = 10;
            HUDpos.Y = 45;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            IsMouseVisible = false;
            spriteBatch = new SpriteBatch(GraphicsDevice);

            
            spacey.LoadContent(Content);
            bg.LoadContent(Content);
            
            crosshairNormalTex = Content.Load<Texture2D>("crosshair");
            HeadsUpDisplay = Content.Load<SpriteFont>("Font");
            spaceCraftTex = Content.Load<Texture2D>("spaceCraft_trans");
            CraftArray = new SpaceShip[5];

            


            RandomCraftScale = new Random();
            RandomCraft = new Random();

            for (int i = 0; i < CraftArray.Length; i++)
            {
                int RandomX = RandomCraft.Next(0, Globals.ScreenWidth - spaceCraftTex.Width);
                int RandomY = RandomCraft.Next(0, Globals.ScreenHight - spaceCraftTex.Height);
                ScaleCraft = RandomCraftScale.Next(1, 3);

                Vector2 PosCraft = new Vector2(RandomX, RandomY);
                PositionCraft = new SpaceShip(spaceCraftTex, PosCraft, Vector2.Zero, ScaleCraft);
                CraftArray[i] = PositionCraft;
                
            }
            AsteroidTex = Content.Load<Texture2D>("astroid");
            asteroids = new List<Asteroid>();

            for (int i = 0; i <= 8; i++)
            {
                // random position for asteroids
                position.X = random.Next(0, Globals.ScreenWidth - AsteroidTex.Width);
                position.Y = random.Next(0, Globals.ScreenHight - AsteroidTex.Height);
                position = new Vector2(position.X, position.Y);
                // random direction and speed for asteroids
                velocity.X = random.Next(-6, 6);
                velocity.Y = random.Next(-6, 6);
                velocity = new Vector2(velocity.X, velocity.Y);
                // random scale for meteor
                scale = random.Next(2, 4);

                asteroid = new Asteroid(AsteroidTex, velocity, position, scale);
                asteroids.Add(asteroid);
            }

            go.LoadContent(Content);


        }

        // Update
        protected override void Update(GameTime gameTime)
        {
            // Exit Game
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            bg.Update(gameTime);
            

            // Crosshair
            previousMouseState = mouseState;
            mouseState = Mouse.GetState();
            crosshairRect = new Rectangle(mouseState.X - crosshairNormalTex.Width / 2,
                mouseState.Y - crosshairNormalTex.Height / 2,
            crosshairNormalTex.Width, crosshairNormalTex.Height);

            if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                isPressed = true;
                isReleased = true;
            }
            else
            {
                isPressed = false;
                isReleased = false;
            }
            // AsteroidList updating, check for collision
            if (lives > 0)
            {
                
                foreach (Asteroid asteroid in asteroids)
                {
                    asteroid.Update();
                    if (asteroid.boundingBox.Intersects(spacey.boundingBox))
                    {
                        asteroid.isAlive = false;
                        lives--;
                        score -= 10;
                        spacey.health -= 40;
                    }
                    // if asteroid goes beyond 0 in Y axel isAlive == false
                    // and player losses a life
                    if (asteroid.position.Y < 0 - AsteroidTex.Height || asteroid.position.X < 0 - AsteroidTex.Width && asteroid.isAlive == true)
                    {
                        asteroid.isAlive = false;
                        
                    }
                    // If asteroid is within mouseState & is pressed then remove
                    // asteroid == isDead
                    else if (asteroid.boundingBox.Contains(mouseState.X, mouseState.Y) && isPressed && isReleased)
                    {
                        isDead = asteroid.IsDead(mouseState.X, mouseState.Y);
                        if (isDead)
                        {
                            // when isDead/Clicked on, user get 10 pts
                            score += 20;
                        }

                    }
                    if (!asteroid.isAlive)
                    {
                        // when Asteroids are not "isAlive" remove from list
                        asteroids.Remove(asteroid);
                        position = new Vector2(position.X, position.Y);

                        velocity.X = random.Next(0, Globals.ScreenWidth - AsteroidTex.Width);
                        velocity.Y = random.Next(0, Globals.ScreenHight - AsteroidTex.Height);
                        velocity = new Vector2(velocity.X, velocity.Y);

                        scale = random.Next(2, 4);
                        // create new meteor after one dies
                        asteroidAdd = new Asteroid(AsteroidTex, velocity, position, scale);
                        asteroids.Add(asteroidAdd);
                        break;
                    }
                    
                }
                
                
            }

            spacey.Update(gameTime);
            if(lives == 0)
            {
                asteroids.Clear();
            }
            
            base.Update(gameTime);
        }

        

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            bg.Draw(spriteBatch);
            foreach(var spaceShip in CraftArray)
            {
                spaceShip.Draw(spriteBatch);
            }

            foreach (Asteroid asteroid in asteroids)
            {
                asteroid.Draw(spriteBatch);
            }
            
                spacey.Draw(spriteBatch);

            spriteBatch.DrawString(HeadsUpDisplay, "SCORE: " + score.ToString(),
                HUDpos, Color.White);
            if (lives <= 0)
            {
                go.Draw(spriteBatch);
                spriteBatch.DrawString(HeadsUpDisplay, "YOU GOT: " + score.ToString() + " SCORE",
                HUDpos, Color.White);
            }
            
            spriteBatch.Draw(crosshairNormalTex, crosshairRect, Color.White);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
        // Load Asteroids from list, if they go outside window remove from list
        

    } 

}