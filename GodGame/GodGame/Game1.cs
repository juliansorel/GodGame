using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GodGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public const int WINDOW_HEIGHT = 600;
        public const int WINDOW_WIDTH = 800;
        const int NO_FOLLOWERS = 10;
        
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<GameObject> gameObjects = new List<GameObject>();
        List<GameObject> gameObjectsToAdd = new List<GameObject>();
        List<GameObject> gameObjectsToRemove = new List<GameObject>();
        public static Random random = new Random();


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            for (int i = 0; i < NO_FOLLOWERS; i++)
            {
                gameObjects.Add(new Follower(Content, random.Next(WINDOW_WIDTH), random.Next(WINDOW_HEIGHT), this));
            }


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            MouseState mouse = Mouse.GetState();
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Update(gameTime, mouse);
            }

            foreach (GameObject gameObject in gameObjectsToAdd)
            {
                gameObjects.Add(gameObject);
            }
            gameObjectsToAdd.Clear();

            foreach (GameObject gameObjectToRemove in gameObjectsToRemove)
            {
                gameObjects.Remove(gameObjectToRemove);
            }
            gameObjectsToRemove.Clear();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.ForestGreen);
            
            // TODO: Add your drawing code here
            spriteBatch.Begin();
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Draw(spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void Smight(int x, int y)
        {
            AddObject(new Lightning(Content, x, y, this));
        }

        public void RemoveObject(GameObject objectToRemove)
        {
            gameObjectsToRemove.Add(objectToRemove);
        }

        public void AddObject(GameObject objectToAdd)
        {
            gameObjectsToAdd.Add(objectToAdd);
        }
    }
}
