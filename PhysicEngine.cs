using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_sdl
{
    struct Coordinate
    {
        public float x;
        public float y;

        public Coordinate(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }

    class PhysicEngine
    {
        public Coordinate[] Step(Coordinate[] coo, float dt)
        {
            Coordinate x_i = coo[0];
            Coordinate v_i = coo[1];
            Coordinate a_i = coo[2];

            Coordinate x_f, v_f, a_f;

            a_f = a_i;

            v_f.x = v_i.x + a_i.x * dt;
            v_f.y = v_i.y + a_i.y * dt;

            x_f.x = x_i.x + v_i.x * dt;
            x_f.y = x_i.y + v_i.y * dt;

            return new Coordinate[] { x_f, v_f, a_f };
        }
    }
}
