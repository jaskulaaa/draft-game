
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

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


class Gra
{   
    private List<Zawodnik> zawodnicy; 
    private List<Sklad1> sklad1;  // Lista do przechowywania zawodników
    public string nazwaGracza1;
    public string nazwaGracza2;
    

    public Gra()
    {
        zawodnicy = new List<Zawodnik>();
        WczytajZawodnikowZPliku("C:\\Users\\student\\Desktop\\mango\\draft-game\\draftkarne\\pilakrze.txt");
        Console.WriteLine("Gracz 1 podaj swoja nazwe");
        nazwaGracza1 = Console.ReadLine();
        Console.WriteLine("Gracz 2 podaj swoja nazwe");
        nazwaGracza2 = Console.ReadLine();
        Console.WriteLine("Rozpoczęcie gry pomiędzy " + nazwaGracza1 + " vs " + nazwaGracza2);
            
        sklad1 = new List<Sklad1>();
        RozpocznijDraft();

    }

    

    public void WczytajZawodnikowZPliku(string sciezkaPliku)
    {
        try
        {
           
            string[] linie = File.ReadAllLines(sciezkaPliku);

         
            foreach (string linia in linie)
            {
                
                string[] dane = linia.Split(',');

                if (dane.Length == 5)  // Sprawdź, czy linia zawiera poprawną liczbę danych
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
        Console.WriteLine("Rozpoczęcie budowy skłądu " + nazwaGracza1);
        
        Random losowe = new Random();
        string[] mozlwibramkarze = new string[3]; 
        string[] mozlwiobroncy = new string[6];
        string[] mozlwipomocnicy = new string[9];
        string[] mozlwinapastnicy = new string[6];
        for (int i = 0; i < mozlwibramkarze.Length; i++)
        {
            bool spr = false;

            mozlwibramkarze[i] = zawodnicy[losowe.Next(76,100)].imienazwisko;

            foreach(string bram in mozlwibramkarze)
            {
                if(mozlwibramkarze [i] == bram)
                {
                    spr = true;
                }
            }
            if (spr == true){

            }
           
          
        }
        for (int i = 0; i < mozlwiobroncy.Length; i++)
        {
            bool spr = false;

            mozlwiobroncy[i] = zawodnicy[losowe.Next(56, 75)].imienazwisko;

            foreach (string ob in mozlwiobroncy)
            {
                if (mozlwiobroncy[i] == ob)
                {
                    spr = true;
                }
            }
            if (spr == true)
            {

            }


        }
        for (int i = 0; i < mozlwipomocnicy.Length; i++)
        {
            bool spr = false;

            mozlwipomocnicy[i] = zawodnicy[losowe.Next(30, 55)].imienazwisko;

            foreach (string pom in mozlwipomocnicy)
            {
                if (mozlwipomocnicy[i] == pom)
                {
                    spr = true;
                }
            }
            if (spr == true)
            {

            }


        }
        for (int i = 0; i < mozlwinapastnicy.Length; i++)
        {
            bool spr = false;

            mozlwinapastnicy[i] = zawodnicy[losowe.Next(30, 55)].imienazwisko;

            foreach (string nap in mozlwinapastnicy)
            {
                if (mozlwinapastnicy[i] == nap)
                {
                    spr = true;
                }
            }
            if (spr == true)
            {

            }


        }
        Console.WriteLine("Wybierz jednego z 3 Bramkarzy podając numer od 1 do 3");
        foreach (string bram in mozlwibramkarze )
        {
            Console.WriteLine (bram);
        }
        int wybor = int.Parse(Console.ReadLine());
        if (wybor > 3 || wybor < 1)
        {
            Console.WriteLine("Prosze wybrac numer od 1 do 3");
        }
        else
        {
            string wybranybram = mozlwibramkarze[wybor-1];
            Console.WriteLine("wybrałeś Bramkarza: " + wybranybram);
        }
        Console.WriteLine("Wybierz dwoch z 6 oobroncow podając dwa różne numery od 1 do 6");
        foreach(string bram in mozlwiobroncy)
        {
            Console.WriteLine(bram);
        }
        int wybor1 = int.Parse(Console.ReadLine());
        int wybor2 = int.Parse(Console.ReadLine());
        if ((wybor1 > 6 || wybor1 < 1)|| (wybor2 > 6 || wybor2 < 1) || (wybor == wybor2))
        {
            Console.WriteLine("Prosze wybrac numer od 1 do 6 i nie podawaj dwa razy tego samegu numeru");
        }
        else
        {
            string wybranyobr = mozlwiobroncy[wybor1-1];
            string wybranyobr2 = mozlwiobroncy[wybor2-1];
            Console.WriteLine("wybrałeś Obrońcó: " + wybranyobr + " i " + wybranyobr2);
        }


        // Logika draftu, wybór zawodników przez obu graczy

    }

    public void SymulujMecz()
    {
        // Logika symulacji meczu, generowanie wyniku
      

        // Rzuty karne
        RzutyKarne();
    }

    public void RzutyKarne()
    {
        // Logika wykonywania rzutów karnych
        // Ostateczny wynik
    }

    public static void Main()
    {
       
        Gra gra = new Gra();
    }

}



