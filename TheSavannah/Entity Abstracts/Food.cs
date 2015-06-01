using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TheSavannah.Entity_Abstracts
{
    interface Food
    {
        int Consumed(int t);
        Vector2 GetPosition();
    }
}
