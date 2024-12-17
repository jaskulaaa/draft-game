using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters;
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
    public int wynikgracza1;
    public int wynikgracza2;
    public Gra()
    {
        zawodnicy = new List<Zawodnik>();
        string currentDirectory = Directory.GetCurrentDirectory();
        string sciezkaPliku = Path.Combine(currentDirectory, "../../../pilakrze.txt");
        if (File.Exists(sciezkaPliku))
        {

        }
        else
        {

        }
        WczytajZawodnikowZPliku(sciezkaPliku);
        Console.WriteLine("Gracz 1 podaj swoją nazwę:");
        nazwaGracza1 = Console.ReadLine();
        Console.WriteLine("Gracz 2 podaj swoją nazwę:");
        nazwaGracza2 = Console.ReadLine();
        Console.WriteLine("Rozpoczęcie gry pomiędzy " + nazwaGracza1 + " vs " + nazwaGracza2);

        sklad1 = new List<Sklad1>();
        sklad2 = new List<Sklad2>();
        RozpocznijDraft();
        WyswietlBoisko();
       
        ktowygra();
       
        karne();
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
            WybierzZawodnikow(mozlwinapastnicy,"Wykonawca karnych",1,numerskladu);

            // Wyświetlenie składu

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
        string liniazielona = new string(' ', 100);
        liniazielona = new string(' ', 94 - nazwaGracza1.Length);
        Console.WriteLine("Skład " + nazwaGracza1 + liniazielona);
        foreach (var zawodnik in sklad1)
        {
             liniazielona = new string(' ', 70 - zawodnik.poz.Length - zawodnik.imienazwisko.Length);
            Console.WriteLine($"{zawodnik.poz}: {zawodnik.imienazwisko} (Atk: {zawodnik.atk}, Obr: {zawodnik.obr}, Kar: {zawodnik.kar})" + liniazielona);
           
        }
        liniazielona = new string(' ', 94 - nazwaGracza2.Length);
        Console.WriteLine(liniaBoiska);
        Console.WriteLine(liniaBoiska);
        Console.WriteLine("Skład " + nazwaGracza2 + liniazielona);
        foreach (var zawodnik in sklad2)
         {
            liniazielona = new string(' ', 70 - zawodnik.poz.Length - zawodnik.imienazwisko.Length);
            Console.WriteLine($"{zawodnik.poz}: {zawodnik.imienazwisko} (Atk: {zawodnik.atk}, Obr: {zawodnik.obr}, Kar: {zawodnik.kar})" + liniazielona);
                 
        }
        Console.WriteLine(liniaBoiska);
        Console.ResetColor();
    }
    public void ktowygra()

    {
        
        int wynikskladu1 = 0;
        foreach (var zawodnik in sklad1)
        {
            wynikskladu1 += zawodnik.atk;
            wynikskladu1 += zawodnik.obr;
            wynikskladu1 += zawodnik.kar;

        }
        int wynikskladu2 = 0;
        foreach (var zawodnik in sklad2)
        {
            wynikskladu2 += zawodnik.atk;
            wynikskladu2 += zawodnik.obr;
            wynikskladu2 += zawodnik.kar;   

        }
        Console.WriteLine("Gracz 1 ma wynik : " + wynikskladu1);
        Console.WriteLine("Gracz 2 ma wynik : " + wynikskladu2);
        if (wynikskladu1 > wynikskladu2 && wynikskladu1 < wynikskladu2 + 10)
        {
            Console.WriteLine("WYNIK WSTĘPNY 3:2");
            wynikgracza1 = 3;
            wynikgracza2 = 2;

        }
        if (wynikskladu1 > wynikskladu2 + 10)
        {
            Console.WriteLine("WYNIK WSTĘPNY 4:2");
            wynikgracza1 = 4;
            wynikgracza2 = 2;

        }
        if (wynikskladu1 < wynikskladu2 && wynikskladu2 < wynikskladu1 + 10)
        {
            Console.WriteLine("WYNIK WSTĘPNY 2:3");
            wynikgracza1 = 2;
            wynikgracza2 = 3;

        }
        if (wynikskladu1 + 10 < wynikskladu2 )
        {
            Console.WriteLine("WYNIK WSTĘPNY 2:4");
            wynikgracza1 = 2;
            wynikgracza2 = 4;

        }
        if (wynikskladu1 == wynikskladu2)
        {
            Console.WriteLine("WYNIK WSTĘPNY 2:2");
            wynikgracza1 = 2;
            wynikgracza2 = 2;

        }



    }
    public void karne()
    {
        int ilosc_kranych_gracza1 = wynikgracza1;
        int ilosc_kranych_gracza2 = wynikgracza2;
        int wykonawca1;
        int wykonawca2;
        int bramkarz1;
        int bramkarz2;
        int udanekarne = 0;
        int udanekarne2 = 0;
        bramkarz1 = sklad1[0].kar;
        bramkarz2 = sklad2[0].kar;
        wykonawca1 = sklad1[8].kar;
        wykonawca2 = sklad2[8].kar;
        Random losowanie;
        losowanie = new Random();
        Console.WriteLine("Najpierw karne gracza 1");
        for (int i = 0; i < ilosc_kranych_gracza1; i++)
        {
            Console.WriteLine("Wbierdz miejsce do oddania strzału");
            Console.WriteLine("  1  " + "2  " + "3  " + "\n  " + "4  " + "5  " + "6  " + "\n" + "  7  " + "8  " + "9  ");
            int strzal = int.Parse(Console.ReadLine());
            if (wykonawca1 + 5 > bramkarz2)
            {
               int czyudane = losowanie.Next(1,5);
                if (czyudane == 1 || czyudane == 2 || czyudane == 3 || czyudane == 4)
                {
                    if (bramkarz2 % 2 == 0)
                    {
                        int czyudane2 = losowanie.Next(1, 5);
                        if (strzal % 2 == 0)
                        {
                            if (czyudane2 == 1 || czyudane == 5)
                            {
                                Console.WriteLine("GOL!!!!!");
                                udanekarne += 1;
                            }
                            else
                            {
                                Console.WriteLine("Niestety orbonione");
                            }
                        }
                        else
                        {
                            if (czyudane2 == 1 || czyudane2 == 5 || czyudane2 == 4 || czyudane2 == 3)
                            {
                                Console.WriteLine("GOL!!!!!");
                                udanekarne += 1;
                            }
                            else
                            {
                                Console.WriteLine("Niestety orbonione");
                            }
                        }
                    }
                    else
                    {
                        int czyudane2 = losowanie.Next(1, 5);
                        if (strzal % 2 == 0)
                        {
                            if (czyudane2 == 1 || czyudane2 == 5 || czyudane2 == 4 || czyudane2 == 3)
                            {
                                Console.WriteLine("GOL!!!!!");
                                udanekarne += 1;
                            }
                            else
                            {
                                Console.WriteLine("Niestety orbonione");
                            }
                        }
                        else
                        {
                            if (czyudane2 == 1 || czyudane == 5)
                            {
                                Console.WriteLine("GOL!!!!!");
                                udanekarne += 1;
                            }
                            else
                            {
                                Console.WriteLine("Niestety orbonione");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("niestety twój strzelec nie trafił w bramkę");
                }
            }
            else
            {
                int czyudane = losowanie.Next(1, 5);
                if (czyudane == 1 || czyudane == 2 || czyudane == 3 || czyudane == 4)
                {
                    if (bramkarz2 % 2 == 0)
                    {
                        int czyudane2 = losowanie.Next(1, 5);
                        if (strzal % 2 == 0)
                        {
                            if (czyudane2 == 1 || czyudane == 5)
                            {
                                Console.WriteLine("GOL!!!!!");
                                udanekarne += 1;
                            }
                            else
                            {
                                Console.WriteLine("Niestety orbonione");
                            }
                        }
                        else
                        {
                            if (czyudane2 == 1 || czyudane2 == 5 || czyudane2 == 4 )
                            {
                                Console.WriteLine("GOL!!!!!");
                                udanekarne += 1;
                            }
                            else
                            {
                                Console.WriteLine("Niestety orbonione");
                            }
                        }
                    }
                    else
                    {
                        int czyudane2 = losowanie.Next(1, 5);
                        if (strzal % 2 == 0)
                        {
                            if (czyudane2 == 1 || czyudane2 == 5 || czyudane2 == 4)
                            {
                                Console.WriteLine("GOL!!!!!");
                                udanekarne += 1;
                            }
                            else
                            {
                                Console.WriteLine("Niestety orbonione");
                            }
                        }
                        else
                        {
                            if (czyudane2 == 1 || czyudane2 == 5)
                            {
                                Console.WriteLine("GOL!!!!!");
                                udanekarne += 1;
                            }
                            else
                            {
                                Console.WriteLine("Niestety orbonione");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("niestety twój strzelec nie trafił w bramkę");
                }
            }

        }
        Console.WriteLine("Teraz karne gracza 2");
        for (int i = 0; i < ilosc_kranych_gracza2; i++)
        {
            Console.WriteLine("Wbierdz miejsce do oddania strzału");
            Console.WriteLine("  1  " + " 2  " + " 3  " + "\n" + "4  " + "5  " + "6  " + "\n" + "  7  " + " 8  " + " 9  ");
            int strzal = int.Parse(Console.ReadLine());
            if (wykonawca2 + 5 > bramkarz1)
            {
                int czyudane = losowanie.Next(1, 5);
                if (czyudane == 1 || czyudane == 2 || czyudane == 3 || czyudane == 4)
                {
                    if (bramkarz2 % 2 == 0)
                    {
                        int czyudane2 = losowanie.Next(1, 5);
                        if (strzal % 2 == 0)
                        {
                            if (czyudane2 == 1 || czyudane == 5)
                            {
                                Console.WriteLine("GOL!!!!!");
                                udanekarne += 1;
                            }
                            else
                            {
                                Console.WriteLine("Niestety orbonione");
                            }
                        }
                        else
                        {
                            if (czyudane2 == 1 || czyudane2 == 5 || czyudane2 == 4 || czyudane2 == 3)
                            {
                                Console.WriteLine("GOL!!!!!");
                                udanekarne += 1;
                            }
                            else
                            {
                                Console.WriteLine("Niestety orbonione");
                            }
                        }
                    }
                    else
                    {
                        int czyudane2 = losowanie.Next(1, 5);
                        if (strzal % 2 == 0)
                        {
                            if (czyudane2 == 1 || czyudane2 == 5 || czyudane2 == 4 || czyudane2 == 3)
                            {
                                Console.WriteLine("GOL!!!!!");
                                udanekarne += 1;
                            }
                            else
                            {
                                Console.WriteLine("Niestety orbonione");
                            }
                        }
                        else
                        {
                            if (czyudane2 == 1 || czyudane == 5)
                            {
                                Console.WriteLine("GOL!!!!!");
                                udanekarne += 1;
                            }
                            else
                            {
                                Console.WriteLine("Niestety orbonione");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("niestety twój strzelec nie trafił w bramkę");
                }
            }
            else
            {
                int czyudane = losowanie.Next(1, 5);
                if (czyudane == 1 || czyudane == 2 || czyudane == 3 || czyudane == 4)
                {
                    if (bramkarz2 % 2 == 0)
                    {
                        int czyudane2 = losowanie.Next(1, 5);
                        if (strzal % 2 == 0)
                        {
                            if (czyudane2 == 1 || czyudane == 5)
                            {
                                Console.WriteLine("GOL!!!!!");
                                udanekarne += 1;
                            }
                            else
                            {
                                Console.WriteLine("Niestety orbonione");
                            }
                        }
                        else
                        {
                            if (czyudane2 == 1 || czyudane2 == 5 || czyudane2 == 4)
                            {
                                Console.WriteLine("GOL!!!!!");
                                udanekarne += 1;
                            }
                            else
                            {
                                Console.WriteLine("Niestety orbonione");
                            }
                        }
                    }
                    else
                    {
                        int czyudane2 = losowanie.Next(1, 5);
                        if (strzal % 2 == 0)
                        {
                            if (czyudane2 == 1 || czyudane2 == 5 || czyudane2 == 4)
                            {
                                Console.WriteLine("GOL!!!!!");
                                udanekarne += 1;
                            }
                            else
                            {
                                Console.WriteLine("Niestety orbonione");
                            }
                        }
                        else
                        {
                            if (czyudane2 == 1 || czyudane2 == 5)
                            {
                                Console.WriteLine("GOL!!!!!");
                                udanekarne += 1;
                            }
                            else
                            {
                                Console.WriteLine("Niestety orbonione");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("niestety twój strzelec nie trafił w bramkę");
                }
            }

        }
        Console.Clear();
        Console.SetWindowSize(120, 40); // Ustawia szerokość i wysokość okna konsoli

        Console.WriteLine("KONCOWY WYNIK " + udanekarne + " : " + udanekarne2);
       

       
        Console.WriteLine("Dziekujemy za GRE");

    }




    public static void Main()
    {
        Gra gra = new Gra();


    }
}

