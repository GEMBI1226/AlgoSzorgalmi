using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoSzorgalmi
{
    struct Node
    {
        public int Ertek;
        public int Kov;
    }
    public class LancoltLista
    {
        const int VegJel = -1;
        const int MaxElem = 20;

        Node[] Elem = new Node[MaxElem + 1];
        int Szabad;
        public int Fej;
        public void SzabadKezd()
        {
            Szabad = 1;

            for (int i = 1; i < MaxElem; i++)
                Elem[i].Kov = i + 1;

            Elem[MaxElem].Kov = VegJel;
            Fej = VegJel;
        }
        public bool Lefoglal(out int hely)
        {
            if (Szabad != VegJel)
            {
                hely = Szabad;
                Szabad = Elem[Szabad].Kov;
                return true;
            }
            else
            {
                hely = VegJel;
                return false;
            }
        }
        public void Felszabadit(int hely)
        {
            if (hely != VegJel)
            {
                Elem[hely].Kov = Szabad;
                Szabad = hely;
            }
        }
        public bool ListaFuzElol(int adat)
        {
            int p;
            if (Lefoglal(out p))
            {
                Elem[p].Ertek = adat;
                Elem[p].Kov = Fej;
                Fej = p;
                return true;
            }
            return false;
        }

        public bool ListaTorolElol()
        {
            int p;
            if (Fej != VegJel)
            {
                p = Fej;
                Fej = Elem[Fej].Kov;
                Felszabadit(p);
                return true;
            }
            return false;
        }

        public bool ListaFuzMoge(int EzUtan, int adat)
        {
            int p;
            if (EzUtan != VegJel && Lefoglal(out p))
            {
                Elem[p].Ertek = adat;
                Elem[p].Kov = Elem[EzUtan].Kov;
                Elem[EzUtan].Kov = p;
                return true;
            }
            return false;
        }

        public bool ListaTorolMogul(int EMogul)
        {
            int p;
            if (EMogul != VegJel && (Elem[EMogul].Kov != VegJel))
            {
                p = Elem[EMogul].Kov;
                Elem[EMogul].Kov = Elem[p].Kov;
                Felszabadit(p);
                return true;
            }
            return false;
        }

        public bool ListaFuzVeg(int adat)
        {
            int p;
            if (Fej == VegJel)
            {
                return ListaFuzElol(adat);
            }
            else
            {
                p = Fej;
                while (Elem[p].Kov != VegJel)
                {
                    p = Elem[p].Kov;
                }
                return ListaFuzMoge(p, adat);
            }
        }
        public bool ListaTorolVeg()
        {
            int pe, p;
            if (Fej != VegJel)
            {
                if (Elem[Fej].Kov == VegJel)
                {
                    return ListaTorolElol();
                }
                else
                {
                    pe = Fej;
                    p = Elem[Fej].Kov;
                    while (Elem[p].Kov != VegJel)
                    {
                        pe = p;
                        p = Elem[p].Kov;
                    }
                    return ListaTorolMogul(pe);
                }
            }
            return false;
        }

        public bool ListaKereses(int adat, out int hely)
        {
            int p;
            p = Fej;
            while (p != VegJel && Elem[p].Ertek != adat)
            {
                p = Elem[p].Kov;
            }
            hely = p;
            return p != VegJel;
        }

        public void Kiir()
        {
            int p;
            Console.Write("Lista: ");
            p = Fej;
            while (p != VegJel)
            {
                Console.Write(Elem[p].Ertek + " ");
                p = Elem[p].Kov;
            }
            Console.WriteLine();
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            int hely;
            LancoltLista lista = new LancoltLista();
            lista.SzabadKezd();

            for (int i = 1; i <= 10; i++)
                lista.ListaFuzVeg(i);

            Console.WriteLine("Lista feltöltve:");
            lista.Kiir();
            Console.WriteLine();

            lista.ListaTorolElol();
            Console.WriteLine("Első elem törlése:");
            lista.Kiir();
            Console.WriteLine();

            lista.ListaFuzMoge(lista.Fej, 69);
            Console.WriteLine("69 beszúrva a FEJ mögé:");
            lista.Kiir();
            Console.WriteLine();

            lista.ListaTorolVeg();
            Console.WriteLine("Utolsó elem törlése:");
            lista.Kiir();
            Console.WriteLine();

            lista.ListaFuzElol(99);
            Console.WriteLine("99 beszúrva a lista elejére:");
            lista.Kiir();
            Console.WriteLine();

            lista.ListaTorolMogul(lista.Fej);
            Console.WriteLine("FEJ mögötti elem törölve:");
            lista.Kiir();
            Console.WriteLine();

            lista.ListaFuzVeg(2000);
            Console.WriteLine("2000 beszúrva a lista végére:");
            lista.Kiir();
            Console.WriteLine();

            lista.ListaKereses(5, out hely);
            lista.ListaTorolMogul(hely);
            Console.WriteLine("Az 5 mögötti elem törölve:");
            lista.Kiir();
            Console.WriteLine();

            if (lista.ListaKereses(13, out hely))
                Console.WriteLine("A 13 érték megtalálható a(z) " + hely + ". indexen.");
            else
                Console.WriteLine("A 13 nincs a listában.");
            Console.WriteLine();

            if (lista.ListaKereses(5, out hely))
                Console.WriteLine("A 5 érték megtalálható az " + hely + ". indexen.");
            else
                Console.WriteLine("A 5 nincs a listában.");
        }
    }
}
