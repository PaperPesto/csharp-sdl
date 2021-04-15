using System;
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

            SDL.SDL_Rect rect;
            rect.h = 50;
            rect.w = 50;
            rect.x = 500;
            rect.y = 300;

            float ax = 0.0f;
            float ay = 0.0f;

            float vx = 0.0f;
            float vy = 0.0f;

            float x = 500.0f;
            float y = 300.0f;

            float dt = 0.01f;
            float f = 0.1f;
            double ticks;

            SDL.SDL_Event e;
            bool quit = false;
            while (!quit)
            {
                // events
                while (SDL.SDL_PollEvent(out e) != 0)
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
                                    ay = -f;
                                    break;
                                case SDL.SDL_Keycode.SDLK_a:
                                    ax = -f;
                                    break;
                                case SDL.SDL_Keycode.SDLK_s:
                                    ay = f;
                                    break;
                                case SDL.SDL_Keycode.SDLK_d:
                                    ax = f;
                                    break;
                            }
                            break;
                    }
                }

                // update
                ticks = SDL.SDL_GetTicks();
                Console.WriteLine(ticks);
                vx = vx + ax * dt;
                vy = vy + ay * dt;
                x = x + vx * dt;
                y = y + vy * dt;

                rect.x = (int)x;
                rect.y = (int)y;

                // borders
                if (rect.x >= 1020 || rect.x <= 0) vx = -vx;
                if (rect.y >= 800 || rect.y <= 0) vy = -vy;

                // render
                SDL.SDL_SetRenderDrawColor(renderer, 255, 120, 0, 255); // orange
                SDL.SDL_RenderClear(renderer);

                SDL.SDL_SetRenderDrawColor(renderer, 0, 150, 255, 255);   // azzurro
                SDL.SDL_RenderFillRect(renderer, ref rect);
                SDL.SDL_RenderPresent(renderer);

                //SDL.SDL_Delay(50);
            }

            SDL.SDL_DestroyWindow(window);
            SDL.SDL_DestroyRenderer(renderer);

            SDL.SDL_Quit();
        }
    }
}
