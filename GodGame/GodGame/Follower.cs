using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GodGame
{
    class Follower : GameObject
    {
        private enum State
        {
            Idle,
            Wandering,
            Dead
        }

        private const int CORPSE_TO_SKELETON_TIME_MS = 1800000;

        private State _state;
        private float _speedPerMs = 0.013F;
        private Vector2 _direction;
        private bool _directionSet = false;
        private int _elapsedMiliseconds = 0;
        private bool _buttonPressed = false;
        private int _timeAsCorpse = 0;

        public Follower(ContentManager contentManager, int x, int y, Game1 parent): base (contentManager, x, y, parent, "Follower")
        {
            _state = State.Wandering;
        }

        public override void Update(GameTime gameTime, MouseState mouse)
        {
            int i = Game1.random.Next(100);

            // smight
            if (_drawRectangle.Contains(mouse.X, mouse.Y) && _state != State.Dead)
            {
                _state = State.Idle;
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    _buttonPressed = true;
                }
                else
                {
                    if (_buttonPressed)
                    {
                        // hit
                        _state = State.Dead;
                        _parent.Smight(_x, _y);
                    }

                    _buttonPressed = false;

                }
            }
            else
            {
                if (mouse.LeftButton == ButtonState.Released)
                {
                    _buttonPressed = false;
                }
            }

            switch (_state)
            {
                case State.Wandering:
                    if (i == 0)
                    {
                        _state = State.Idle;
                    }
                    if (!_directionSet)
                    {
                        _direction = new Vector2(Game1.random.Next(-50, 50), Game1.random.Next(-50, 50));
                        _direction.Normalize();
                        _directionSet = true;
                    }
                    _elapsedMiliseconds += gameTime.ElapsedGameTime.Milliseconds;
                    int deltaX = (int)(_direction.X * _speedPerMs * _elapsedMiliseconds);
                    int deltaY = (int)(_direction.Y * _speedPerMs * _elapsedMiliseconds);
                    if (deltaX != 0 || deltaY != 0 || (_direction.X == 0 && _direction.Y == 0))
                    {
                        _elapsedMiliseconds = 0;
                    }
                    _x = _x + deltaX;
                    if (_x < 0 || _x > Game1.WINDOW_WIDTH)
                    {
                        _direction.X *= -1;
                    }
                    _y = _y + deltaY;
                    if (_y < 0 || _y > Game1.WINDOW_HEIGHT)
                    {
                        _direction.Y *= -1;
                    }
                    break;
                case State.Idle:
                    _directionSet = false;
                    if (i < 10)
                    {
                        _state = State.Wandering;
                    }
                    break;
                case State.Dead:
                    _timeAsCorpse += gameTime.ElapsedGameTime.Milliseconds;
                    if (_timeAsCorpse >= CORPSE_TO_SKELETON_TIME_MS)
                    {
                        _parent.RemoveObject(this);
                        _parent.AddObject(new Skeleton(_parent.Content, _x, _y, _parent));
                    }
                    break;
            }

            _drawRectangle.X = _x - _texture.Width / 2;
            _drawRectangle.Y = _y - _texture.Height / 2;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_state == State.Dead)
            {
                spriteBatch.Draw(_texture, _drawRectangle, Color.Gray);
            }
            else
            {
                spriteBatch.Draw(_texture, _drawRectangle, Color.White);
            }
        }
    }
}
