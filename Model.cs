using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    class Model
    {
        public int[,] pole = { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };

        public int skore = 0;

        public void Pridej(int cislo)
        {
            List<Tuple<int,int>> prazdne = new List<Tuple<int, int>>();

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    if (pole[x, y] == 0)
                    {
                        prazdne.Add(new Tuple<int, int>(x, y));
                    }
                }
            }

            Random random = new Random();
            int nahodne = random.Next(0, prazdne.Count);

            Tuple<int, int> souradnice = prazdne[nahodne];
            pole[souradnice.Item1, souradnice.Item2] = cislo;
        }

        public void Posun(int x, int y)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int radek = (x == 1) ? 3 - i : i;
                    int sloupec = (y == 1) ? 3 - j : j;

                    PosunJeden(radek, sloupec, x, y);
                }
            }
        }

        public void PosunJeden(int realneX, int realneY, int x, int y)
        {
            if (pole[realneX, realneY] == 0) return;

            int nextX = realneX + x;
            int nextY = realneY + y;

            while (Uvnitr(nextX, nextY))
            {
                if (pole[nextX, nextY] == 0)
                {
                    pole[nextX, nextY] = pole[realneX, realneY];
                    pole[realneX, realneY] = 0;
                    realneX = nextX;
                    realneY = nextY;
                    nextX += x;
                    nextY += y;
                }
                else if (pole[nextX, nextY] == pole[realneX, realneY])
                {
                    // Spojování
                    pole[nextX, nextY] *= 2;
                    skore += pole[nextX, nextY];
                    pole[realneX, realneY] = 0;
                    break;
                }
                else
                {
                    break; // Nemůže dál
                }
            }
        }

        public bool Misto(int realneX, int realneY, int x, int y)
        {
            while (Uvnitr(realneX + x, realneY + y))
            {
                if (pole[realneX + x, realneY + y] == 0)
                {
                    return true; // Neni místo
                }
                realneX += x;
                realneY += y;
            }
            return false;
        }

        public bool Uvnitr(int x, int y)
        {
            return x >= 0 && x < 4 && y >= 0 && y < 4;
        }
    }
}
