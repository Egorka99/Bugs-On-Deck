using System;
using System.Drawing;
using System.Collections.Generic; 

namespace ClassLibraryBugs
{ 
      
    public class BugsOnDeck
    { 
        private Graphics g;
        private Pen pen; 
        private SolidBrush color;

        public List<BugProp> bugslist; 

        private List<Point> spawnpoints;  
        private Point[] movepoints;


        public class BugProp
        {
            public Bug bug; 
            public Point direction; 
            public string movingmap;
            public bool canmoving; 
        } 

           
        /// <summary> 
        /// Инициализируем средства для рисования
        /// </summary>
        public BugsOnDeck(Graphics g, Pen pen, SolidBrush color)
        {
            this.g = g; 
            this.pen = pen;    
            this.color = color;

            bugslist = new List<BugProp>();     
        } 
           
        public void Spawn()     
        { 
             
            spawnpoints = new List<Point>
            { 
                new Point(0,0), new Point(50,0),  new Point(100,0), new Point(150,0) , new Point(200,0),
                new Point(0,50), new Point(50,50), new Point(100,50) , new Point(150,50), new Point(200,50),
                new Point(0,100), new Point(50,100), new Point(100,100) , new Point(150,100), new Point(200,100),
                new Point(0,150), new Point(50,150), new Point(100,150) , new Point(150,150), new Point(200,150),
                new Point(0,200), new Point(50,200), new Point(100,200) , new Point(150,200), new Point(200,200),
            };
             
            Random rnd = new Random();

            // Появление жуков 
            for (int i = 0; i < 5; i++)
            {
                // Координаты появления
                int bugX = spawnpoints[rnd.Next(0, spawnpoints.Count)].X; 
                int bugY = spawnpoints[rnd.Next(0, spawnpoints.Count)].Y;

                // Добавляем жуков
                bugslist.Add(new BugProp() {
                    bug = new Bug(bugX, bugY),
                    movingmap = "",
                    canmoving = true
                }); 
                 
                // удаляем использованные точки спавна
                for (int j = 0; j < spawnpoints.Count; j++)
                {
                    if (spawnpoints[j].X == bugX && spawnpoints[j].Y == bugY) 
                        spawnpoints.Remove(spawnpoints[j]);  
                } 
   
            } 
              
            //bug1 = new Bug(spawnpoints[rnd.Next(0, spawnpoints.Length)].X, spawnpoints[rnd.Next(0, spawnpoints.Length)].Y);
            //bug2 = new Bug(spawnpoints[rnd.Next(0, spawnpoints.Length)].X, spawnpoints[rnd.Next(0, spawnpoints.Length)].Y);
            //bug3 = new Bug(spawnpoints[rnd.Next(0, spawnpoints.Length)].X, spawnpoints[rnd.Next(0, spawnpoints.Length)].Y);
            //bug4 = new Bug(spawnpoints[rnd.Next(0, spawnpoints.Length)].X, spawnpoints[rnd.Next(0, spawnpoints.Length)].Y);
            //bug5 = new Bug(spawnpoints[rnd.Next(0, spawnpoints.Length)].X, spawnpoints[rnd.Next(0, spawnpoints.Length)].Y);

            //сделать remove  

            //bug1.Show(g, pen, new SolidBrush(Color.Red));
            //bug2.Show(g, pen, new SolidBrush(Color.Green));
            //bug3.Show(g, pen, new SolidBrush(Color.Blue));
            //bug4.Show(g, pen, new SolidBrush(Color.Yellow));
            //bug5.Show(g, pen, new SolidBrush(Color.Purple)); 

            Draw(); 
           
        }       

        private void Draw()
        {
            foreach (var item in bugslist)
            {
                item.bug.Show(g, pen, new SolidBrush(Color.DarkRed));
            }
        } 

        /// <summary>
        /// Остановка жуков при столкновении или выходе из поля
        /// </summary>
        private void Stop(List<BugProp> bugslist)
        { 
            // Массив индексов, которые необходимо удалить
            List<int> index = new List<int>();

            // сравниваем все координаты, если есть одинаковые - произошло столкновение, запрещаем движение
            for (int i = 0; i < bugslist.Count; i++)
            {
                for (int j = 0; j < bugslist.Count; j++)
                {
                    // выход за пределы поля
                    if (bugslist[i].bug.GetX() < 0 || bugslist[i].bug.GetX() > 200 ||
                        bugslist[i].bug.GetY() < 0 || bugslist[i].bug.GetY() > 200)
                    {
                        if (!index.Contains(i))
                            index.Add(i);
                    }

                    if (i != j)
                    {
                        //если коорд одинаковые
                        if (bugslist[i].bug.GetX() == bugslist[j].bug.GetX() &&
                            bugslist[i].bug.GetY() == bugslist[j].bug.GetY())
                        {
                            // записываем индексы в массив  
                            if (!index.Contains(i))
                                index.Add(i);
                            if (!index.Contains(j))
                                index.Add(j);
                        }
                    }

                }
            }
         
            // запрещаем движение  жуков 
            foreach (var item in index)
            {
                   bugslist[item].canmoving = false;
            }
        }
             
        public void FirstStep() 
        {
            movepoints = new Point[]
            {
                new Point(50,0), //влево
                new Point(-50,0), //вправо 
                new Point(0,-50), //вверх
                new Point(0,50), //вниз
            };
              
            g.Clear(Color.Peru);



            Random rnd = new Random();

            // изменяем координаты с помощью move
            for (int i = 0; i < 5; i++)
            {

                Point offsetPoint = movepoints[rnd.Next(0, movepoints.Length)];
                int offsetX = offsetPoint.X;
                int offsetY = offsetPoint.Y;
                 
                AddWay(bugslist[i], offsetX, offsetY);
                  

                bugslist[i].bug.Move(offsetX , offsetY);
                bugslist[i].direction = new Point(offsetX, offsetY);

            }

            Stop(bugslist); 
              
             
            // рисуем
            Draw();  

        } 
        /// <summary>
        /// Обновить путь жука в MovingMap
        /// </summary> 
        /// <param name="bug"></param>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        public void AddWay(BugProp bug, int offsetX,int offsetY)
        {
            if (offsetX == 50 && offsetY == 0)
                bug.movingmap += "right" + " ";
            if (offsetX == -50 && offsetY == 0) 
                bug.movingmap += "left" + " "; 
            if (offsetX == 0 && offsetY == -50)
                bug.movingmap += "up" + " ";
            if (offsetX == 0 && offsetY == 50)
                bug.movingmap += "down" + " ";
        }

        public List<string> Info() 
        {
            List<string> info = new List<string>();

            foreach (var item in bugslist)
            {
                info.Add(
                    "Координаты: " + item.bug.GetX() + ":" + item.bug.GetY() +
                    " Перемещения: " + item.movingmap
                    );
            }
            return info; 
        } 

        public void Move()
        {
            g.Clear(Color.Peru);

            foreach (var item in bugslist)
            {
                if (item.canmoving)
                {
                    item.bug.Move(item.direction.X, item.direction.Y);
                    AddWay(item, item.direction.X, item.direction.Y);

                    Stop(bugslist);


                } 
       
            }

            Draw(); 
        } 
    }

    /// <summary>
    /// Свойства и методы жука  
    /// </summary> 
    public class Bug
    {
        private Point leftup;
        private Rectangle rect;
        private int size;
         
        public Bug(int x, int y) 
        {  
            leftup = new Point(x, y); 

            size = 50;
        }   
         
        public void Show(Graphics g, Pen pen, SolidBrush brush)
        {
            rect = new Rectangle(leftup.X, leftup.Y, size, size);
            g.DrawRectangle(pen, rect); 
            g.FillRectangle(brush, rect);
        } 
          
        public void Move(int a, int b)  
        { 
            leftup.X += a; leftup.Y += b;
        }


        public int GetX() { return leftup.X; }
        public int GetY() { return leftup.Y; }

         
    }    
}
