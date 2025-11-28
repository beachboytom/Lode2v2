namespace Lode2v2;

public class Team
{
    public string Jmeno;             
    public string[,] Mapa;           
    public string[,] Zasahy;         
    public int Zivoty;               

    public Team(string name)
    {
        Jmeno = name;
        Mapa = new string[10, 10];
        Zasahy = new string[10, 10];
        Zivoty = 12;
    }
}