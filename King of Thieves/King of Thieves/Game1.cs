using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using King_of_Thieves.Graphics;
using King_of_Thieves.Actors;
using System.Collections.Generic;

using Gears.Cloud;
using King_of_Thieves.usr.local;
using King_of_Thieves.Input;
using System.Linq;
using Gears.Cloud.Utility;

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
        Actors.Player.CPlayer player;
        CComponent compTest = new CComponent();
        Actors.Menu.CMenu testMenu;
        CComponent menuComo = new CComponent();
        HighPerfTimer _benchmarker = new HighPerfTimer();

        //Screen Resolution defaults
        private const int ScreenWidth = 320;
        private const int ScreenHeight = 240;

        public Game1()
        {
            this.IsFixedTimeStep = false;
            
            graphics = new GraphicsDeviceManager(this);
            graphics.SynchronizeWithVerticalRetrace = false;
            GraphicsAdapter.UseReferenceDevice = false;
            //graphics.PreferredBackBufferHeight = ScreenHeight
            //graphics.PreferredBackBufferWidth = ScreenWidth;
            //graphics.SynchronizeWithVerticalRetrace = true;
            //graphics.ApplyChanges();
            
            Content.RootDirectory = @"Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content. Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //Register our ContentManager
            //ContentButler.setGame(this);
            Master.Initialize(this);

            //Setup screen display/graphics device
            ViewportHandler.SetScreen(ScreenWidth, ScreenHeight);
            graphics.PreferredBackBufferWidth = ViewportHandler.GetWidth();
            graphics.PreferredBackBufferHeight = ViewportHandler.GetHeight();
            graphics.SynchronizeWithVerticalRetrace = true;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Graphics.CGraphics.acquireGraphics(ref graphics);

            

            Master.SetClearColor(Color.CornflowerBlue);

            CTextures.init(Content);

            Master.Push(new DevMenu());
            //Master.Push(new PlayableState());

            Master.GetInputManager().AddInputHandler(new CInput());
            CMasterControl.glblInput = Master.GetInputManager().GetCurrentInputHandler() as Input.CInput;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            CMasterControl.glblContent = Content;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            Graphics.CGraphics.spriteBatch = spriteBatch;
            CMasterControl.audioPlayer = new Sound.CAudioPlayer();

            // TODO: use this.Content to load your game content here
           // Graphics.CTextureDict.init(Content);
            //testSprite = new CSprite("test",new CTextureAtlas(Content.Load<Texture2D>("test"), "test", 19, 23, 0));
            
            //player = new Actors.Player.CPlayer();

            
            
                
            

            //compTest.root = player;
            //compTest.actors.Add("sword", new Actors.Items.Swords.CSword("sword", new Vector2(player.position.X - 13, player.position.Y - 13)));

            //CMasterControl.audioPlayer.soundBank.Add("04_-_Phantom_Ganon", new Sound.CSound(Content.Load<Song>("04_-_Phantom_Ganon"), false, 0));
            CMasterControl.audioPlayer.soundBank.Add("cursor", new Sound.CSound(Content.Load<SoundEffect>("cursor"),true));
            CMasterControl.audioPlayer.soundBank.Add("lttp_heart", new Sound.CSound(Content.Load<SoundEffect>("lttp_heart")));

            //testMenu = new Actors.Menu.CMenu("MenuTest", new CSprite(CTextures.texture("menu")), 120, CMasterControl.audioPlayer.soundBank["04_-_Phantom_Ganon"], CMasterControl.audioPlayer.soundBank["cursor"]);
            menuComo.root = testMenu;
            

            //when a component has been created, it must be added to the Comm Net.
            //CMasterControl.commNet.Add(0, new List<CActorPacket>());
            //CMasterControl.commNet.Add(1, new List<CActorPacket>());

            //CMasterControl.mapList.Add("TestMap",new Map.MTestMap("TestMap", Map.MAPTYPES.ROOT));
            CMasterControl.mapManager.cacheMaps(false, "tiletester.xml");
            CMasterControl.mapManager.swapMap("tiletester.xml");
            //CMasterControl.mapList.Add("TestMapPart1", new Map.MTestMap("TestMapPart1", Map.MAPTYPES.CHUNK));
            //Input.CMrMapIO.Save(CMasterControl.mapList["TestMap"].Map, "testmap.xml");
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
            {
                this.Exit();
            }
            //Exit when Escape is pressed(Dunno if this iterferes with the editor?)
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Escape))
                this.Exit();

            CInput input = Master.GetInputManager().GetCurrentInputHandler() as CInput;
            if (input.getInputRelease(Microsoft.Xna.Framework.Input.Keys.B))
                CActor.showHitBox = !CActor.showHitBox;
            _benchmarker.Start();
            
            Master.Update(gameTime);
            //CMasterControl.mapManager.updateMap(gameTime);
            _benchmarker.Stop();
            //CMasterControl.audioPlayer.Update();
            base.Update(gameTime);
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Master.GetClearColor());

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            //spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, RasterizerState.CullNone, null, globalTransformation);
            
            Master.Draw(spriteBatch);
            //CMasterControl.mapManager.drawMap();
            
            if (CActor.showHitBox)
            {
                double fps = (1 / _benchmarker.Duration);
                spriteBatch.DrawString(Content.Load<SpriteFont>("Fonts/benchmarker"), _benchmarker.Duration.ToString(), Vector2.Zero, Color.White);
            }

            spriteBatch.End();

            

            base.Draw(gameTime);

            System.GC.Collect();
        }
    }
}
