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
using King_of_Thieves.Graphics;
using King_of_Thieves.Actors;

namespace King_of_Thieves
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        CSprite testSprite = null;
        CActorTest actorTest;
        CComponent compTest = new CComponent();
        Actors.Menu.CMenu testMenu;
        CComponent menuComo = new CComponent();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 240;
            graphics.PreferredBackBufferWidth = 320;
            graphics.SynchronizeWithVerticalRetrace = true;
            graphics.ApplyChanges();
            Graphics.CGraphics.acquireGraphics(ref graphics);
            Content.RootDirectory = "Content";
            this.IsFixedTimeStep = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content. Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            CTextures.init(Content);
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
            Graphics.CGraphics.spriteBatch = spriteBatch;

            // TODO: use this.Content to load your game content here
           // Graphics.CTextureDict.init(Content);
            testSprite = new CSprite(new CTextureAtlas(Content.Load<Texture2D>("test"), 19, 23, 0));
            

            actorTest = new CActorTest("Test");
            compTest.root = actorTest;

            

            CMasterControl.audioPlayer.soundBank.Add("04_-_Phantom_Ganon", new Sound.CSound(Content.Load<Song>("04_-_Phantom_Ganon"), false, 0));
            CMasterControl.audioPlayer.soundBank.Add("cursor", new Sound.CSound(Content.Load<SoundEffect>("cursor")));


            testMenu = new Actors.Menu.CMenu("MenuTest", new CSprite(CTextures.texture("menu")), 120, CMasterControl.audioPlayer.soundBank["04_-_Phantom_Ganon"], CMasterControl.audioPlayer.soundBank["cursor"]);
            menuComo.root = testMenu;

            //We really, really need an object database to just do this shit automagically.
            CMasterControl.mapList.Add("TestMap",new Map.MTestMap("TestMap", Map.MAPTYPES.ROOT));
            CMasterControl.mapList.Add("TestMapPart1", new Map.MTestMap("TestMapPart1", Map.MAPTYPES.CHUNK));
            //CMasterControl.audioPlayer.song = new Sound.CSound(Content.Load<Song>("04_-_Phantom_Ganon"), false, 0);
            //CMasterControl.audioPlayer.addSfx(new Sound.CSound(Content.Load<Song>("04_-_Phantom_Ganon"), false, 0));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            CMasterControl.audioPlayer.stop();
            Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            CMasterControl.gameTime = gameTime;



            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            Input.CInput.update();

            

            //compTest.root.position = new Vector2(Input.CInput.mouseX, Input.CInput.mouseY);
            compTest.updateActors(gameTime);
            menuComo.updateActors(gameTime);

            if (Input.CInput.getInputRelease(Microsoft.Xna.Framework.Input.Keys.Enter))
                CMasterControl.audioPlayer.addSfx(new Sound.CSound(Content.Load<SoundEffect>("lttp_heart")));

            //CMasterControl.audioPlayer.Update();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
            spriteBatch.Begin();
            compTest.drawActors();
            menuComo.drawActors();
            spriteBatch.End();
        }
    }
}
