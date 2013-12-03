using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GodGame
{
    class Lightning : GameObject
    {
        private const int WIDTH = 30;
        private const int HEIGHT = 50;
        private const int FRAME_TIME = 50;

        private Rectangle _sourceRectange;

        private int _animationTime = 0;
        private int _frame = 0;

        public Lightning(ContentManager content, int x, int y, Game1 parent) : base (content, x, y, parent, "SmiteRed")
        {
            _sourceRectange = new Rectangle(0, 0, WIDTH, HEIGHT);
            _drawRectangle = new Rectangle(x - WIDTH / 2, y - HEIGHT, WIDTH, HEIGHT);
        }

        public override void Update(GameTime gameTime, MouseState mouse)
        {
            _animationTime += gameTime.ElapsedGameTime.Milliseconds;
            if (_animationTime > FRAME_TIME)
            {
                _animationTime = 0;
                _frame++;
                if (_frame > 2)
                {
                    _parent.RemoveObject(this);
                    return;
                }
                _sourceRectange.X = WIDTH * _frame;
            }

        }
    }
}
