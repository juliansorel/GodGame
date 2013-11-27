using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace GodGame
{
    public abstract class GameObject
    {
        public abstract void Update(GameTime gameTime, MouseState mouse);
        public abstract void Draw(SpriteBatch spriteBatch); 
    }
}
