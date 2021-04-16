using System;
using csharp_sdl;
using SDL2;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) < 0)
            {
                Console.WriteLine("Unable to initiale SDL. Error: {0}", SDL.SDL_GetError());
                return;
            }

            var window = IntPtr.Zero;
            window = SDL.SDL_CreateWindow("Pippo maniack",
                SDL.SDL_WINDOWPOS_CENTERED,
                SDL.SDL_WINDOWPOS_CENTERED,
                1020,
                800,
                SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE);

            if (window == IntPtr.Zero)
            {
                Console.Write("Unable to create a window. Error = {0}", SDL.SDL_GetError());
                return;
            }

            var renderer = IntPtr.Zero;
            renderer = SDL.SDL_CreateRenderer(window, -1, 0);
            if (renderer != IntPtr.Zero)
            {
                //SDL.SDL_SetRenderDrawColor(renderer, 255, 255, 255, 255);
                Console.Write("Renderer created!");
            }

            //SDL.SDL_Delay(5000);

            PhysicEngine engine = new PhysicEngine();

            SDL.SDL_Rect rect;
            rect.h = 50;
            rect.w = 50;
            rect.x = 500;
            rect.y = 300;

            Coordinate x = new Coordinate(500.0f, 300.0f);
            Coordinate v = new Coordinate(0.0f, 0.0f);
            Coordinate a = new Coordinate(0.0f, 0.0f);
            Coordinate[] tens = new Coordinate[] { x, v, a };

            float dt = 0.01f;
            float f = 20.0f;
            double ticks;

            SDL.SDL_Event e;
            bool quit = false;
            while (!quit)
            {
                tens[2].x = 0.0f;
                tens[2].y = 0.0f;

                // events --------------------------------------------------------
                if (SDL.SDL_PollEvent(out e) != 0)
                {
                    Console.WriteLine(e.type);

                    switch (e.type)
                    {
                        case SDL.SDL_EventType.SDL_QUIT:
                            quit = true;
                            break;
                        case SDL.SDL_EventType.SDL_KEYDOWN:
                            switch (e.key.keysym.sym)
                            {
                                case SDL.SDL_Keycode.SDLK_q:
                                    quit = true;
                                    break;
                                case SDL.SDL_Keycode.SDLK_w:
                                    tens[2].y = -f;
                                    break;
                                case SDL.SDL_Keycode.SDLK_a:
                                    tens[2].x = -f;
                                    break;
                                case SDL.SDL_Keycode.SDLK_s:
                                    tens[2].y = f;
                                    break;
                                case SDL.SDL_Keycode.SDLK_d:
                                    tens[2].x = f;
                                    break;
                            }
                            break;
                    }
                }

                // update --------------------------------------------------------
                ticks = SDL.SDL_GetTicks();
                Console.WriteLine(ticks);

                tens = engine.Step(tens, dt);

                rect.x = (int)tens[0].x;
                rect.y = (int)tens[0].y;

                int line_vx1 = rect.x;
                int line_vy1 = rect.y;
                int line_vx2 = line_vx1 + (int)tens[1].x;
                int line_vy2 = line_vy1 + (int)tens[1].y;

                // borders
                if (tens[0].x >= 1020 || tens[0].x <= 0) tens[1].x = -tens[1].x;
                if (tens[0].y >= 800 || tens[0].y <= 0) tens[1].y = -tens[1].y;


                // render --------------------------------------------------------
                SDL.SDL_SetRenderDrawColor(renderer, 255, 200, 0, 255); // orange
                SDL.SDL_RenderClear(renderer);

                SDL.SDL_SetRenderDrawColor(renderer, 0, 150, 255, 255);   // azzurro
                SDL.SDL_RenderFillRect(renderer, ref rect);

                SDL.SDL_SetRenderDrawColor(renderer, 255, 0, 0, 255);   // rosso
                SDL.SDL_RenderDrawLine(renderer, line_vx1, line_vy1, line_vx2, line_vy2);

                SDL.SDL_RenderPresent(renderer);

                //SDL.SDL_Delay(50);
            }

            SDL.SDL_DestroyWindow(window);
            SDL.SDL_DestroyRenderer(renderer);

            SDL.SDL_Quit();
        }
    }
}
