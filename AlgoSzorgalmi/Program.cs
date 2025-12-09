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
        const int MaxElem = 20;
        const int VegJel = -1;

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
            LancoltLista lista = new LancoltLista();
            lista.SzabadKezd();

            for (int i = 1; i <= 10; i++)
                lista.ListaFuzVeg(i);

            Console.WriteLine("1. Lista feltöltve 1-től 10-ig:");
            lista.Kiir();

            lista.ListaTorolElol();
            Console.WriteLine("2. Törlés elöl után:");
            lista.Kiir();

            lista.ListaTorolVeg();
            Console.WriteLine("3. Törlés hátul után:");
            lista.Kiir();

            if (lista.ListaKereses(5, out int hely))
                Console.WriteLine("A 5 érték megtalálható a(z) " + hely + ". indexen.");
            else
                Console.WriteLine("A 5 nincs a listában.");

            lista.ListaFuzElol(99);
            Console.WriteLine("4. 99 beszúrva a lista elejére:");
            lista.Kiir();

            lista.ListaFuzMoge(lista.Fej, 69);
            Console.WriteLine("5. 77 beszúrva a FEJ mögé (99 mögé):");
            lista.Kiir();

            lista.ListaTorolMogul(lista.Fej);
            Console.WriteLine("6. FEJ mögötti elem törölve:");
            lista.Kiir();
        }
    }
}
