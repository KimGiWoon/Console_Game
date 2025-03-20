using System;
using System.Data;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;

// 미로찾기 게임 만들기!



namespace System
{
    class Program
    {
        // Player Position
        #region
        struct IsPosition   
        {
            public int x;
            public int y;
        }
        #endregion

        // Game Execution Step
        #region
        static void Main(string[] args)
        {
            bool gameOver = false;  // Game Over Condition
            IsPosition playerPosition;
            IsPosition goalObj;
            IsPosition monster;
            Stopwatch stopWatch = new Stopwatch();

            char[,] map;
            
            Start(out playerPosition, out goalObj, out monster,out map, stopWatch);


            while (gameOver == false)
            {

                Render(playerPosition, goalObj,ref monster,  map);
                ConsoleKey keyDown = Input();
                PlayUpdate(keyDown, ref playerPosition, goalObj,  map, ref gameOver);
            }
           
            End(stopWatch);
        }
        #endregion

        // Start Preparation Reset
        #region 
        static void Start(out IsPosition playerPosition, out IsPosition goalObj, out IsPosition monsterPos,out char[,] map, Stopwatch stopWatch)
        {
            Console.CursorVisible = false;
            // Game Start Text
            StartText();

            // Object First Position
            AllObjFirstPosition(out playerPosition, out goalObj, out monsterPos);
            
            // Play Time Start
            stopWatch.Start();

            // Map Create
            map = new char[22, 31]
            {
                     // 0    1    2    3    4    5    6    7    8    9   10   11   12   13   14   15   16   17   18   19   20   21   22   23   24   25   26   27   28   29   30
                /*0*/{ '┏', '━', '━', '━', '━', '━', '━', '━', '━', '━', '━', '━', '━', '━', '━', '━', '━', '━', '━', '━', '━', '━', '━', '━', '━', '━', '━' ,'━', '━', '━', '┓'},
                /*1*/{ '┃', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒' ,'▒', '▒', '▒', '┃'},
                /*2*/{ '┃', '▒', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '▒', ' ', ' ', ' ', ' ', ' ', ' ', '▒', ' ', '▒', ' ', '▒', ' ' ,' ', ' ', '▒', '┃'},
                /*3*/{ '┃', '▒', '▒', '▒', '▒', '▒', '▒', '▒', ' ', '▒', '▒', '▒', '▒', ' ', '▒', '▒', '▒', '▒', '▒', '▒', ' ', '▒', ' ', '▒', ' ', '▒', ' ' ,'▒', ' ', '▒', '┃'},
                /*4*/{ '┃', '▒', ' ', '▒', ' ', ' ', ' ', '▒', ' ', '▒', ' ', ' ', ' ', ' ', '▒', ' ', ' ', ' ', ' ', ' ', ' ', '▒', ' ', '▒', ' ', '▒', ' ' ,'▒', ' ', '▒', '┃'},
                /*5*/{ '┃', '▒', ' ', '▒', ' ', '▒', ' ', '▒', ' ', '▒', ' ', '▒', '▒', ' ', '▒', ' ', '▒', '▒', '▒', '▒', '▒', '▒', ' ', '▒', ' ', '▒', ' ' ,'▒', ' ', '▒', '┃'},
                /*6*/{ '┃', '▒', ' ', ' ', ' ', '▒', ' ', ' ', ' ', '▒', ' ', ' ', ' ', ' ', '▒', ' ', ' ', ' ', ' ', ' ', ' ', '▒', ' ', '▒', ' ', '▒', ' ' ,'▒', ' ', '▒', '┃'},
                /*7*/{ '┃', '▒', '▒', '▒', '▒', '▒', '▒', '▒', ' ', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', ' ', '▒', ' ', '▒', ' ', '▒', ' ', '▒', ' ' ,'▒', ' ', '▒', '┃'},
                /*8*/{ '┃', '▒', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '▒', ' ', '▒', ' ', ' ', ' ', '▒', ' ' ,'▒', ' ', '▒', '┃'},
                /*9*/{ '┃', '▒', ' ', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', ' ', '▒', '▒', '▒', '▒', ' ', '▒', ' ', '▒', ' ', '▒', ' ', ' ', ' ' ,'▒', ' ', '▒', '┃'},
               /*10*/{ '┃', '▒', ' ', '▒', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '▒', ' ', ' ', ' ', ' ', '▒', ' ', '▒', ' ', '▒', ' ', '▒', '▒' ,'▒', ' ', '▒', '┃'},
               /*11*/{ '┃', '▒', ' ', '▒', ' ', '▒', '▒', '▒', '▒', '▒', ' ', '▒', '▒', '▒', '▒', ' ', '▒', '▒', ' ', '▒', ' ', '▒', ' ', '▒', ' ', '▒', ' ' ,' ', ' ', '▒', '┃'},
               /*12*/{ '┃', '▒', ' ', ' ', ' ', ' ', ' ', '▒', ' ', ' ', ' ', ' ', ' ', ' ', '▒', ' ', '▒', '▒', ' ', '▒', ' ', '▒', ' ', '▒', ' ', '▒', ' ' ,'▒', '▒', '▒', '┃'},
               /*13*/{ '┃', '▒', '▒', '▒', '▒', '▒', ' ', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', ' ', ' ', ' ', ' ', '▒', ' ', '▒', ' ', '▒', ' ', '▒', ' ' ,'▒', ' ', '▒', '┃'},
               /*14*/{ '┃', '▒', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '▒', ' ', '▒', '▒', '▒', '▒', ' ', '▒', '▒', '▒', ' ', '▒', ' ' ,'▒', ' ', '▒', '┃'},
               /*15*/{ '┃', '▒', ' ', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', ' ', '▒', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '▒', ' ' ,'▒', ' ', '▒', '┃'},
               /*16*/{ '┃', '▒', ' ', ' ', ' ', ' ', '▒', ' ', ' ', ' ', '▒', ' ', '▒', ' ', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', ' ', '▒', ' ' ,'▒', ' ', '▒', '┃'},
               /*17*/{ '┃', '▒', ' ', '▒', '▒', ' ', '▒', ' ', '▒', ' ', '▒', ' ', '▒', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '▒', ' ' ,' ', ' ', '▒', '┃'},
               /*18*/{ '┃', '▒', ' ', ' ', '▒', ' ', '▒', ' ', '▒', ' ', '▒', ' ', '▒', ' ', '▒', ' ', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', ' ', '▒', '▒' ,'▒', '▒', '▒', '┃'},
               /*19*/{ '┃', '▒', ' ', ' ', '▒', ' ', ' ', ' ', '▒', ' ', ' ', ' ', ' ', ' ', '▒', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' ,' ', ' ', ' ', '┃'},
               /*20*/{ '┃', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒', '▒' ,'▒', '▒', '▒', '┃'},
               /*21*/{ '┗', '━', '━', '━', '━', '━', '━', '━', '━', '━', '━', '━', '━', '━', '━', '━', '━', '━', '━', '━', '━', '━', '━', '━', '━', '━', '━' ,'━', '━', '━', '┛'},
            };




        }
        #endregion

        // All Object First Position
        #region
        static void AllObjFirstPosition(out IsPosition playerPosition, out IsPosition goalObj, out IsPosition monsterPosition)
        {

            // PlayerPosition Reset
            playerPosition.x = 2;
            playerPosition.y = 2;
            // Goal Object Position
            goalObj.x = 29;
            goalObj.y = 19;
            // Monster Position
            monsterPosition.x = 0;
            monsterPosition.y = 0;
        }
        #endregion

        // Game Drawing
        #region
        static void Render(IsPosition playerPosiotion, IsPosition goalObj,ref IsPosition monsterPos, char[,] map)
        {
            //Console.Clear();    // Clear After Player Print
            Console.SetCursorPosition(0, 0);    // Cursor Position
            // Map Print
            MapPrint(map);
            // Player Posiotion Setting
            PlayerPrint(playerPosiotion);
            // Goal Position Setting
            GoalPrint(goalObj);
            // Monster Position Setting
            //MonsterPrint(monsterPos, map);


        }
        #endregion

        // 2D Tile Map Print 
        #region
        static void MapPrint(char[,] map)
        {
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    
                    if (map[y,x] == '▒')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write('▒');
                        Console.ResetColor();
                    }
                    else if (map[y, x] == ' ')
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.Write(' ');
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(map[y, x]);
                    }
                }
                Console.WriteLine();
            }

        }
        #endregion

        // Player Print
        #region
        static void PlayerPrint(IsPosition playerPosition)
        {

            Console.SetCursorPosition(playerPosition.x, playerPosition.y);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write('●');
            Console.ResetColor();
            
        }
        #endregion

        // Goal Obj Print
        #region
        static void GoalPrint(IsPosition goalObj)
        {
            Console.SetCursorPosition(goalObj.x, goalObj.y);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write('☆');
            Console.ResetColor();
        }
        #endregion

        // Monster Print    // 몬스터가 랜덤위치로 생성되어 움직이는 로직 추가 보완하여 구현
        #region
        //static void MonsterPrint(IsPosition monsterPos, char[,] map)
        //{
        //    Random random = new Random();
        //    bool success = true;
        //    while (success)
        //    {
        //        monsterPos.x = random.Next(2, 29);
        //        monsterPos.y = random.Next(2, 20);

        //        if (map[monsterPos.y, monsterPos.x] != '▒')
        //        {
        //            success = false;
        //        }
        //    }
        //    Console.SetCursorPosition(monsterPos.x, monsterPos.y);
        //    Console.ForegroundColor = ConsoleColor.Green;
        //    Console.WriteLine("Ω");
        //    Console.ResetColor();

        //}
        #endregion

        // Game Drawing Input
        #region
        static ConsoleKey Input()
        {
            // Player Move KeyDown Return
            return Console.ReadKey(true).Key;

        }
        #endregion

        // Game Play Update
        #region
        static void PlayUpdate(ConsoleKey keyDown, ref IsPosition playerAction, IsPosition goalPosition, char[,] map, ref bool gameOver)
        {
            // Player Move
            PlayerMoveAction(keyDown, ref playerAction, map);
            // Clear Condition 
            bool Clear = ClearStateCheck(playerAction, goalPosition);

            if (Clear)
            {
                gameOver = true;
            }
        }
        #endregion

        // Clear State Check
        #region
        static bool ClearStateCheck(IsPosition playerAction, IsPosition goalPosition)
        {
            bool finish = (playerAction.x == goalPosition.x) && (playerAction.y == goalPosition.y); // Clear Condition

            return finish;
        }
        #endregion


        // Player Move Action
        #region
        static void PlayerMoveAction(ConsoleKey keyDown, ref IsPosition playerAction, char[,] map)
        {
            switch (keyDown)
            {
                // Player Up, Down, Left, Right Move Action
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    if(map[playerAction.y-1, playerAction.x] == ' ')
                    {
                        playerAction.y--;
                        break;
                    }
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    if(map[playerAction.y+1, playerAction.x] == ' ')
                    {
                        playerAction.y++;
                        break;
                    }
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    if(map[playerAction.y, playerAction.x-1] == ' ')
                    {
                        playerAction.x--;
                        break;
                    }
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    if(map[playerAction.y, playerAction.x+1] == ' ')
                    {
                        playerAction.x++;
                        break;
                    }
                    break;
               

            }
        }
        #endregion
       
        // Game End
        #region
        static void End(Stopwatch stopWatch)
        {
            Console.Clear();
            EndText(stopWatch);


        }
        #endregion

        // Game Start Text
        #region
        static void StartText()
        {
            Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
            Console.WriteLine("┃                     ★  Game Start !!! ★                      ┃");
            Console.WriteLine("┃           출구를 찾아 이 험난한 미로에서 탈출하세요!!        ┃");
            Console.WriteLine("┃                                                              ┃");
            Console.WriteLine("┃                        W              †                      ┃");
            Console.WriteLine("┃           이동키 :  A  S  D   or   ←  ↓  →                   ┃");
            Console.WriteLine("┃                                                              ┃");
            Console.WriteLine("┃            * 아무 키나 눌러서 탈출을 시작해 보세요           ┃");
            Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");
            ConsoleKey startKey = Console.ReadKey(true).Key;
            Console.Clear();
        }
        #endregion

        // Game Clear Text
        #region
        static void EndText(Stopwatch stopWatch)
        {
            // Play Time Stop
            stopWatch.Stop();
            string scoreLevel = ScoreLevel(stopWatch);
            float clearTime = ((float)stopWatch.ElapsedMilliseconds * 0.001f);

            Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
            Console.WriteLine("┃                     ☆  Game Clear !!! ☆                      ┃");
            Console.WriteLine("┃           게임 클리어를 진심으로 축하 드립니다~~!!           ┃");
            Console.WriteLine($"┃               Game Play Time : {clearTime:N2}초 입니다.               ┃");
            Console.WriteLine("┃                                                              ┃");
            Console.WriteLine($"┃             Escape Level : {scoreLevel}              ┃");
            Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");

        }
        #endregion
        // Clear Lever Text
        #region
        static string ScoreLevel(Stopwatch stopWatch)
        {
            float scoreLevel = ((float)stopWatch.ElapsedMilliseconds * 0.001f);
            if(scoreLevel < 12f)
            {
                return "당신은! 탈출 매니아!";
            }
            else if(scoreLevel > 15f || scoreLevel < 30f)
            {
                return "조금만 더 분발하세요";
            }

            return "아이고...굶어 죽게 생겼네ㅠㅠ";
        }
        #endregion
    }
}

