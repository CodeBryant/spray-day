using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace SprayDay
{
	public class ParticleEngine
	{
		Random rnd;
		List<Texture2D> Textures;
		List<Particle> Particles;
		public Vector2 Location;
		SoundEffect effect;
		SoundEffectInstance instance;
		private List<Vector2> coords = new List<Vector2>();
		Texture2D sprayTexture;
		Color color;

		public ParticleEngine(List<Texture2D> textures, SoundEffect ef, Texture2D st)
		{
			Particles = new List<Particle>();
			Textures = textures;
			rnd = new Random();
			Location = Vector2.Zero;
			effect = ef;
			instance = effect.CreateInstance ();
			instance.IsLooped = true;
			sprayTexture = st;
		}

		private Particle GenerateNewParticle(Color color1)
		{
			Texture2D texture = Textures[rnd.Next(Textures.Count)];
			Vector2 position = Location;
			double angle = 3 * rnd.Next();
			Vector2 velocity = new Vector2((float)Math.Cos(angle) + (float)rnd.Next(-2, 2), (float)Math.Sin(angle) + (float)rnd.Next(-3, 3));
			float omega = 0.025f;
			float lt = 15 + rnd.Next(20);
			float size = 0.7f * (float)rnd.NextDouble();
			color = color1;
			return new Particle(texture, position, velocity, omega, lt, size, color);
		}


		public void Update(Color color)
		{
			if (Mouse.GetState ().LeftButton == ButtonState.Pressed) {
				coords.Add (new Vector2 (Mouse.GetState ().X, Mouse.GetState ().Y));

				int total = 25;
				for (int i = 0; i < total; i++) {
					Particles.Add (GenerateNewParticle (color));
				}
				if (instance.State == SoundState.Stopped) { instance.Play (); }
			} else {
				instance.Stop ();
			}

			for (int i = 0; i < Particles.Count; i++)
			{
				Particles[i].Update();

				if (!Particles[i].isAlive)
				{
					Particles.RemoveAt(i);
					i--;
				}
			}
		}

		public void Draw(SpriteBatch batch)
		{
			batch.Begin(SpriteSortMode.BackToFront, BlendState.Additive);
			Primitives2D.LoadPixel (sprayTexture);
			for (int i = 2; i < coords.Count; i++) {
				//color.A = (byte)255;
				//batch.PutPixel ((float)coords[i].X - sprayTexture.Width / 2, (float)coords[i].Y - sprayTexture.Height / 2, sprayTexture, color);
				//batch.DrawLine(coords [i-1], coords [i], color);
			}
			for (int index = 0; index < Particles.Count; index++) {
				Particles[index].Draw(batch);
			}
			batch.End();
		}
	}
}