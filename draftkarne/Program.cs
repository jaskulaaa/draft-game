using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

public class Zawodnik
{
    public string imienazwisko;
    public int atk;
    public int obr;
    public int kar;
    public string poz;

    public Zawodnik(string nickname, int atack, int def, int penalty, string position)
    {
        imienazwisko = nickname;
        atk = atack;
        obr = def;
        kar = penalty;
        poz = position;
    }
}

public class Sklad1
{
    public string imienazwisko;
    public int atk;
    public int obr;
    public int kar;
    public string poz;

    public Sklad1(string nickname, int atack, int def, int penalty, string position)
    {
        imienazwisko = nickname;
        atk = atack;
        obr = def;
        kar = penalty;
        poz = position;
    }
}
public class Sklad2
{
    public string imienazwisko;
    public int atk;
    public int obr;
    public int kar;
    public string poz;

    public Sklad2(string nickname, int atack, int def, int penalty, string position)
    {
        imienazwisko = nickname;
        atk = atack;
        obr = def;
        kar = penalty;
        poz = position;
    }
}

class Gra
{
    private List<Zawodnik> zawodnicy;
    private List<Sklad1> sklad1;
    private List<Sklad2> sklad2;  // Lista składu gracza
    public string nazwaGracza1;
    public string nazwaGracza2;

    public Gra()
    {
        zawodnicy = new List<Zawodnik>();
        WczytajZawodnikowZPliku("C:\\Users\\kacpe\\Desktop\\draft\\draft-game\\draftkarne\\pilakrze.txt");
        Console.WriteLine("Gracz 1 podaj swoją nazwę:");
        nazwaGracza1 = Console.ReadLine();
        Console.WriteLine("Gracz 2 podaj swoją nazwę:");
        nazwaGracza2 = Console.ReadLine();
        Console.WriteLine("Rozpoczęcie gry pomiędzy " + nazwaGracza1 + " vs " + nazwaGracza2);

        sklad1 = new List<Sklad1>();
        sklad2 = new List<Sklad2>();
        RozpocznijDraft();
        WyswietlBoisko();
    }

    public void WczytajZawodnikowZPliku(string sciezkaPliku)
    {
        try
        {
            string[] linie = File.ReadAllLines(sciezkaPliku);

            foreach (string linia in linie)
            {
                string[] dane = linia.Split(',');

                if (dane.Length == 5)
                {
                    string imienazwisko = dane[0].Trim();
                    int atk = int.Parse(dane[1].Trim());
                    int obr = int.Parse(dane[2].Trim());
                    int kar = int.Parse(dane[3].Trim());
                    string poz = dane[4].Trim();

                    Zawodnik zawodnik = new Zawodnik(imienazwisko, atk, obr, kar, poz);
                    zawodnicy.Add(zawodnik);
                }
            }

            Console.WriteLine("Zawodnicy zostali wczytani z pliku.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Wystąpił błąd podczas wczytywania pliku: {e.Message}");
        }
    }

    public void RozpocznijDraft()
    {
        int il = 1;
        do
        {


            Random losowe = new Random();
            string[] mozlwibramkarze = new string[3];
            string[] mozlwiobroncy = new string[6];
            string[] mozlwipomocnicy = new string[9];
            string[] mozlwinapastnicy = new string[6];
            int numerskladu = il;
            if (numerskladu == 1)
            {
                Console.WriteLine("Rozpoczęcie budowy składu " + nazwaGracza1);
            }
            if (numerskladu == 2)
            {
                Console.WriteLine("Rozpoczęcie budowy składu " + nazwaGracza2);
            }
            // Losowanie Bramkarzy
            LosujZawodnikow(mozlwibramkarze, 76, 100, losowe);

            // Losowanie Obrońców
            LosujZawodnikow(mozlwiobroncy, 56, 75, losowe);

            // Losowanie Pomocników
            LosujZawodnikow(mozlwipomocnicy, 31, 55, losowe);

            // Losowanie Napastników
            LosujZawodnikow(mozlwinapastnicy, 1, 30, losowe);

            // Wybór zawodników
            WybierzZawodnikow(mozlwibramkarze, "Bramkarz", 1, numerskladu);
            WybierzZawodnikow(mozlwiobroncy, "Obrońca", 2, numerskladu);
            WybierzZawodnikow(mozlwipomocnicy, "Pomocnik", 3, numerskladu);
            WybierzZawodnikow(mozlwinapastnicy, "Napastnik", 2, numerskladu);

            // Wyświetlenie składu

            Console.WriteLine("Twój skład:");
            if (il == 1)
            {
                foreach (var zawodnik in sklad1)
                {
                    Console.WriteLine($"{zawodnik.poz}: {zawodnik.imienazwisko} (Atk: {zawodnik.atk}, Obr: {zawodnik.obr}, Kar: {zawodnik.kar})");

                }
            }
            if (il == 2)
            {
                foreach (var zawodnik in sklad2)
                {
                    Console.WriteLine($"{zawodnik.poz}: {zawodnik.imienazwisko} (Atk: {zawodnik.atk}, Obr: {zawodnik.obr}, Kar: {zawodnik.kar})");

                }
            }
            il++;
        } while (il <= 2);

    }

    private void LosujZawodnikow(string[] lista, int minmalnyindex, int maksymlanyindex, Random losowe)
    {
        for (int i = 0; i < lista.Length; i++)
        {
            bool spr;
            do
            {
                spr = false;
                lista[i] = zawodnicy[losowe.Next(minmalnyindex, maksymlanyindex)].imienazwisko;

                for (int j = 0; j < i; j++)
                {
                    if (lista[i] == lista[j])
                    {
                        spr = true;
                        break;
                    }
                }
            } while (spr);
        }
    }

    private void WybierzZawodnikow(string[] lista, string pozycja, int liczbaDoWyboru, int numerskladu)
    {
        Console.WriteLine($"Wybierz {liczbaDoWyboru} z {lista.Length} {pozycja.ToLower()}(ów) podając odpowiednie numery:");

        for (int i = 0; i < lista.Length; i++)
        {
            Console.WriteLine($"{i + 1}: {lista[i]}");
        }

        List<int> wybrane = new List<int>();
        while (wybrane.Count < liczbaDoWyboru)
        {
            int wybor = int.Parse(Console.ReadLine());
            if (wybor < 1 || wybor > lista.Length || wybrane.Contains(wybor))
            {
                Console.WriteLine("Niepoprawny wybór! Spróbuj ponownie.");
            }
            else
            {
                wybrane.Add(wybor);
                string wybranyZawodnik = lista[wybor - 1];
                Zawodnik zawodnik = zawodnicy.Find(z => z.imienazwisko == wybranyZawodnik);
                if (zawodnik != null)
                {
                    if (numerskladu == 1)
                    {
                        sklad1.Add(new Sklad1(zawodnik.imienazwisko, zawodnik.atk, zawodnik.obr, zawodnik.kar, pozycja));
                    }
                    if (numerskladu == 2)
                    {
                        sklad2.Add(new Sklad2(zawodnik.imienazwisko, zawodnik.atk, zawodnik.obr, zawodnik.kar, pozycja));
                    }
                    Console.WriteLine($"{zawodnik.imienazwisko} został dodany do Twojego składu!");
                }
            }
        }
    }
    public void WyswietlBoisko()
    {
        Console.Clear();

        Console.BackgroundColor = ConsoleColor.Green;
        Console.ForegroundColor = ConsoleColor.White;

        string liniaBoiska = new string('-', 100);
        Console.WriteLine(liniaBoiska);
        Console.WriteLine("|");


        Console.WriteLine(liniaBoiska);


        Console.ResetColor();
    }




    public static void Main()
    {
        Gra gra = new Gra();


    }
}

