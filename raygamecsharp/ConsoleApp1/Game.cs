using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using Raylib;
using static Raylib.Raylib;

namespace ConsoleApp1
{
    class Game
    {
        Stopwatch stopwatch = new Stopwatch();

        private long currentTime = 0;
        private long lastTime = 0;
        private float timer = 0;
        private int fps = 1;
        private int frames;

        private float deltaTime = 0.005f;

        SceneObject tankObject = new SceneObject();
        SceneObject turretObject = new SceneObject();
        SceneObject bulletObject = new SceneObject();
        SceneObject tankHitBoxPoint1 = new SceneObject();
        SceneObject tankHitBoxPoint2 = new SceneObject();
        SceneObject tankHitBoxPoint3 = new SceneObject();
        SceneObject tankHitBoxPoint4 = new SceneObject();

        SpriteObject tankSprite = new SpriteObject();
        SpriteObject turretSprite = new SpriteObject();
        SpriteObject bulletSprite = new SpriteObject();

        public void Init()
        {
            stopwatch.Start();
            lastTime = stopwatch.ElapsedMilliseconds;

            tankSprite.Load("tankBlue_outline.png");
            tankSprite.SetRotate(-90 * (float)(Math.PI / 180f));
            tankSprite.SetPosition(-tankSprite.Width / 2f, tankSprite.Height / 2f);

            turretSprite.Load("barrelBlue.png");
            turretSprite.SetRotate(-90 * (float)(Math.PI / 180f));
            turretSprite.SetPosition(0, turretSprite.Width / 2f);

            tankHitBoxPoint1.SetPosition(-tankSprite.Width / 2f, -tankSprite.Height / 2f);
            tankHitBoxPoint2.SetPosition(tankSprite.Width / 2f, -tankSprite.Height / 2f);
            tankHitBoxPoint3.SetPosition(tankSprite.Width / 2f, tankSprite.Height / 2f);
            tankHitBoxPoint4.SetPosition(-tankSprite.Width / 2f, tankSprite.Height / 2f);

            turretObject.AddChild(turretSprite);
            tankObject.AddChild(tankSprite);
            tankObject.AddChild(turretObject);
            tankObject.AddChild(tankHitBoxPoint1);
            tankObject.AddChild(tankHitBoxPoint2);
            tankObject.AddChild(tankHitBoxPoint3);
            tankObject.AddChild(tankHitBoxPoint4);

            tankObject.SetPosition(GetScreenWidth() / 2f, GetScreenHeight() / 2f);
        }
        public void Shutdown()
        {

        }
        public void Update()
        {
            currentTime = stopwatch.ElapsedMilliseconds;
            deltaTime = (currentTime - lastTime) / 1000.0f;

            timer += deltaTime;
            if (timer >= 1)
            {
                fps = frames;
                frames = 0;
                timer -= 1;
            }
            frames++;

            if (IsKeyDown(KeyboardKey.KEY_A))
            {
                tankObject.Rotate(-deltaTime * 5);
            }
            if (IsKeyDown(KeyboardKey.KEY_D))
            {
                tankObject.Rotate(deltaTime * 5);
            }
            if (IsKeyDown(KeyboardKey.KEY_W))
            {
                Vector3 facing = new Vector3(tankObject.LocalTransform.m1, tankObject.LocalTransform.m2, 1) * deltaTime * 250;
                tankObject.Translate(facing.x, facing.y);
            }
            if (IsKeyDown(KeyboardKey.KEY_S))
            {
                Vector3 facing = new Vector3(tankObject.LocalTransform.m1, tankObject.LocalTransform.m2, 1) * deltaTime * -250;
                tankObject.Translate(facing.x, facing.y);
            }
            if (IsKeyDown(KeyboardKey.KEY_Q))
            {
                turretObject.Rotate(-deltaTime * 10);
            }
            if (IsKeyDown(KeyboardKey.KEY_E))
            {
                turretObject.Rotate(deltaTime * 10);
            }

            //turretSprite.SetPosition(((float)Math.Atan2(turretSprite.globalTransform.m2, turretSprite.globalTransform.m1) * 50f), turretSprite.Width / 2f);

            tankObject.Update(deltaTime);

            lastTime = currentTime;
        }
        public void Draw()
        {
            BeginDrawing();

            ClearBackground(Color.WHITE);
            DrawText(fps.ToString(), 10, 10, 12, Color.RED);

            tankObject.Draw();

            EndDrawing();
        }
    }
}
