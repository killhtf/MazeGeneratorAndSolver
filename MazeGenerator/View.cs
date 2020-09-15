﻿using System;
using System.Collections.Generic;
using System.Drawing;

namespace MazeGenerator
{
    public class View
    {

        // Набор кистей
        private readonly Brush brushBlack = new SolidBrush(Color.FromArgb(255, 60, 60, 60));
        private readonly Brush brushWhite = new SolidBrush(Color.White);
        private readonly Brush brushLimeGreen = new SolidBrush(Color.LimeGreen);
        private readonly Brush brushViolet = new SolidBrush(Color.Violet);

        Point start; // Стартовая тчока
        Point finish; // Конечная точка
        int pixelSize; // Множитель размера одного квадрата отрисовки
        int mazeWidth; // Ширина лабиринта
        int mazeHeight; // Высота лабиринта
        private readonly Random rnd = new Random();

        // Объект для отрисовки
        public Graphics GraphicsObject { get; set; }
        // Битмап
        public Bitmap MazeBitmap { get; set; }

        /// <summary>
        /// Конструктор, принимающий объект для рисования
        /// </summary>
        /// <param name="draw">Объект, на котором производится отрисовка</param>
        public View(Graphics draw)
        {
            GraphicsObject = draw;
        }

        /// <summary>
        /// Метод изначальной отрисовки лабиринта
        /// </summary>
        /// <param name="Maze">Массив лабиринта</param>
        /// <param name="width">Ширина picturebox'a</param>
        /// <param name="height">Высота picturebox'a</param>
        public void DrawMazeInitState(int mazewidth, int mazeheight, int width, int height)
        {
            GraphicsObject.Clear(Color.FromArgb(255, 240, 240, 240));
            mazeWidth = mazewidth;
            mazeHeight = mazeheight;

            int b1 = width / mazeWidth;
            int b2 = height / mazeHeight;
            pixelSize = Math.Min(b1, b2);
            FillMazePicture();
        }

        /// <summary>
        /// Отрисовка изменений при построении лабиринта
        /// </summary>
        /// <param name="point">Точка изменения</param>
        /// <param name="color">Цвет изменения</param>
        public void DrawChange(Point point, Color color)
        {
            if (IsNotStartPoint(point) && IsNotFinishPoint(point))
            {
                SolidBrush brush = new SolidBrush(color);
                GraphicsObject.FillRectangle(brush, point.X * pixelSize, point.Y * pixelSize, pixelSize, pixelSize);
                brush.Dispose();
            }
        }
        /// <summary>
        /// Перегруженный метод для "фич" при отрисовке
        /// </summary>
        /// <param name="change">Точка изменения</param>
        /// <param name="code">Код фичи</param>
        public void DrawChange(Point change, int code)
        {
            Color newColor;
            switch (code)
            {
                case 1:
                    {// Почти полный рандом
                        newColor = Color.FromArgb(255, rnd.Next(30, 256), rnd.Next(30, 256), rnd.Next(30, 256));
                        break;
                    }
                case 2:
                    {// Красный
                        newColor = Color.FromArgb(255, rnd.Next(190, 256), rnd.Next(60, 120), rnd.Next(80, 150));
                        break;
                    }
                case 3:
                    {// Зелёный
                        newColor = Color.FromArgb(255, rnd.Next(80, 120), rnd.Next(200, 250), rnd.Next(80, 150));
                        break;
                    }
                case 4:
                    {// Синий
                        newColor = Color.FromArgb(255, rnd.Next(50, 130), rnd.Next(60, 130), rnd.Next(190, 256));
                        break;
                    }
                case 50:
                    {// 50 оттенков серого (orly)
                        int a = rnd.Next(150, 200);
                        newColor = Color.FromArgb(255, a, a, a);
                        break;
                    }
                case 23:
                    {// Желтый
                        newColor = Color.FromArgb(255, rnd.Next(180, 230), rnd.Next(180, 240), rnd.Next(40, 120));
                        break;
                    }
                case 34:
                    {// Бирюзовый
                        newColor = Color.FromArgb(255, rnd.Next(40, 120), rnd.Next(175, 250), rnd.Next(180, 230));
                        break;
                    }
                case 42:
                    {// Фиолетовый
                        newColor = Color.FromArgb(255, rnd.Next(170, 220), rnd.Next(40, 120), rnd.Next(175, 250));
                        break;
                    }
                case 48:
                    {// Тёмная цветовая гамма
                        newColor = Color.FromArgb(255, rnd.Next(80, 140), rnd.Next(80, 140), rnd.Next(80, 140));
                        break;
                    }
                case 49:
                    {// Светлая цветовая гамма
                        newColor = Color.FromArgb(255, rnd.Next(190, 256), rnd.Next(190, 256), rnd.Next(190, 256));
                        break;
                    }
                default:
                    {
                        newColor = Color.FromArgb(255, 255, 170, 102);
                        break;
                    }
            }
            DrawChange(change, newColor);
        }

        /// <summary>
        /// Отрисовка чёрных квадратов при генерации пустых областей
        /// </summary>
        /// <param name="black">Список точек</param>
        public void DrawBlackPoints(List<Point> black)
        {
            for (int i = 0; i < black.Count; i++)
                DrawChange(black[i], Color.FromArgb(255, 60, 60, 60));
        }

        /// <summary>
        /// Метод, задающий точки старта и финиша
        /// </summary>
        /// <param name="st">Точка старта</param>
        /// <param name="fin">Точка финиша</param>
        public void SetStartAndFinish(Point st, Point fin)
        {
            start = st;
            finish = fin;
        }

        /// <summary>
        /// Отрисовка лабиринта в bitmap
        /// </summary>
        /// <param name="Maze">Лабиринт</param>
        /// <param name="size">Размер для отрисовки квадрата</param>
        public void InitMazeBitmap(int[,] Maze, int size)
        {
            mazeWidth = Maze.GetLength(0);
            mazeHeight = Maze.GetLength(1);
            pixelSize = size;
            int w = mazeWidth * pixelSize;
            int h = mazeHeight * pixelSize;
            MazeBitmap = new Bitmap(w, h);
            GraphicsObject = Graphics.FromImage(MazeBitmap);
            GraphicsObject.Clear(Color.FromArgb(255, 240, 240, 240));
            FillMazePicture();
        }

        /// <summary>
        /// Метод для стартовой отрисовки лабиринта
        /// </summary>
        private void FillMazePicture()
        {
            // Отрисовка всего поля, старта и финиша
            GraphicsObject.FillRectangle(brushWhite, 0, 0, mazeWidth * pixelSize, mazeHeight * pixelSize);
            GraphicsObject.FillRectangle(brushViolet, pixelSize * start.X, pixelSize * start.Y, pixelSize, pixelSize);
            GraphicsObject.FillRectangle(brushLimeGreen, finish.X * pixelSize, finish.Y * pixelSize, pixelSize, pixelSize);

            //Отрисовка чёрных полос для создания поля
            for (int i = 0; i < mazeHeight; i++)
            {
                if (i % 2 == 0)
                    GraphicsObject.FillRectangle(brushBlack, 0, i * pixelSize, mazeWidth * pixelSize, pixelSize);
            }
            for (int j = 0; j < mazeWidth; j++)
            {
                if (j % 2 == 0)
                    GraphicsObject.FillRectangle(brushBlack, j * pixelSize, 0, pixelSize, mazeHeight * pixelSize);
            }
        }

        /// <summary>
        /// Отрисовка круга при самостоятельном прохождении лабиринта
        /// </summary>
        /// <param name="change">Точка для отрисовки</param>
        /// <param name="color">Цвет точки</param>
        public void DrawCircle(Point change, Color color)
        {
            SolidBrush brush = new SolidBrush(color);
            GraphicsObject.FillEllipse(brush, change.X * pixelSize, change.Y * pixelSize, pixelSize, pixelSize);
            brush.Dispose();
        }

        /// <summary>
        /// Проверка, является ли точка стартом лабиринта
        /// </summary>
        /// <param name="point">Точка</param>
        /// <returns>true, если точка не является стартом лабиринта</returns>
        private bool IsNotStartPoint(Point point)
        {
            return point.X != start.X || point.Y != start.Y;
        }

        /// <summary>
        /// Проверка, является ли точка финишем лабиринта
        /// </summary>
        /// <param name="point">Точка</param>
        /// <returns>true, если точка не является финишем лабиринта</returns>
        private bool IsNotFinishPoint(Point point)
        {
            return point.X != finish.X || point.Y != finish.Y;
        }

        /// <summary>
        /// Освобождение ресурсов
        /// </summary>
        public void Dispose()
        {
            GraphicsObject.Dispose();
            MazeBitmap.Dispose();
        }
    }
}
