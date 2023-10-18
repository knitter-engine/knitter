﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace Knitter;

public class KnitterEngine
{
    private readonly World _world;
    private bool _canRun;

    public KnitterEngine(World world) {
        //TODO: world should be load from asset
        _world = world;
    }

    public void Run()//TODO: avoid run twice
    {
        Task.Factory.StartNew(() => {
            _canRun = true;
            while (_canRun)
            {
                Time.Update();
                //TODO: input.Update();
                _world.Update();
                //TODO: render.Update();
                Thread.Sleep((int)Random.Shared.NextInt64(100, 500));
            }
        });
        using (GameMainWindow game = new GameMainWindow(800, 600, "LearnOpenTK"))
        {
            game.Run();
        }
    }

    public void Stop()
    {
        _canRun = false;
    }

}