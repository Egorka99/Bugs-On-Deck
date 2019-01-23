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

        private List<BugAndDir> bugslist; 

        private Point[] spawnpoints;  
        private Point[] movepoints;

        public int bugX;
        public int bugY;




        public struct BugAndDir
        {
            public Bug bug;
            public Point direction; 
        }

          
        /// <summary>
        /// Инициализируем средства для рисования
        /// </summary>
        public BugsOnDeck(Graphics g, Pen pen, SolidBrush color)
        {
            this.g = g; 
            this.pen = pen;    
            this.color = color;

            bugslist = new List<BugAndDir>();     
        } 
           
        public void Spawn()     
        { 

            spawnpoints = new Point[]
            { 
                new Point(0,0), new Point(50,0),  new Point(100,0), new Point(150,0) , new Point(200,0),
                new Point(0,50), new Point(50,50), new Point(100,50) , new Point(150,50), new Point(200,50),
                new Point(0,100), new Point(50,100), new Point(100,100) , new Point(150,100), new Point(200,100),
                new Point(0,150), new Point(50,150), new Point(100,150) , new Point(150,150), new Point(200,150),
                new Point(0,200), new Point(50,200), new Point(100,200) , new Point(150,200), new Point(200,200),
            };
             
            Random rnd = new Random();

            for (int i = 0; i < 1; i++)
            {
                bugX = spawnpoints[rnd.Next(0, spawnpoints.Length)].X;
                bugY = spawnpoints[rnd.Next(0, spawnpoints.Length)].Y;

                bugslist.Add(new BugAndDir() {
                    bug = new Bug(bugX, bugY)
            });
             
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
                item.bug.Show(g, pen, new SolidBrush(Color.Blue));
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
              
            g.Clear(Color.WhiteSmoke);  

            Random rnd = new Random(); 

            // изменяем координаты с помощью move
            foreach (var item in bugslist) 
            {

                int offsetX = movepoints[rnd.Next(0, movepoints.Length)].X; 
                int offsetY = movepoints[rnd.Next(0, movepoints.Length)].Y;
                item.bug.Move(offsetX, offsetY);

                bugX += offsetX;
                bugY += offsetY; 
            }

            // если координаты фигур совпадают , удаляем обе фигуры

            // рисуем

            Draw(); 

        } 
         
        public void Move()
        { 
             
        }
    }

    /// <summary>
    /// Свойства и методы жука  
    /// </summary> 
    public class Bug
    {
        private Point center;
        private Rectangle rect;
        private int size;

        private Graphics g; 
         
        public Bug(int x, int y) 
        {  
            center = new Point(x, y); 

            size = 50;
        }   
         
        public void Show(Graphics g, Pen pen, SolidBrush brush)
        {
            rect = new Rectangle(center.X, center.Y, size, size);
            g.DrawRectangle(pen, rect); 
            g.FillRectangle(brush, rect);
        } 
          
        public void Move(int a, int b)  
        { 
            center.X += a; center.Y += b;
        }


    } 
}
