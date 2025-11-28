using System;

namespace Lode2v2
{
    internal class Program
    {
        static void Main(string[] args)
        {
          
            Console.Clear();
            Console.WriteLine("=======================================");
            Console.WriteLine("             LODĚ 2v2");
            Console.WriteLine("=======================================");
            
            Console.WriteLine();
            Console.WriteLine("HRA 2v2 – dva týmy, každý má Radar a Střelce.");
            Console.WriteLine();
            Console.WriteLine("PRAVIDLA:");
            Console.WriteLine("- Každý tým má jednu společnou mapu 10x10.");
            Console.WriteLine("- Na mapě jsou 4 lodě (1x2, 1x3, 1x3, 1x4).");
            Console.WriteLine("- Náhodné rozmístění lodí.");
            Console.WriteLine();
            Console.WriteLine("ROLE:");
            Console.WriteLine("Radar → skenuje oblast 3x3.");
            Console.WriteLine("Střelec → střílí na souřadnici.");
            Console.WriteLine();
            Console.WriteLine("POŘADÍ TAHŮ:");
            Console.WriteLine("A – Radar");
            Console.WriteLine("A – Střelec");
            Console.WriteLine("B – Radar");
            Console.WriteLine("B – Střelec");
            Console.WriteLine();
            Console.WriteLine("Stiskni ENTER pro pokračování...");
            Console.ReadLine();
            

    
            Team teamA = new Team("A");
            Team teamB = new Team("B");

            UdelejMapu(teamA.Mapa);
            UdelejMapu(teamB.Mapa);
            UdelejMapu(teamA.Zasahy);
            UdelejMapu(teamB.Zasahy);

            RozmistitLode(teamA.Mapa);
            RozmistitLode(teamB.Mapa);

            Hrac A_radar = new Hrac("A - Radar", "radar", teamA, teamB);
            Hrac A_strelec = new Hrac("A - Střelec", "strelec", teamA, teamB);

            Hrac B_radar = new Hrac("B - Radar", "radar", teamB, teamA);
            Hrac B_strelec = new Hrac("B - Střelec", "strelec", teamB, teamA);
            Hrac[] poradi = { A_radar, A_strelec, B_radar, B_strelec };

            int Tah = 0;
            bool konecHry = false;


            
            do
            {
                Console.Clear();
                Hrac aktualni = poradi[Tah];

                Console.WriteLine("===== LODĚ 2v2 =====");
                Console.WriteLine("Na tahu je: " + aktualni.Jmeno);
                Console.WriteLine("Role: " + aktualni.Role );


              
                Console.WriteLine("Mapa nepřítele (jen zásahy):");
                VypisMapu(aktualni.Zasahy);
                Console.WriteLine();
                
                Console.WriteLine("1. Ukázat moji mapu");
                Console.WriteLine("2. Pokračovat");
                Console.Write("Vyber možnost: ");
                string volba = Console.ReadLine();

                if (volba == "1")
                {
                    Console.Clear();
                    Console.WriteLine("Tvoje mapa (vlastní lodě):\n");
                    VypisMapu(aktualni.MojeMapa);
                    Console.WriteLine("Stiskni Enter");
                    Console.ReadLine();
                    
                    continue; //tohle vymyslel chatgpt 
                    

                }else if (volba == "2")
                {
                    if (aktualni.Role == "radar")
                    {
                       
                        Console.WriteLine("RADAR – zadej souřadnici pro scan:");

                        Console.Write("X: ");
                        int x = int.Parse(Console.ReadLine()) - 1;

                        Console.Write("Y: ");
                        int y = int.Parse(Console.ReadLine()) - 1;

                        Console.WriteLine();

                        Radar(aktualni.Nepritel.Mapa, x, y);

                        Console.WriteLine("Stiskni ENTER…");
                        Console.ReadLine();
                    }
                    
                    if (aktualni.Role == "strelec")
                    {
                        bool striliDale = true;

                        while (striliDale)
                        {
                          
                            Console.WriteLine("STŘELEC – zadej souřadnici:");

                            Console.Write("X: ");
                            int x = int.Parse(Console.ReadLine()) -1;

                            Console.Write("Y: ");
                            int y = int.Parse(Console.ReadLine()) -1;

                            Strelec(aktualni, x, y);

                            // Po střelbě zjistíme, co se stalo:
                            if (aktualni.Zasahy[y, x] == "X")
                            {
                                Console.WriteLine("Pokračuj – byl to zásah!");
                                striliDale = true;
                            }
                            else
                            {
                                Console.WriteLine("Mimo – konec tahu.");
                                striliDale = false;
                            }

                            Console.WriteLine("Enter...");
                            Console.ReadLine();
                        }
                    }
                    
                }

                if (teamA.Zivoty <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Tým B vyhrál!");
                    Console.ResetColor();
                    konecHry = true;
                }

                if (teamB.Zivoty <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Tým A vyhrál!");
                    Console.ResetColor();
                    konecHry = true;
                }

                if (konecHry)
                {
                    Console.WriteLine("Stiskni Enter pro ukončení...");
                    Console.ReadLine();
                    break;   
                }

              
                
                Tah++;
                if (Tah > 3)
                {
                    Tah = 0;
                }
                   

            } while (!konecHry);
        }
        
        static void UdelejMapu(string[,] mapa)
        {
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    mapa[y, x] = "_";
                }
            }
        }

        static void VypisMapu(string[,] mapa)
        {
            Console.WriteLine();
            Console.Write("  ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("1 2 3 4 5 6 7 8 9 10 ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("X");
            Console.ResetColor();
           
            for (int i = 0; i < 10; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(i + 1 + "  ");
                Console.ResetColor();
                
                for (int j = 0; j < 10; j++)
                {
                    string znak = mapa[i, j];

                    
                    if (znak == "X")
                        Console.ForegroundColor = ConsoleColor.Red;       
                    else if (znak == "O")
                        Console.ForegroundColor = ConsoleColor.Blue;       
                    else if (znak == "2" || znak == "3" || znak == "4")
                        Console.ForegroundColor = ConsoleColor.Yellow;     
                    else
                        Console.ForegroundColor = ConsoleColor.Cyan;                               

                    Console.Write(znak + " ");
                    Console.ResetColor();
                    
                }
                Console.WriteLine();
            }
            
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Y");
            Console.ResetColor();
        }
        
        static void Radar(string[,] mapa, int x, int y)
        {
            bool nasel = false;

            
            for (int i = 0; i < 3; i++)      
            {
                for (int j = 0; j < 3; j++)  
                {
                    int kontrolaY = y + (i - 1);
                    int kontrolaX = x + (j - 1);

                    
                    if (kontrolaX >= 0 && kontrolaX < 10 && kontrolaY >= 0 && kontrolaY < 10)
                    {
                        
                        if (mapa[kontrolaY, kontrolaX] == "2" || mapa[kontrolaY, kontrolaX] == "3" || mapa[kontrolaY, kontrolaX] == "4")
                        {
                            nasel = true;
                        }
                    }
                }
            }

            if (nasel)
            {
                Console.WriteLine("RADAR: V oblasti JE loď.");
            }
            else
            {
                Console.WriteLine("RADAR: V oblasti NENÍ loď.");
            }
               
           
               
        }
        
        static void Strelec(Hrac hrac, int x, int y)
        {
            if (hrac.Tym.Zasahy[y, x] == "X" || hrac.Tym.Zasahy[y, x] == "O")
            {
                Console.WriteLine("Sem jsi už střílel!");
                return;
            }

            if (hrac.Nepritel.Mapa[y, x] == "2" || 
                hrac.Nepritel.Mapa[y, x] == "3" || 
                hrac.Nepritel.Mapa[y, x] == "4")
            {
                Console.WriteLine("ZÁSÁH!");
                hrac.Tym.Zasahy[y, x] = "X";
                hrac.Nepritel.Zivoty--;
            }
            else
            {
                Console.WriteLine("MIMO");
                hrac.Tym.Zasahy[y, x] = "O";
            }
        }

        
        static void RozmistitLode(string[,] mapa)
        {
            Random rnd = new Random();

            int[] lodDelky = { 2, 3, 3, 4 };

            foreach (int delka in lodDelky)
            {
                bool umisteno = false;

                while (!umisteno)
                {
                    int orientace = rnd.Next(0, 2);  // 0 = vodorovně, 1 = svisle
                    int startX = rnd.Next(0, 10);
                    int startY = rnd.Next(0, 10);

                    bool vejde = true;

           
                    if (orientace == 0)
                    {
                        if (startX + delka > 10) vejde = false;
                    }
                    else
                    {
                        if (startY + delka > 10) vejde = false;
                    }

                    if (vejde == false)
                        continue;

                    
                    bool volno = true;

                    if (orientace == 0)
                    {
                        for (int i = 0; i < delka; i++)
                        {
                            if (mapa[startY, startX + i] != "_")
                                volno = false;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < delka; i++)
                        {
                            if (mapa[startY + i, startX] != "_")
                                volno = false;
                        }
                    }

                    if (volno == false)
                    {
                        continue;

                    }
                        
                   
                    if (orientace == 0)
                    {
                        for (int i = 0; i < delka; i++)
                        {
                            mapa[startY, startX + i] = delka.ToString();
                        }
                            
                    }
                    else
                    {
                        for (int i = 0; i < delka; i++)
                            mapa[startY + i, startX] = delka.ToString();
                    }

                    umisteno = true;
                }
            }
        }
    }
}