// See https://aka.ms/new-console-template for more information
using ConsoleAppDb;
using Microsoft.EntityFrameworkCore;
using SampleWebAPI.Data;
using SampleWebAPI.Domain;


//Entity Framework
SamuraiContext _context = new SamuraiContext();
_context.Database.EnsureCreated();

Console.WriteLine("Sebelum tambah data samurai");
//GetSamurai();
/*Console.WriteLine("Tambah data samurai");
AddSamurai();*/
//AddMultipleSamurai("Ado", "Bima", "Camara","Danile","Erwin","Franco","Giant","Hartanto","Icigo");
//GetSamuraiByName("samurai");
//var data = GetById(2);
//Console.WriteLine($"GetById - {data.Id} - {data.Name}");

/*var samurai = _context.Samurais.Find(2);
Console.WriteLine($"{samurai.Id} - {samurai.Name}");*/
//AddMoreThanOneType();

//UpdateSamurai(6,"Rengoku Kyojiro");
//DeleteSamurai(15);
//DeleteMultipleSamurai("Samurai");
//AddSamuraiWithQuote();
//AddQuoteToExistingSamurai();
//AddSamuraiToExistingBattle();
//AddSamuraiWithHorse();
//AddHorseToExistingSamurai();
//GetSamurai();
//GetQuotes();
//AddMoreThanOneType();
//GetBattle();
//RemoveSamuraiFromBattle();
//GetBattlesWithSamurais();
//GetQuotesWithSamurai();
//GetSamuraiWithQuotes();
//ProjectionSample();

//AddSword();
//QueryWithRawSQLInterpolated();
//AddMoreThanOneType();

//AddSamuraiWithQuote()
//AddQuoteToExistingSamurai();
//AddSamuraiToExistingBattle();
//AddMultipleSamurai("Ado", "Bima", "Camara", "Danile", "Erwin", "Franco", "Giant", "Hartanto", "Icigo");
//AddMultipleElement("Air elemental", "Earth elementa", "Fire elemental", "Water elemental", "Darkness elemental",
//    "Nature elemental", "Lightning elemental");

//AddTypeToExistingSword();
//AddSamuraiToExistingBattle();
AddElementToExistingSword();
QueryUsingSP();

Console.ReadKey();

void AddSamurai()
{
    var samurai = new Samurai { Name = "Tanjiro" };
    _context.Samurais.Add(samurai);
    _context.SaveChanges();
}
void AddMultipleSamurai(params string[] names)
{
    foreach(string name in names)
    {
        _context.Samurais.Add(new Samurai { Name= name });
    }
    _context.SaveChanges();
}
void AddMoreThanOneType()
{
    _context.AddRange(new Samurai { Name = "Kaido" },
        new Battle { Name = "Battle of Anegawa"},
        new Battle { Name = "Battle of Dragon"},
        new Battle { Name = "Battle of Raizo" },
        new Battle { Name = "Battle of Ramen" },
        new Battle { Name = "Battle of Belanda" },
        new Battle { Name = "Battle of Kyoto" });
_context.SaveChanges();
}
Samurai GetById(int id)
{
    //var result = _context.Samurais.Where(s => s.Id == id).FirstOrDefault();
    var result = (from s in _context.Samurais
                  where s.Id == id
                  select s).FirstOrDefault();
    if (result != null)
        return result;
    else
        throw new Exception($"Data dengan id {id} tidak ditemukan");
}
void GetSamurai()
{
    var samurais = _context.Samurais.OrderByDescending(s => s.Name).ToList();
    /*var samurais = (from s in _context.Samurais
                   orderby s.Name descending
                   select s).ToList();*/
    Console.WriteLine($"Jumlah samurai: {samurais.Count}");
    foreach(var samurai in samurais)
    {
        Console.WriteLine($"{samurai.Id} - {samurai.Name}");
    }
}
void GetBattle()
{
    var battles = _context.Battles.OrderBy(b => b.Name).ToList();
    foreach(var battle in battles)
    {
        Console.WriteLine($"{battle.BattleId} - {battle.Name}");
    }
}
void GetSamuraiByName(string name)
{
    var samurais = _context.Samurais.Where(s => s.Name.Contains(name.ToLower())).OrderBy(s => s.Name).ToList();
    foreach(var samurai in samurais)
    {
        Console.WriteLine($"{samurai.Id} - {samurai.Name}");
    }
}
void GetQuotes()
{
    var quotes = _context.Quotes.OrderBy(q => q.Text).ToList();
    foreach( var quote in quotes)
    {
        Console.WriteLine($"{quote.Text} - {quote.SamuraiId}");
    }
}
void GetQuotesWithSamurai()
{
    var quotes = _context.Quotes.Include(q=>q.Samurai).OrderBy(q => q.Text).ToList();
    foreach(var quote in quotes)
    {
        Console.WriteLine($"{quote.Text} by {quote.Samurai.Name}");
    }
}
void GetSamuraiWithQuotes()
{
    var samurais = _context.Samurais.Include(s => s.Quotes).ToList();
    foreach(var samurai in samurais)
    {
        Console.WriteLine($"{samurai.Name}");
        foreach(var quote in samurai.Quotes)
        {
            Console.WriteLine($"-----> {quote.Text}");
        }
    }
}
void UpdateSamurai(int id,string nama)
{
    var samurai = _context.Samurais.FirstOrDefault(s=>s.Id==id);
    if(samurai!=null)
    {
        samurai.Name = nama;
        _context.SaveChanges();
    }
    else
    {
        Console.WriteLine("Data tidak ditemukan");
    }
}
void DeleteSamurai(int id)
{
    var samurai = _context.Samurais.Find(id);
    if(samurai!=null)
    {
        _context.Samurais.Remove(samurai);
        _context.SaveChanges();
    }
    else
    {
        Console.WriteLine("Data tidak ditemukan");
    }
}
void DeleteMultipleSamurai(string name)
{
    var results = _context.Samurais.Where(s => s.Name.Contains(name.ToLower()))
            .OrderBy(s => s.Name).ToList();
    _context.Samurais.RemoveRange(results);
    _context.SaveChanges();
}


void AddQuoteToExistingSamurai()
{
    var samurai = _context.Samurais.Find(3);
    if(samurai!=null)
    {
        samurai.Quotes.Add(new Quote { Text = "No for war"});
        samurai.Quotes.Add(new Quote { Text = "I am Hero" });
        _context.SaveChanges();
    }
}
void ProjectionSample()
{
    var results = _context.Samurais.Include(s=>s.Quotes).Select(s => new { 
        s.Name,
        JumlahQuotes = s.Quotes.Count
    }).ToList();
    foreach(var item in results)
    {
        Console.WriteLine($"{item.Name} - {item.JumlahQuotes}");
    }
}

void GetBattlesWithSamurais()
{
    var battles = _context.Battles.Include(b => b.Samurais).ToList();
    foreach(var battle in battles)
    {
        Console.WriteLine($"{battle.BattleId} - {battle.Name} :");
        foreach(var samurai in battle.Samurais)
        {
            Console.WriteLine($"-----> {samurai.Id} - {samurai.Name}");
        }
    }
}
void RemoveSamuraiFromBattle()
{
    var battles = _context.Battles.Include(b => b.Samurais.Where(s => s.Id == 2))
        .FirstOrDefault(b => b.BattleId == 2);
    var samurai = battles.Samurais[0];
    battles.Samurais.Remove(samurai);
    _context.SaveChanges();
}
void AddSamuraiWithHorse()
{
    var samurai = new Samurai { Name = "Kenshin Himura", Horse = new Horse { Name = "White Tornado" } };
    _context.Samurais.Add(samurai);
    _context.SaveChanges();
}

void GetSamuraiWithHorse()
{
    var samurais = _context.Samurais.Include(s => s.Horse).ToList();
    foreach(var samurai in samurais)
    {
        if(samurai.Horse!=null)
            Console.WriteLine($"{samurai.Name} - {samurai.Horse.Name}");
    }
}

void QueryWithRawSQL()
{
    //jangan digunakan karena rawan SQL Injection
    string name = "Zenitsu";
    var samurais = _context.Samurais.FromSqlRaw($"select * from Samurais where Name='{name}' ").ToList();
    foreach(var samurai in samurais)
    {
        Console.WriteLine($"{samurai.Name}");
    }
}

void QueryWithRawSQLInterpolated()
{
    string name = "Zenitsu";
    var samurais = _context.Samurais.FromSqlInterpolated($"select * from Samurais where Name={name}").ToList();
    foreach (var samurai in samurais)
    {
        Console.WriteLine($"{samurai.Name}");
    }
}

void GetSamuraiBattleStats()
{
    var stats = _context.SamuraiBattleStats.OrderBy(s => s.Name).ToList();
    foreach(var stat in stats)
    {
        Console.WriteLine($"{stat.Name} - {stat.NumberOfBattles} - {stat.EarliestBattle}");
    }
}

void QueryUsingSP()
{
    var text = "light";
    var samurais = _context.Samurais.FromSqlInterpolated($"exec dbo.SamuraisWhoSaidAWord {text}").ToList();
    foreach(var samurai in samurais)
    {
        Console.WriteLine($"{samurai.Id} - {samurai.Name}");
    }
}
void AddSword()
{
    var newSword = new Sword
    {
        Name = "Excite Bullets",
        Weight = 1.6,
        SamuraiId = 2
    };
    _context.Swords.Add(newSword);
    _context.SaveChanges();
}
void AddMultipleElement(params string[] names)
{
    foreach (string name in names)
    {
        _context.Elements.Add(new Element { Name = name });
    }
    _context.SaveChanges();
}
void AddHorseToExistingSamurai()
{
    var samurai = _context.Samurais.FirstOrDefault(s => s.Id == 10);
    samurai.Horse = new Horse { Name = "Jackalope" };
    _context.SaveChanges();
}
void AddQuote()
{
    var newQuote = new Quote
    {
        Text = "Ini harga mati",
        SamuraiId = 2
    };
    _context.Quotes.Add(newQuote);
    _context.SaveChanges();
}
void AddSamuraiWithQuote()
{
    var samurai = new Samurai
    {
        Name = "Pitung",
        Quotes = new List<Quote>
        {
            new Quote { Text = "I am Hero" },
            new Quote { Text = "One more time" }
        }
    };
    _context.Samurais.Add(samurai);
    _context.SaveChanges();
}
void AddTypeToExistingSword()
{
    var sword = _context.Swords.FirstOrDefault(s => s.Id == 12);
    sword.SType = new SType { Name = "double handed sword" };
    _context.SaveChanges();
}
void AddSamuraiToExistingBattle()
{
    //var battle = _context.Battles.FirstOrDefault(b => b.BattleId == 1);
    //var samurai = _context.Samurais.FirstOrDefault(s => s.Id == 2);

    //var samurai1 = _context.Samurais.Find(1);
    var samurai2 = _context.Samurais.Find(4);
    var samurai3 = _context.Samurais.Find(9);
    var samurai4 = _context.Samurais.Find(1);

    var battle2 = _context.Battles.Find(6);

    //battle.Samurais.Add(samurai);
    samurai2.Battles.Add(battle2);
    samurai3.Battles.Add(battle2);
    samurai4.Battles.Add(battle2);

    _context.SaveChanges();
}

void AddElementToExistingSword()
{
    
    var Element1 = _context.Elements.Find(5);
    //var Element2 = _context.Elements.Find(5);

    var Sword1 = _context.Swords.Find(11);
    var Sword2 = _context.Swords.Find(12);
    //battle.Samurais.Add(samurai);
    Element1.Swords.Add(Sword1);
    Element1.Swords.Add(Sword2);

    _context.SaveChanges();
}









