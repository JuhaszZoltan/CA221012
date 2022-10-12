namespace CA221012
{
    public abstract class Allat
    {
        public string Nev { get; set; }
        public DateTime Szulinap { get; set; }
        public abstract string HangotAdKi();
    }

    public interface ILogolhato
    {
        public int Pozicio { get; }
        public string LogBejegyzes();
    }

    public class Vaza : ILogolhato
    {
        private int _pozicio;
        public int Pozicio
        {
            get => _pozicio; 
            private set => _pozicio = value;
        }

        public string Anyag { get; set; }
        public ConsoleColor Szin { get; set; }
        public bool Torott { get; set; } = false;

        public string LogBejegyzes()
        {
            return 
                $"időbélyeg: {DateTime.Now.Ticks}\n" +
                $"típus: VÁZA\n" +
                $"\tSzín: {Szin}\n" +
                $"\tAnyag: {Anyag}\n" +
                $"\tPozíció: {Pozicio}\n" +
                $"\tEz a váza {(Torott ? "el van törve" : "egyben van")}";
        }

        public Vaza(int pozicio, string anyag, ConsoleColor szin)
        {
            Pozicio = pozicio;
            Anyag = anyag;
            Szin = szin;
        }
    }


    public class Macska : Allat, ILogolhato
    {
        public int EletekSzama { get; set; } = 9;

        public int Pozicio { get; set; }

        public override string HangotAdKi()
        {
            return "miaú - miaú";
        }

        public void Mozog(int hova)
        {
            Pozicio = hova;
        }

        public string LogBejegyzes()
        {
            return
                $"időbélyeg: {DateTime.Now.Ticks}\n" +
                $"típus: MACSKA\n" +
                $"\tNeve: {Nev}\n" +
                $"\tSzülinap: {Szulinap.ToString("yyyy-MM-dd")}\n" +
                $"\tPozíció: {Pozicio}";
        }
    }

    public class Kutya : Allat
    {
        public override string HangotAdKi()
        {
            return "vau - vau";
        }
    }



    public class Program
    {

        static Random rnd = new();
        static void Main()
        {
            List<Allat> allatok = new()
            {
                new Kutya() 
                {
                    Nev = "Bodri",
                    Szulinap = new(2002, 03, 15)
                },
                new Macska()
                {
                    Nev = "Lukrécica",
                    Szulinap = new(2020, 11, 07)
                },
            };
            foreach (var allat in allatok)
            {
                Console.WriteLine($"{allat.Nev} mondja : {allat.HangotAdKi()}!");
            }

            var polc = new ILogolhato[20];

            for (int i = 0; i < 5; i++)
            {
                int vazaHelye = rnd.Next(polc.Length);
                polc[vazaHelye] = new Vaza(
                    pozicio: vazaHelye,
                    anyag: "Agyag",
                    szin: ConsoleColor.Red);
            }

            Macska vk = new Macska()
            {
                Nev = "Váza Killer",
                Szulinap = DateTime.Parse("2010-06-06"),
                Pozicio = -1,
            };

            for (int i = 0; i < 10; i++)
            {
                if (vk.Pozicio != -1 && polc[vk.Pozicio] is Macska)
                {
                    polc[vk.Pozicio] = null;
                }
                vk.Pozicio = rnd.Next(polc.Length);

                if (polc[vk.Pozicio] is Vaza)
                {
                    (polc[vk.Pozicio] as Vaza).Torott = true;
                }
                else
                {
                    polc[vk.Pozicio] = vk;
                }

            }

            foreach (var item in polc)
            {
                if(item is not null)
                    Console.WriteLine(item.LogBejegyzes());
            }

        }
    }
}