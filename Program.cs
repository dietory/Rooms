using System;
using System.Collections.Generic;
using System.Linq;

namespace Rooms
{
    class Room
    {
        public int RoomExists { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Value { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var  test = GenerateRooms(6, 6);
            Console.ReadKey();
            //while(true)
            //{
            //    Console.WriteLine("Куда пойдешь дальше?");
            //    Console.WriteLine($"Выберай: {NextRooms()}");
            //    ChooseRoom(Console.ReadLine());
            //}
        }
        
        private static List<Room> GenerateRooms(int mapH, int mapW)
        {
            var rand = new Random();
            var rooms = new List<Room>();
            for (int i = 0; i < mapW; i++)
            {
                
                for (int k = 0; k < mapH; k++)
                {
                    var room = new Room();
                    room.X = i;
                    room.Y = k;
                    room.RoomExists = rand.Next(0, 2);
                    rooms.Add(room);
                    
                }
            }
            //DeleteStandAloneRoom(rooms);
            var valueCount = rooms.Where(r => r.RoomExists == 1).Count();
            var valueList = new List<int>();
            for (int i = 1; i <= valueCount; i++)
            {
                valueList.Add(i);
            }

            foreach (var room in rooms.Where(r => r.RoomExists == 1))
            {
                var valueIndex = rand.Next(0, valueList.Count);
                room.Value = valueList.IndexOf(valueIndex);
                valueList.RemoveAt(valueIndex);
            }
            return rooms;
        }

        /// <summary>
        /// Delete rooms without neighbor, while not find The Tardis
        /// </summary>
        /// <param name="rooms"></param>
        private static void DeleteStandAloneRoom(List<Room> rooms)
        {
            foreach(var room in rooms)
            {
                if (room.X == 0)
                {
                    var bottomRoom = rooms.Where(r => r.X == room.X + 1 && r.Y == room.Y).First();

                    if (room.Y == 0)
                    {
                        var rightRoom = rooms.Where(r => r.X == room.X && r.Y == room.Y + 1).First();
                        if (rightRoom.RoomExists == 0 && bottomRoom.RoomExists == 0)
                        {
                            room.RoomExists = 0;
                        }
                    }
                    else if (room.Y == rooms.Last().Y)
                    {
                        var leftRoom = rooms.Where(r => r.X == room.X && r.Y == room.Y - 1).First();
                        if (leftRoom.RoomExists == 0 && bottomRoom.RoomExists == 0)
                        {
                            room.RoomExists = 0;
                        }
                    }
                    else
                    {
                        var rightRoom = rooms.Where(r => r.X == room.X && r.Y == room.Y + 1).First();
                        var leftRoom = rooms.Where(r => r.X == room.X && r.Y == room.Y - 1).First();
                        if (rightRoom.RoomExists == 0 && leftRoom.RoomExists == 0 && bottomRoom.RoomExists == 0)
                        {
                            room.RoomExists = 0;
                        }
                    }
                }
                else if (room.X == rooms.Last().X)
                {
                    var topRoom = rooms.Where(r => r.X == room.X - 1 && r.Y == room.Y).First();

                    if (room.Y == 0)
                    {
                        var rightRoom = rooms.Where(r => r.X == room.X && r.Y == room.Y + 1).First();
                        if (rightRoom.RoomExists == 0 && topRoom.RoomExists == 0)
                        {
                            room.RoomExists = 0;
                        }
                    }
                    else if (room.Y == rooms.Last().Y)
                    {
                        var leftRoom = rooms.Where(r => r.X == room.X && r.Y == room.Y - 1).First();
                        if (leftRoom.RoomExists == 0 && topRoom.RoomExists == 0)
                        {
                            room.RoomExists = 0;
                        }
                    }
                    else
                    {
                        var rightRoom = rooms.Where(r => r.X == room.X && r.Y == room.Y + 1).First();
                        var leftRoom = rooms.Where(r => r.X == room.X && r.Y == room.Y - 1).First();
                        if (rightRoom.RoomExists == 0 && leftRoom.RoomExists == 0 && topRoom.RoomExists == 0)
                        {
                            room.RoomExists = 0;
                        }
                    }
                }
                else
                {
                    var topRoom = rooms.Where(r => r.X == room.X - 1 && r.Y == room.Y).First();
                    var bottomRoom = rooms.Where(r => r.X == room.X + 1 && r.Y == room.Y).First();
                    if (room.Y == 0)
                    {
                        var rightRoom = rooms.Where(r => r.X == room.X && r.Y == room.Y + 1).First();
                        if (rightRoom.RoomExists == 0 && topRoom.RoomExists == 0 && bottomRoom.RoomExists == 0)
                        {
                            room.RoomExists = 0;
                        }
                    }
                    else if (room.Y == rooms.Last().Y)
                    {
                        var leftRoom = rooms.Where(r => r.X == room.X && r.Y == room.Y - 1).First();
                        if (leftRoom.RoomExists == 0 && topRoom.RoomExists == 0 && bottomRoom.RoomExists == 0)
                        {
                            room.RoomExists = 0;
                        }
                    }
                    else
                    {
                        var rightRoom = rooms.Where(r => r.X == room.X && r.Y == room.Y + 1).First();
                        var leftRoom = rooms.Where(r => r.X == room.X && r.Y == room.Y - 1).First();
                        if (rightRoom.RoomExists == 0 && leftRoom.RoomExists == 0 && topRoom.RoomExists == 0 && bottomRoom.RoomExists == 0)
                        {
                            room.RoomExists = 0;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Service Method (Cheet)
        /// </summary>
        /// <param name="rooms"></param>
        private static void ShowMap(List<Room> rooms, bool showValue)
        {
            int prevX = 0;
            foreach (var room in rooms)
            {
                
                int currX = room.X;
                if(currX > prevX)
                {
                    Console.WriteLine();
                }
                if(showValue)
                {
                    Console.Write(room.Value);
                }
                else
                {
                    Console.Write(room.RoomExists);
                }
                

                prevX = currX;                
            }
        }
        private static void ChooseRoom(string room)
        {
            throw new NotImplementedException();
        }

        private static string NextRooms()
        {
            throw new NotImplementedException();
        }
    }
}
