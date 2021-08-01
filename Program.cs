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
            DeleteStandAloneRoom(rooms);
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
        private static List<Room> GenerateRoomsGraph(int deep, int x = 0, int y = 0)
        {
            var rooms = new List<Room>();
            var room = new Room { RoomExists = 1, X = x, Y = y };
            rooms.Add(room);
            var rand = new Random();
            if (deep > 0)
            {
               if (rand.Next(0, 5) != 0)
                {
                    rooms.AddRange(GenerateRoomsGraph(--deep, ++x, y));
                }
                if (rand.Next(0, 5) != 0)
                {
                    rooms.AddRange(GenerateRoomsGraph(--deep, --x, y));
                }
                if (rand.Next(0, 5) != 0)
                {
                    rooms.AddRange(GenerateRoomsGraph(--deep, x, ++y));
                }
                if (rand.Next(0, 5) != 0)
                {
                    rooms.AddRange(GenerateRoomsGraph(--deep, x, --y));
                }
            }
            return rooms;
        }

        private static List<Room> CoordNormilize(List<Room> rooms)
        {
            var roomsNorm = new List<Room>();
            var x = rooms.Min(r => r.X);
            var y = rooms.Min(r => r.Y);
            if (x < 0 && y < 0)
            {
                foreach (var room in rooms)
                {
                    var tempRoom = room;
                    tempRoom.X += Math.Abs(x);
                    tempRoom.Y += Math.Abs(y);
                    roomsNorm.Add(tempRoom);
                }
            }
            else if (x < 0)
            {
                foreach (var room in rooms)
                {
                    var tempRoom = room;
                    tempRoom.X += Math.Abs(x);
                    roomsNorm.Add(tempRoom);
                }
            }
            else if (y < 0)
            {
                foreach (var room in rooms)
                {
                    var tempRoom = room;
                    tempRoom.Y += Math.Abs(y);
                    roomsNorm.Add(tempRoom);
                }
            }
            

            return rooms;
        }
        /// <summary>
        /// Delete rooms without neighbor, while not find The Tardis
        /// </summary>
        /// <param name="rooms"></param>
        private static void DeleteStandAloneRoom(List<Room> rooms)
        {
            foreach (var room in rooms)
            {
                if (room.RoomExists == 0)
                {
                    continue;
                }
                var bottomRoom = rooms.Where(r => r.X == room.X + 1 && r.Y == room.Y).FirstOrDefault();
                var topRoom = rooms.Where(r => r.X == room.X - 1 && r.Y == room.Y).FirstOrDefault();
                var leftRoom = rooms.Where(r => r.X == room.X && r.Y == room.Y - 1).FirstOrDefault();
                var rightRoom = rooms.Where(r => r.X == room.X && r.Y == room.Y + 1).FirstOrDefault();

                if (topRoom == null && leftRoom == null)
                {
                    if (rightRoom.RoomExists == 0 && bottomRoom.RoomExists == 0)
                    {
                        room.RoomExists = 0;
                    }
                }
                else if (topRoom == null && rightRoom == null)
                {
                    if (leftRoom.RoomExists == 0 && bottomRoom.RoomExists == 0)
                    {
                        room.RoomExists = 0;
                    }
                }
                else if (topRoom == null)
                {
                    if (rightRoom.RoomExists == 0 && bottomRoom.RoomExists == 0 && leftRoom.RoomExists == 0)
                    {
                        room.RoomExists = 0;
                    }
                }
                else if (bottomRoom == null && leftRoom == null)
                {
                    if (rightRoom.RoomExists == 0 && topRoom.RoomExists == 0)
                    {
                        room.RoomExists = 0;
                    }
                }
                else if (bottomRoom == null && rightRoom == null)
                {
                    if (leftRoom.RoomExists == 0 && topRoom.RoomExists == 0)
                    {
                        room.RoomExists = 0;
                    }
                }
                else if (bottomRoom == null)
                {
                    if (rightRoom.RoomExists == 0 && topRoom.RoomExists == 0 && leftRoom.RoomExists == 0)
                    {
                        room.RoomExists = 0;
                    }
                }
                else if (leftRoom == null)
                {
                    if (rightRoom.RoomExists == 0 && topRoom.RoomExists == 0 && bottomRoom.RoomExists == 0)
                    {
                        room.RoomExists = 0;
                    }
                }
                else if (rightRoom == null)
                {
                    if (leftRoom.RoomExists == 0 && topRoom.RoomExists == 0 && bottomRoom.RoomExists == 0)
                    {
                        room.RoomExists = 0;
                    }
                }
                else
                {
                    if (rightRoom.RoomExists == 0 && leftRoom.RoomExists == 0 && topRoom.RoomExists == 0 && bottomRoom.RoomExists == 0)
                    {
                        room.RoomExists = 0;
                    }
                }
            }
        }

        private static void BeforeDeleteStandAloneRoom(List<Room> rooms)
        {
            foreach (var room in rooms)
            {
                if (room.X == 0)
                {
                    var bottomRoom = rooms.Where(r => r.X == room.X + 1 && r.Y == room.Y).FirstOrDefault();

                    if (room.Y == 0)
                    {
                        var rightRoom = rooms.Where(r => r.X == room.X && r.Y == room.Y + 1).FirstOrDefault();
                        if (rightRoom.RoomExists == 0 && bottomRoom.RoomExists == 0)
                        {
                            room.RoomExists = 0;
                        }
                    }
                    else if (room.Y == rooms.Last().Y)
                    {
                        var leftRoom = rooms.Where(r => r.X == room.X && r.Y == room.Y - 1).FirstOrDefault();
                        if (leftRoom.RoomExists == 0 && bottomRoom.RoomExists == 0)
                        {
                            room.RoomExists = 0;
                        }
                    }
                    else
                    {
                        var rightRoom = rooms.Where(r => r.X == room.X && r.Y == room.Y + 1).FirstOrDefault();
                        var leftRoom = rooms.Where(r => r.X == room.X && r.Y == room.Y - 1).FirstOrDefault();
                        if (rightRoom.RoomExists == 0 && leftRoom.RoomExists == 0 && bottomRoom.RoomExists == 0)
                        {
                            room.RoomExists = 0;
                        }
                    }
                }
                else if (room.X == rooms.Last().X)
                {
                    var topRoom = rooms.Where(r => r.X == room.X - 1 && r.Y == room.Y).FirstOrDefault();

                    if (room.Y == 0)
                    {
                        var rightRoom = rooms.Where(r => r.X == room.X && r.Y == room.Y + 1).FirstOrDefault();
                        if (rightRoom.RoomExists == 0 && topRoom.RoomExists == 0)
                        {
                            room.RoomExists = 0;
                        }
                    }
                    else if (room.Y == rooms.Last().Y)
                    {
                        var leftRoom = rooms.Where(r => r.X == room.X && r.Y == room.Y - 1).FirstOrDefault();
                        if (leftRoom.RoomExists == 0 && topRoom.RoomExists == 0)
                        {
                            room.RoomExists = 0;
                        }
                    }
                    else
                    {
                        var rightRoom = rooms.Where(r => r.X == room.X && r.Y == room.Y + 1).FirstOrDefault();
                        var leftRoom = rooms.Where(r => r.X == room.X && r.Y == room.Y - 1).FirstOrDefault();
                        if (rightRoom.RoomExists == 0 && leftRoom.RoomExists == 0 && topRoom.RoomExists == 0)
                        {
                            room.RoomExists = 0;
                        }
                    }
                }
                else
                {
                    var topRoom = rooms.Where(r => r.X == room.X - 1 && r.Y == room.Y).FirstOrDefault();
                    var bottomRoom = rooms.Where(r => r.X == room.X + 1 && r.Y == room.Y).FirstOrDefault();
                    if (room.Y == 0)
                    {
                        var rightRoom = rooms.Where(r => r.X == room.X && r.Y == room.Y + 1).FirstOrDefault();
                        if (rightRoom.RoomExists == 0 && topRoom.RoomExists == 0 && bottomRoom.RoomExists == 0)
                        {
                            room.RoomExists = 0;
                        }
                    }
                    else if (room.Y == rooms.Last().Y)
                    {
                        var leftRoom = rooms.Where(r => r.X == room.X && r.Y == room.Y - 1).FirstOrDefault();
                        if (leftRoom.RoomExists == 0 && topRoom.RoomExists == 0 && bottomRoom.RoomExists == 0)
                        {
                            room.RoomExists = 0;
                        }
                    }
                    else
                    {
                        var rightRoom = rooms.Where(r => r.X == room.X && r.Y == room.Y + 1).FirstOrDefault();
                        var leftRoom = rooms.Where(r => r.X == room.X && r.Y == room.Y - 1).FirstOrDefault();
                        if (rightRoom.RoomExists == 0 && leftRoom.RoomExists == 0 && topRoom.RoomExists == 0 && bottomRoom.RoomExists == 0)
                        {
                            room.RoomExists = 0;
                        }
                    }
                }
            }
        }

        private static bool CheckRoomForExists(Room room)
        {
            if (room != null)
                return room.RoomExists == 0;
            else
                return true;
        }

        /// <summary>
        /// Service Method (Cheet)
        /// </summary>
        /// <param name="rooms"></param>
        private static void ShowMap(List<Room> rooms, bool showValue = false)
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
