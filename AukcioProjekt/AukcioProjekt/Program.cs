using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AukcioProjekt
{
    // -- 1/B: Készíts egy osztályt Festmeny névvel, az alábbi UML diagram alapján.
    class Festmeny
    {
        private string cim;
        private string festo;
        private string stilus;
        private int licitekSzama = 0;
        private int legmagasabbLicit = 100;
        private DateTime legmagasabbLicitIdeje = DateTime.Now;
        private bool elkelt = false;
        public Festmeny(string cim, string festo, string stilus)
        {
            this.cim = cim;
            this.festo = festo;
            this.stilus = stilus;
        }
        public string getCim()
        {
            return cim;
        }
        public string getFesto()
        {
            return festo;
        }
        public string getStilus()
        {
            return stilus;
        }
        public int getLicitekSzama()
        {
            return licitekSzama;
        }
        public int getLegmagasabbLicit()
        {
            return legmagasabbLicit;
        }
        public bool Elkelt()
        {
            return elkelt;
        }
        public void Licit()
        {
            // -- 1/C.6: Ha már volt licit, akkor a licit eljárás, ezt a metódust hívja meg 10%-os mértékkel.
            Licit(10);
        }
        public void Licit(int mertek)
        {
            // -- 1/C.5: A Licit(merek:int) eljárás működése hasonló legyen csak 10% helyett a megadott %-kal növelje a licitet. A paraméter csak 10 és 100 közötti szám legyen. Hibás paraméter esetén, konzolra hibaüzenetet írjon ki, és ne történjen licit.
            while (elkelt == false)
            {
                // -- 1/C.1: Ha a festmény már elkelt akkor írja ki hibaüzenetet a konzolra és ne történjen semmi.
                if (elkelt == true)
                {
                    Console.WriteLine("A festmény elkelt!");
                    break;
                }
                // -- 1/C.2: Ha még nem volt licit akkor a kezdeti licit értékével (100$) licitáljon, majd növelje a licitek ◦ számát eggyel, és állítsa be a legutolsó licit idejét az aktuális időpontra.
                if (mertek >= 10 && mertek <= 100)
                {
                    if (licitekSzama == 0)
                    {
                        legmagasabbLicit = 100;
                        licitekSzama++;
                        legmagasabbLicitIdeje = DateTime.Now;
                    }
                    else
                    {
                        legmagasabbLicit = legmagasabbLicit + legmagasabbLicit / 100 * mertek;
                        licitekSzama++;
                        legmagasabbLicitIdeje = DateTime.Now;
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("Hibás adat, nem érvenyes licit");
                    break;
                }
            }
        }
        // -- 1/D: Bővítsd ki az osztályt egy Kiir() függvénnyel, ami visszaadja az adatokat az alábbi formában:  festo : cim (stilus)  elkelt / nem kelt el  legmagasabbLicit $ - legutolsoLicitIdeje (összesen: licitekSzama db).
        public void Kiir()
        {
            if (elkelt == true)
            {
                Console.WriteLine($"{festo} : {cim} ({stilus})\n{legmagasabbLicit} $ - {legmagasabbLicitIdeje} (összesen: {licitekSzama} db)");
            }
            else
            {
                Console.WriteLine($"{festo} : {cim} ({stilus})\nnem kelt el\n{legmagasabbLicit} $ - {legmagasabbLicitIdeje} (összesen: {licitekSzama} db)");
            }
        }
        public DateTime getlegmagasabbLicitIdeje()
        {
            return legmagasabbLicitIdeje;
        }
        public void eladva()
        {
            elkelt = true; ;
        }

    }

    class Program
    {
        // -- 2/A: Vegyél fel egy listát legalább 2 festménnyel.
        static List<Festmeny> festmenyek = new List<Festmeny>();
        static void Beolvas()
        {
            // -- 2/B+: Bálint Ferenc is el szeretné adni a festményeit az aukción. Ezeket afestmenyek.csv állomány tartalmazza az alábbi formában: festo; cim; stilus Olvasd be a fájlt és a tartalmát add hozzá a listához!"
            string fajl = "festmenyek.csv";
            StreamReader sr = null;
            try
            {
                using (sr = new StreamReader(fajl, Encoding.UTF8))
                {
                    string[] sor = null;
                    while (!sr.EndOfStream)
                    {
                        sor = sr.ReadLine().Split(';');
                        festmenyek.Add(new Festmeny(sor[1], sor[0], sor[2]));
                    }
                }

            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                    sr.Dispose();
                }
            }
        }
        // -- 2/B: Kérj be a felhasználótól egy darabszámot, majd ugyanennyi új festmény adatait, amit adj hozzá a listához.
        static void Ujadatok()
        {
            int ell = 0;
            while (ell == 0)
            {
                try
                {
                    Console.Write($"Adja meg mennyi új festményt szeretne hozzáadni : ");
                    int ujfestmeny = Convert.ToInt32(Console.ReadLine());
                    if (ujfestmeny > 0)
                    {
                        for (int i = 0; i < ujfestmeny; i++)
                        {
                            Console.Write("Festő: ");
                            string festo = Console.ReadLine();
                            Console.Write("Festmény címe: ");
                            string festmeny = Console.ReadLine();
                            Console.Write("Stílus: ");
                            string stilus = Console.ReadLine();
                            festmenyek.Add(new Festmeny(festmeny, festo, stilus));
                        }
                    }
                    Console.WriteLine();
                    ell = 1;
                }
                catch
                {
                    Console.WriteLine("Hibás adat!");
                }
            }
        }
        // -- 2/C: A program véletlenszerűen licitáljon a képekre 20 alkalommal.
        static void Geplicit()
        {
            Console.WriteLine("A program véletlenszerűen licitál 20 alkalommal! (KÉREM VÁRJON!)");
            for (int i = 0; i < 20; i++)
            {
                Random rnd = new Random();
                System.Threading.Thread.Sleep(200);
                festmenyek[rnd.Next(0, festmenyek.Count)].Licit(rnd.Next(10, 101));
            }
            Console.WriteLine("A program végzett a licitálással!");
        }
        // -- 2/D: A felhasználó konzolon is licitálhasson a festményekre.
        static void FelhasznaloLicit()
        {
            int ell = 0;
            int index = 0;
            int sorszam = 1;
            string mertek;
            // -- 2/D/3: Nem szám beírása esetén a program hibaüzenetet írjon ki majd álljon le.
            try
            {
                Console.WriteLine("Festmények:");
                for (int i = 0; i < festmenyek.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. festmény: {festmenyek[i].getFesto()}: {festmenyek[i].getCim()} ({festmenyek[i].getStilus()})");
                }
                while (sorszam != 0)
                {
                    while (ell == 0)
                    {
                        // -- 2/D/1: A felhasználó először a festmény sorszámát adja meg. A sorszám megadásánál használjon index eltolást. (Ha a felhasználó 3 - at ad meg akkor a lista 2.elemére licitál). 0 megadásával lépjen ki a felhasználó a licitálásból.
                        Console.WriteLine();
                        System.Threading.Thread.Sleep(100);
                        Console.WriteLine($"Adja meg melyik képre szeretne licitálni: \n0: kilépés\n1-{festmenyek.Count} festmények");

                        Console.Write("Választott kép sorszáma: ");
                        sorszam = Convert.ToInt32(Console.ReadLine());
                        if (sorszam != 0)
                        {
                            // -- 2/D/2: Nem létező sorszám esetén a program hibaüzenetet írjon ki, majd kérjen be új sorszámot.
                            if (sorszam < festmenyek.Count + 1)
                            {
                                index = sorszam - 1;
                                ell = 1;
                            }
                            else
                            {
                                Console.WriteLine("Nem létező adat!");
                            }
                        }
                        else
                        {
                            ell = 1;
                            break;
                        }
                        // -- 2/D/4: Ha a festmény elkelt, akkor hibaüzenetet írjon ki, majd kérjen be új sorszámot.
                        if (festmenyek[index].Elkelt() == true)
                        {
                            ell = 0;
                            Console.WriteLine("A festmény elkelt!");
                        }
                        // -- 2/D/8: A sorszám megadása után, ha az adott festményre több mint 2 perce érkezett utoljára licit akkor állítsa be elkeltre, majd hibaüzenetet írjon ki, majd kérjen be új sorszámot
                        if ((DateTime.Now - festmenyek[index].getlegmagasabbLicitIdeje()).TotalMinutes > 2 && (festmenyek[index].getlegmagasabbLicitIdeje()).Year > 2000)
                        {
                            festmenyek[index].Elkelt().Equals(true);
                            ell = 0;
                            Console.WriteLine("A festmény elkelt!");
                        }
                        Console.WriteLine();
                    }
                    if (sorszam != 0)
                    {
                        // -- 2/D/5: A sorszám megadása után kérje be, hogy milyen mértékkel szeretne licitálni a felhasználó.
                        Console.WriteLine("Adja meg milyen mértékkel szeretne licitálni: \nenter: automatikusan +10%-os licit\negyéb esetben megadható érték: 10-100");
                        Console.Write("Mérték:");
                        mertek = Console.ReadLine();
                        try
                        {
                            festmenyek[index].Licit(Convert.ToInt32(mertek));
                        }
                        catch
                        {
                            // -- 2/D/6: A megadás ne legyen kötelező, ha a felhasználó egyből entert üt le akkor az alap 10 % -os licittel lehessen licitálni.
                            if (mertek == "")
                            {
                                festmenyek[index].Licit();
                                Console.WriteLine("Automatikus 10%-os licit megtörtént");
                            }
                            // -- 2/D/7: Ha a felhasználó nem számot ad meg akkor a program hibaüzenettel álljon le.
                            else
                            {
                                Console.WriteLine("Hibás adat!");
                                vege = 1;
                                break;
                            }
                        }
                        ell = 0;
                        Console.WriteLine();
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch
            {
                Console.WriteLine("Hibás adat!");
                vege = 1;
            }
            if (vege == 0)
            {
                Console.WriteLine();
                Kiiratas();
            }
        }
        static void Kiiratas()
        {
            // -- 2/D/9: Miután a felhasználó befejezte a licitálást, az összes olyan festmény, amire érkezett licit legyen elkelt.
            for (int i = 0; i < festmenyek.Count; i++)
            {
                if ((festmenyek[i].getLicitekSzama()) > 0)
                {
                    festmenyek[i].eladva();
                }
            }
            // -- 2/E: Írd ki a festményeket a konzolra!
            for (int i = 0; i < festmenyek.Count; i++)
            {
                Console.WriteLine($"{i + 1}. festmény:");
                festmenyek[i].Kiir();
                Console.WriteLine();
            }
        }
        // -- 3:
        static void UtolsoFeladat()
        {
            if (vege == 0)
            {
                // -- 3/A: Keresd meg a legdrágábban elkelt festményt, majd az adatait konzolra.
                int ertek = festmenyek[0].getLegmagasabbLicit();
                int hely = 0;
                for (int i = 1; i < festmenyek.Count; i++)
                {
                    if (festmenyek[i].getLegmagasabbLicit() > ertek)
                    {
                        ertek = festmenyek[i].getLegmagasabbLicit();
                        hely = i;
                    }
                }
                Console.WriteLine("3/A feladat:");
                Console.WriteLine($"A legdrágábban elkelt festmény: {festmenyek[hely].getFesto()}: {festmenyek[hely].getCim()} ({festmenyek[hely].getStilus()}) - {ertek} $");
                Console.WriteLine();

                // -- 3/B: Döntsd el, hogy van-e olyan festmény, amelyre 10-nél több alkalommal licitáltak.
                string vane = "Nincs";
                for (int i = 0; i < festmenyek.Count; i++)
                {
                    if (festmenyek[i].getLicitekSzama() > 10)
                    {
                        vane = "Van";
                        i = festmenyek.Count;
                    }
                    else
                    {
                        vane = "Nincs";
                    }
                }
                Console.WriteLine("3/B feladat:");
                Console.WriteLine($"{vane} olyan festmény amelyre 10 -nél többen licitáltak!");
                Console.WriteLine();

                // -- 3/C: Számold meg, hogy hány olyan festmény van, amely nem kelt el.
                int elkelte = 0;
                for (int i = 0; i < festmenyek.Count; i++)
                {
                    if (festmenyek[i].Elkelt() == false)
                    {
                        elkelte = elkelte + 1;
                    }
                }
                Console.WriteLine("3/C feladat:");
                Console.WriteLine($"{elkelte} db olyan festmény van ami nem kelt el!");
                Console.WriteLine();
                // -- 3/D: Rendezd át a listát a Legmagasabb Licit szerint csökkenő sorrendben, majd írd ki újra a festményeket.
                Console.WriteLine("3/D feladat:");
                festmenyek = festmenyek.OrderByDescending(x => x.getLegmagasabbLicit()).ToList();
                Kiiratas();
                // -- 3/+: A rendezett lista tartalmát írd ki egy festmenyek_rendezett.csv nevű fájlba.
                string fajl = "festmenyek_rendezett.csv";
                StreamWriter ki = new StreamWriter(fajl);
                for (int i = 0; i < festmenyek.Count; i++)
                {
                    ki.WriteLine($"{festmenyek[i].getFesto()};{festmenyek[i].getCim()};{festmenyek[i].getStilus()}");
                }
                ki.Close();
                Console.WriteLine($"3/+ feladat: festmények kiírva a {fajl} fájlba.");
            }
        }
        static int vege = 0;
        static void Main(string[] args)
        {
            // -- 2/A: Vegyél fel egy listát legalább 2 festménnyel.
            festmenyek.Add(new Festmeny("Kecske", "Kovács Béla", "Goatikus"));
            festmenyek.Add(new Festmeny("Kecske tájak", "Kovács Béla", "Goatikus"));
            Ujadatok();
            Beolvas();
            Geplicit();
            Console.WriteLine();
            FelhasznaloLicit();
            Console.WriteLine("\n\n");
            UtolsoFeladat();
            Console.WriteLine("Program vége.");
            Console.ReadKey();
        }


    }
}
