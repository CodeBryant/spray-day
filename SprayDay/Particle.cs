using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

namespace SprayDay
{
	public class Particle
	{
		Texture2D Texture;
		Vector2 Position;
		Vector2 Velocity;
		float AngularVelocity;
		public float LifeTime; float Angle;
		public Color Color;
		float Scale;
		public bool isAlive = true;

		public Particle(Texture2D tex, Vector2 pos, Vector2 vel, float omega, float lifeTime, float sc, Color color)
		{
			Texture = tex;
			Position = pos;
			Velocity = vel;
			AngularVelocity = omega;
			Angle = 0;
			LifeTime = lifeTime;
			Color = color;
			Scale = sc;
		}

		public void Update()
		{
			LifeTime--;
			Position += Velocity;
			Angle += AngularVelocity;
			if (LifeTime <= 0)
			{
				isAlive = false;
			}
		}

		public void Draw(SpriteBatch batch)
		{
			Rectangle sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
			Vector2 origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
			batch.Draw(Texture, Position, sourceRectangle, Color,
			           Angle, origin, Scale, SpriteEffects.None, 0f);
		}
	}
}