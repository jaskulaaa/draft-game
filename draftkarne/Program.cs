
using System;
using System.Collections.Generic;
using System.IO;

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

class Gra
{
    private List<Zawodnik> zawodnicy;  // Lista do przechowywania zawodników

    public Gra()
    {
        zawodnicy = new List<Zawodnik>();
        WczytajZawodnikowZPliku(".zawodnicytxt");
    }

    public void WczytajZawodnikowZPliku(string sciezkaPliku)
    {
        try
        {
            // Otwórz plik do odczytu
            string[] linie = File.ReadAllLines(sciezkaPliku);

            // Przetwarzaj każdą linię
            foreach (string linia in linie)
            {
                // Podziel linię na części, zakładając, że dane są oddzielone przecinkami
                string[] dane = linia.Split(',');

                if (dane.Length == 5)  // Sprawdź, czy linia zawiera poprawną liczbę danych
                {
                    string imienazwisko = dane[0].Trim();
                    int atk = int.Parse(dane[1].Trim());
                    int obr = int.Parse(dane[2].Trim());
                    int kar = int.Parse(dane[3].Trim());
                    string poz = dane[4].Trim();

                    // Utwórz obiekt Zawodnik i dodaj do listy
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
        // Logika draftu, wybór zawodników przez obu graczy
    }

    public void SymulujMecz()
    {
        // Logika symulacji meczu, generowanie wyniku
        //  Console.WriteLine($"Wynik meczu: {wynikGracza1} - {wynikGracza2}");

        // Rzuty karne
        RzutyKarne();
    }

    public void RzutyKarne()
    {
        // Logika wykonywania rzutów karnych
        // Ostateczny wynik
    }
}

