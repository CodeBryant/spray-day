using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace SprayDay
{
    public class MainGame : Game
    {
        GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		SoundEffect effect;
		ParticleEngine engine;
		List<Texture2D> tex = new List<Texture2D>();
		Random rnd;
		Color sprayColor;
		Texture2D sprayTexture;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);

			Window.Title = "";
            Content.RootDirectory = "Content";	            
			graphics.IsFullScreen = false;
			rnd = new Random ();
		}

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
			spriteBatch = new SpriteBatch(GraphicsDevice);
			tex.Add(Content.Load<Texture2D>("spray1"));
			tex.Add(Content.Load<Texture2D>("spray2"));
			effect = Content.Load<SoundEffect>("spray");
			sprayTexture = Content.Load<Texture2D> ("sprayTexture");
			engine = new ParticleEngine(tex, effect, sprayTexture);
        }

        protected override void Update(GameTime gameTime)
        {
			engine.Location = new Vector2(Mouse.GetState().X,Mouse.GetState().Y);
			if (Keyboard.GetState ().IsKeyDown (Keys.LeftShift)) {
				sprayColor = new Color((byte)200, (byte)200, (byte)100, (byte)rnd.Next(0, 10));
			} else {
				sprayColor = new Color((byte)100, (byte)24, (byte)222, (byte)rnd.Next(0, 10));
			}
			engine.Update (sprayColor);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
           	graphics.GraphicsDevice.Clear(Color.Black);
			engine.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}