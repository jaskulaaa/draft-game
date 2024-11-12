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
    

    private List<Zawodnik> zawodnicy;
    private List<Zawodnik> skladGracza1;
    private List<Zawodnik> skladGracza2;

    public Gra()
    {
        zawodnicy = new List<Zawodnik>
        {
            new Zawodnik("Jan Kowalski", 0, 90, 75, "Bramkarz"),
   
           
        };
    }

    public void RozpocznijDraft()
    {
        // Logika draftu, wybór zawodników przez obu graczy
    }

    public void SymulujMecz()
    {
        // Logika symulacji meczu, generowanie wyniku
        Console.WriteLine($"Wynik meczu: {wynikGracza1} - {wynikGracza2}");

        // Rzuty karne
        RzutyKarne();
    }

    public void RzutyKarne()
    {
        // Logika wykonywania rzutów karnych
    }
}



