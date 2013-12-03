using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace GodGame
{
    class Skeleton: GameObject
    {
        public Skeleton(ContentManager contentManager, int x, int y, Game1 parent): base (contentManager, x, y, parent, "Skeleton")
        {
        }
    }
}
