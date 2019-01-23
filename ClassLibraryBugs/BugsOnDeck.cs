using System;
using System.Drawing;
using System.Collections.Generic; 

namespace ClassLibraryBugs
{ 
    public class BugsOnDeck
    {
        // средства для рисования фигур
        private Graphics g;
        private Pen pen; 
        private SolidBrush color; 

        /// <summary> 
        /// Список жуков 
        /// </summary>
        public List<BugProp> bugslist; 
         
        /// <summary>
        /// Координаты для появления жука
        /// </summary>
        private List<Point> spawnpoints;  

        /// <summary>
        /// Координаты для движения жука
        /// </summary>
        private Point[] movepoints;

        /// <summary>
        /// Жук и его свойства 
        /// </summary>
        public class BugProp
        {   
            /// <summary>
            /// Жук
            /// </summary>
            public Bug bug; 

            /// <summary>
            /// Координаты, задающие направление движения
            /// </summary>
            public Point Direction; 

            /// <summary>
            /// Список перемещений жука
            /// </summary>
            public string MovingMap;

            /// <summary>
            /// Не запрещено ли у жука движение 
            /// </summary>
            public bool CanMoving; 
        } 

           
        /// <summary> 
        /// Инициализируем средства для рисования и список
        /// </summary>
        public BugsOnDeck(Graphics g, Pen pen, SolidBrush color)
        {
            this.g = g; 
            this.pen = pen;    
            this.color = color;

            bugslist = new List<BugProp>();

            g.Clear(Color.Peru);
        } 
           
        /// <summary>
        /// Появление 5 жуков на доске
        /// </summary>
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
                    MovingMap = "",
                    CanMoving = true
                }); 
                  
                // удаляем использованные точки спавна
                for (int j = 0; j < spawnpoints.Count; j++)
                {
                    if (spawnpoints[j].X == bugX && spawnpoints[j].Y == bugY) 
                        spawnpoints.Remove(spawnpoints[j]);  
                } 
   
            } 

            // рисуем жуков
            Draw(); 
           
        }       

        /// <summary>
        /// Метод для рисование жуков на доске
        /// </summary>
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
                   bugslist[item].CanMoving = false;
            }
        }
        
        /// <summary>
        /// Первый шаг: случайным образом двигаем жука и запоминаем направление
        /// </summary>
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
                // запоминаем направление
                bugslist[i].Direction = new Point(offsetX, offsetY);

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
                bug.MovingMap += "right" + " ";
            if (offsetX == -50 && offsetY == 0) 
                bug.MovingMap += "left" + " "; 
            if (offsetX == 0 && offsetY == -50)
                bug.MovingMap += "up" + " ";
            if (offsetX == 0 && offsetY == 50)
                bug.MovingMap += "down" + " ";
        }

        /// <summary>
        /// Информация о жуке: координаты, перемещение
        /// </summary>
        /// <returns></returns>
        public List<string> Info() 
        {
            List<string> info = new List<string>();

            foreach (var item in bugslist)
            {
                info.Add(
                    "Координаты: " + item.bug.GetX() + ":" + item.bug.GetY() +
                    " Перемещения: " + item.MovingMap
                    );
            }
            return info; 
        } 
        /// <summary>
        /// Движение жука в определенную сторону
        /// </summary>
        public void Move()
        {
            g.Clear(Color.Peru);

            foreach (var item in bugslist)
            {
                // может ли этот жук двигатся
                if (item.CanMoving)
                {
                    item.bug.Move(item.Direction.X, item.Direction.Y);
                    AddWay(item, item.Direction.X, item.Direction.Y);

                    Stop(bugslist);

                } 
       
            }

            Draw(); 
        } 

        /// <summary>
        /// Проверка: конец игры 
        /// </summary>
        /// <returns></returns>
        public bool Check()
        {
            foreach (var item in bugslist)
            {
                if (item.CanMoving) 
                    return false;
            }

            bugslist.Clear();

            return true;
        } 
    }

}
