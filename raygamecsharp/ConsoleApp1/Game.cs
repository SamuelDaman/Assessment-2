using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using Raylib;
using static Raylib.Raylib;

namespace ConsoleApp1
{
    /// <summary>
    /// This class is where the main game takes place.
    /// </summary>
    class Game
    {
        //These are used for timing and framerate.
        Stopwatch stopwatch = new Stopwatch();

        private long currentTime = 0;
        private long lastTime = 0;
        private float timer = 0;
        private int fps = 1;
        private int frames;

        private float deltaTime = 0.005f;

        //This is the tank and the objects used to create it.
        SceneObject tankObject = new SceneObject();
        SceneObject turretObject = new SceneObject();
        List<SceneObject> bulletObject = new List<SceneObject>();
        public static List<SceneObject> tankHitBoxPoint = new List<SceneObject>(4);

        SpriteObject tankSprite = new SpriteObject();
        SpriteObject turretSprite = new SpriteObject();

        //These are used to create a wall that stops bullets and turns red if the tank collides with it.
        public static List<Vector2> wall = new List<Vector2>()
        {
            new Vector2(1200, 100),
            new Vector2(1250, 500)
        };
        AABB2 wallHitBox = new AABB2(wall[0], wall[1]);

        //These are changing colors used for feedback on whether the tank and wall are colliding or not.
        Color tankBoxColor = Color.GREEN;
        Color wallBoxColor = Color.GREEN;

        /// <summary>
        /// This intializes the game.
        /// </summary>
        public void Init()
        {
            stopwatch.Start();
            lastTime = stopwatch.ElapsedMilliseconds;

            //Here is where the tank sprite is initialized.
            tankSprite.Load("tankBlue_outline.png");
            tankSprite.SetRotate(-90 * (float)(Math.PI / 180f));
            tankSprite.SetPosition(-tankSprite.Width / 2f, tankSprite.Height / 2f);

            //Here is where the barrel sprite is initialized.
            turretSprite.Load("barrelBlue.png");
            turretSprite.SetRotate(-90 * (float)(Math.PI / 180f));
            turretSprite.SetPosition(0, turretSprite.Width / 2f);

            //This is where the points used to fit the tank's AABB are set up.
            for (int i = 0; i < tankHitBoxPoint.Capacity; i++)
            {
                tankHitBoxPoint.Add(new SceneObject());
            }
            tankHitBoxPoint[0].SetPosition(-tankSprite.Width / 2f, (-tankSprite.Height / 2f) - 4);
            tankHitBoxPoint[1].SetPosition((tankSprite.Width / 2f) - 4, (-tankSprite.Height / 2f) - 4);
            tankHitBoxPoint[2].SetPosition((tankSprite.Width / 2f) - 4, tankSprite.Height / 2f);
            tankHitBoxPoint[3].SetPosition(-tankSprite.Width / 2f, tankSprite.Height / 2f);

            //Here is where the individual parts of the tank are put together by adding them to the main object as children.
            turretObject.AddChild(turretSprite);
            tankObject.AddChild(tankSprite);
            tankObject.AddChild(turretObject);
            for (int i = 0; i < tankHitBoxPoint.Count; i++)
            {
                tankObject.AddChild(tankHitBoxPoint[i]);
            }

            tankObject.SetPosition(GetScreenWidth() / 2f, GetScreenHeight() / 2f);
        }
        public void Shutdown()
        {

        }
        /// <summary>
        /// This is used to update the information of the objects in the game.
        /// </summary>
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

            //These statements are used to rotate the tank.
            if (IsKeyDown(KeyboardKey.KEY_A))
            {
                tankObject.Rotate(-deltaTime * 5);
            }
            if (IsKeyDown(KeyboardKey.KEY_D))
            {
                tankObject.Rotate(deltaTime * 5);
            }
            //These are used to move the tank forward and back.
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
            //These are used to rotate the barrel on top of the tank.
            if (IsKeyDown(KeyboardKey.KEY_Q))
            {
                turretObject.Rotate(-deltaTime * 10);
            }
            if (IsKeyDown(KeyboardKey.KEY_E))
            {
                turretObject.Rotate(deltaTime * 10);
            }

            //These check the player is too off screen and, if so, moves them to the other side of the screen.
            if (tankObject.GlobalTransform.m7 < -100)
            {
                tankObject.globalTransform.m7 = 1600;
            }
            if (tankObject.GlobalTransform.m7 > 1600)
            {
                tankObject.globalTransform.m7 = -100;
            }
            if (tankObject.GlobalTransform.m8 < -100)
            {
                tankObject.globalTransform.m8 = 1000;
            }
            if (tankObject.GlobalTransform.m8 > 1000)
            {
                tankObject.globalTransform.m8 = -100;
            }

            //This calls the shoot function.
            if (IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                Shoot();
            }

            BulletManagement();

            tankObject.Update(deltaTime);

            lastTime = currentTime;
        }
        /// <summary>
        /// This draws all the objects
        /// </summary>
        public void Draw()
        {
            BeginDrawing();

            ClearBackground(Color.WHITE);
            DrawText(fps.ToString(), 10, 10, 20, Color.RED);

            //This draws the tank.
            tankObject.Draw();

            //This draws a box around the tank to show where the corners of the tank are.
            DrawLine((int)tankHitBoxPoint[0].GlobalTransform.m7, (int)tankHitBoxPoint[0].GlobalTransform.m8, (int)tankHitBoxPoint[1].GlobalTransform.m7, (int)tankHitBoxPoint[1].GlobalTransform.m8, Color.PURPLE);
            DrawLine((int)tankHitBoxPoint[1].GlobalTransform.m7, (int)tankHitBoxPoint[1].GlobalTransform.m8, (int)tankHitBoxPoint[2].GlobalTransform.m7, (int)tankHitBoxPoint[2].GlobalTransform.m8, Color.PURPLE);
            DrawLine((int)tankHitBoxPoint[2].GlobalTransform.m7, (int)tankHitBoxPoint[2].GlobalTransform.m8, (int)tankHitBoxPoint[3].GlobalTransform.m7, (int)tankHitBoxPoint[3].GlobalTransform.m8, Color.PURPLE);
            DrawLine((int)tankHitBoxPoint[3].GlobalTransform.m7, (int)tankHitBoxPoint[3].GlobalTransform.m8, (int)tankHitBoxPoint[0].GlobalTransform.m7, (int)tankHitBoxPoint[0].GlobalTransform.m8, Color.PURPLE);

            //This converts the tankHitBoxPoint SceneObjects to Vector2's so they can be used in the Fit function.
            List<Vector2> points = new List<Vector2>()
            {
                new Vector2(tankHitBoxPoint[0].GlobalTransform.m7, tankHitBoxPoint[0].GlobalTransform.m8),
                new Vector2(tankHitBoxPoint[1].GlobalTransform.m7, tankHitBoxPoint[1].GlobalTransform.m8),
                new Vector2(tankHitBoxPoint[2].GlobalTransform.m7, tankHitBoxPoint[2].GlobalTransform.m8),
                new Vector2(tankHitBoxPoint[3].GlobalTransform.m7, tankHitBoxPoint[3].GlobalTransform.m8)
            };
            //This Fits the bounding box around the given points.
            tankObject.BoundingBox(points);
            //And this draws the bounding box.
            DrawLine((int)tankObject.boundingBox.min.x, (int)tankObject.boundingBox.min.y, (int)tankObject.boundingBox.max.x, (int)tankObject.boundingBox.min.y, tankBoxColor);
            DrawLine((int)tankObject.boundingBox.max.x, (int)tankObject.boundingBox.min.y, (int)tankObject.boundingBox.max.x, (int)tankObject.boundingBox.max.y, tankBoxColor);
            DrawLine((int)tankObject.boundingBox.max.x, (int)tankObject.boundingBox.max.y, (int)tankObject.boundingBox.min.x, (int)tankObject.boundingBox.max.y, tankBoxColor);
            DrawLine((int)tankObject.boundingBox.min.x, (int)tankObject.boundingBox.max.y, (int)tankObject.boundingBox.min.x, (int)tankObject.boundingBox.min.y, tankBoxColor);
            
            //This draws the wall.
            DrawRectangle((int)wall[0].x, (int)wall[0].y, (int)(wall[1].x - wall[0].x), (int)(wall[1].y - wall[0].y), Color.BROWN);
            //This draws the AABB around the wall.
            DrawRectangleLines((int)wall[0].x, (int)wall[0].y, (int)(wall[1].x - wall[0].x), (int)(wall[1].y - wall[0].y), wallBoxColor);

            EndDrawing();
        }
        /// <summary>
        /// This function initialiazes the bullets so they can be fired.
        /// </summary>
        public void Shoot()
        {
            SceneObject bullet = new SceneObject();
            SpriteObject sprite = new SpriteObject();
            List<SceneObject> HitBoxPoints = new List<SceneObject>(4);

            //This is where the bullets' sprites are initialized.
            sprite.Load("barrelBlue.png");
            sprite.SetRotate(-90 * (float)(Math.PI / 180f));
            sprite.SetPosition(0, sprite.Width / 2f);

            //This is where the bullets' corners are listed for the purpose of creating an AABB.
            for (int i = 0; i < HitBoxPoints.Capacity; i++)
            {
                HitBoxPoints.Add(new SceneObject());
            }
            HitBoxPoints[0].SetPosition(0, -sprite.Width / 2f);
            HitBoxPoints[1].SetPosition(sprite.Height, -sprite.Width / 2f);
            HitBoxPoints[2].SetPosition(sprite.Height, sprite.Width / 2f);
            HitBoxPoints[3].SetPosition(0, sprite.Width / 2f);

            //This is where the bullets are put togather.
            bullet.AddChild(sprite);
            bullet.SetRotate(turretObject.GlobalTransform.GetRotateZ());
            bullet.SetPosition(turretObject.GlobalTransform.m7, turretObject.GlobalTransform.m8);
            for (int i = 0; i < HitBoxPoints.Count; i++)
            {
                bullet.AddChild(HitBoxPoints[i]);
            }
            bulletObject.Add(bullet);
        }

        /// <summary>
        /// This manages non-collision bullet interactions.
        /// </summary>
        public void BulletManagement()
        {
            foreach (SceneObject bullet in bulletObject)
            {
                //This finds where bullet is facing and moves it in that direction.
                Vector3 facing = new Vector3(bullet.GlobalTransform.m1, bullet.GlobalTransform.m2, 1) * deltaTime * 1000;
                bullet.Translate(facing.x, facing.y);
                
                //This makes the bullets wrap around the screen.
                if (bullet.GlobalTransform.m7 < -100)
                {
                    bullet.globalTransform.m7 = 1600;
                }
                if (bullet.GlobalTransform.m7 > 1600)
                {
                    bullet.globalTransform.m7 = -100;
                }
                if (bullet.GlobalTransform.m8 < -100)
                {
                    bullet.globalTransform.m8 = 1000;
                }
                if (bullet.GlobalTransform.m8 > 1000)
                {
                    bullet.globalTransform.m8 = -100;
                }

                //This is where the AABBs for each bullet is drawn.
                List<Vector2> bulletPoints = new List<Vector2>(4)
                {
                    new Vector2(bullet.GetChild(1).GlobalTransform.m7, bullet.GetChild(1).GlobalTransform.m8),
                    new Vector2(bullet.GetChild(2).GlobalTransform.m7, bullet.GetChild(2).GlobalTransform.m8),
                    new Vector2(bullet.GetChild(3).GlobalTransform.m7, bullet.GetChild(3).GlobalTransform.m8),
                    new Vector2(bullet.GetChild(4).GlobalTransform.m7, bullet.GetChild(4).GlobalTransform.m8),
                };
                DrawLine((int)bulletPoints[0].x, (int)bulletPoints[0].y, (int)bulletPoints[1].x, (int)bulletPoints[1].y, Color.PURPLE);
                DrawLine((int)bulletPoints[1].x, (int)bulletPoints[1].y, (int)bulletPoints[2].x, (int)bulletPoints[2].y, Color.PURPLE);
                DrawLine((int)bulletPoints[2].x, (int)bulletPoints[2].y, (int)bulletPoints[3].x, (int)bulletPoints[3].y, Color.PURPLE);
                DrawLine((int)bulletPoints[3].x, (int)bulletPoints[3].y, (int)bulletPoints[0].x, (int)bulletPoints[0].y, Color.PURPLE);

                bullet.BoundingBox(bulletPoints);
                DrawLine((int)bullet.boundingBox.min.x, (int)bullet.boundingBox.min.y, (int)bullet.boundingBox.max.x, (int)bullet.boundingBox.min.y, Color.GREEN);
                DrawLine((int)bullet.boundingBox.max.x, (int)bullet.boundingBox.min.y, (int)bullet.boundingBox.max.x, (int)bullet.boundingBox.max.y, Color.GREEN);
                DrawLine((int)bullet.boundingBox.max.x, (int)bullet.boundingBox.max.y, (int)bullet.boundingBox.min.x, (int)bullet.boundingBox.max.y, Color.GREEN);
                DrawLine((int)bullet.boundingBox.min.x, (int)bullet.boundingBox.max.y, (int)bullet.boundingBox.min.x, (int)bullet.boundingBox.min.y, Color.GREEN);

                bullet.Draw();
            }
            //This delete bullets if there are too many in the game at once.
            if (bulletObject.Count > 750)
            {
                bulletObject.RemoveAt(0);
            }
        }
        /// <summary>
        /// This manages hit detection.
        /// </summary>
        public void CollisionDetection()
        {
            //This checks if the tank is colliding with the wall and turns the tank's hit box red.
            if (tankObject.boundingBox.Overlaps(wallHitBox))
            {
                tankBoxColor = Color.RED;
            }
            else
            {
                tankBoxColor = Color.GREEN;
            }

            //This checks if the wall is colliding with the tank and turns the wall's hit box red.
            if (wallHitBox.Overlaps(tankObject.boundingBox))
            {
                wallBoxColor = Color.RED;
            }
            else
            {
                wallBoxColor = Color.GREEN;
            }

            //This detects if the any bullets collide with the wall and the deletes the bullets that do.
            for (int i = 0; i < bulletObject.Count; i++)
            {
                if (bulletObject[i].boundingBox.Overlaps(wallHitBox))
                {
                    bulletObject.RemoveAt(i);
                }
            }
        }
    }
}
