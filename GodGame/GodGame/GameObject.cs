using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GodGame
{
    public abstract class GameObject
    {
        protected Texture2D _texture;
        protected Rectangle _drawRectangle;
        protected int _x;
        protected int _y;
        protected Game1 _parent;

        public GameObject(ContentManager contentManager, int x, int y, Game1 parent, string textureName)
        {
            _texture = contentManager.Load<Texture2D>(textureName);
            _drawRectangle = new Rectangle(x - _texture.Width / 2, y - _texture.Height / 2, _texture.Width, _texture.Height);
            _x = x;
            _y = y;
            _parent = parent;
        }

        public virtual void Update(GameTime gameTime, MouseState mouse)
        {
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _drawRectangle, Color.White);
        }
    }
}
