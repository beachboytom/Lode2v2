namespace Lode2v2
{
    internal class Hrac
    {
        public string Jmeno;
        public string Role; // radar strelec
        public string[,] MojeMapa;
        public string[,] NepritelMapa;
        public string[,] Zasahy;
        public Team Tym;          
        public Team Nepritel;

        public Hrac(string jmeno, string role, Team mujTym, Team nepritel)
        {
            Jmeno = jmeno;
            Role = role;

            Tym = mujTym;
            Nepritel = nepritel;
            
            MojeMapa = mujTym.Mapa;
            NepritelMapa = nepritel.Mapa;
            Zasahy = mujTym.Zasahy;
        }
    }
}