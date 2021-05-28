using UnityEngine;

public class RandomName : MonoBehaviour
{
    [SerializeField] string[] names = {
        "Outriggr",
        "Outsider",
        "OutOfDisWorld",
        "OutOfDisWorld",
        "God",
        "BBGun",
        "GunsMan",
        "Warlock",
        "WarMachine",
        "Iron-Man",
        "Sifu",
        "Saddlewitch",
        "WitchHunter",
        "Witcher",
        "Witch",
        "Zombie",
        "Monster",
        "LateNever",
        "Kittywake",
        "LlamaDrama",
        "GraveKeeper",
        "GateKeeper",
        "DragonMaster",
        "Toothless",
        "GrimReap",
        "Sleepnaz",
        "Sleepless",
        "Selfless",
        "Megalith",
        "Megalodon",
        "Sharky",
        "Indium",
        "Paladium",
        "Paladin",
        "General",
        "Major",
        "Corporal",
        "BizzyBee",
        "BumbleBee",
        "Beee",
        "Beast",
        "BeastMaster",
        "CastleClimb",
        "Quern",
        "Queen",
        "TrueFate",
        "FateKiller",
        "Blacklight",
        "Escobar",
        "Bacterigerm",
        "Viral",
        "AnarKiss",
        "OrangeGlade",
        "Maggotta",
        "SableCat",
        "Staple",
        "Nomadiction",
        "Apocalypse",
        "Doom",
        "Dr.Doom",
        "Dr.Dre",
        "Margary",
        "Morello",
        "Minion",
        "Midgeabean",
        "Willowisp",
        "Wasp",
        "Ant-Man",
        "Spider-Man",
        "Chinaplate",
        "BatonRelay",
        "Aurilau",
        "Treasure Hunter",
        "Hunter",
        "Driver",
        "Baby Driver",
        "Bond",
        "Mr. Bond",
        "James Bond",
        "SmokePlumes",
        "Smoker",
        "Dr. Strange",
        "Macro Madam",
        "Slark",
        "Slyrack",
        "Stark",
        "NeoToad",
        "Neo",
        "Dinotrex",
        "T-Rex",
        "BeardDemon",
        "Deamon",
        "Demon",
        "Deamon Slayer",
        "Slayer",
        "Dot",
        "Mr.Dot",
        "Dredd",
        "Judge",
        "Justice",
        "Wonder Woman",
        "Deadlight",
        "FryerTuck",
        "Gigadude",
        "Die Hard",
        "Die",
        "Dead",
        "Dead Man",
        "Masterpiece",
        "Piggy Bank",
        "Bank Robber",
        "Heist",
        "Fast Guy",
        "Doctor",
        "Maze Runner",
        "Runner",
        "Climber",
        "Walker",
        "Video Gamer",
        "Joker",
        "Gamer",
        "Striker",
        "Magic Kid",
        "Magic",
        "Bullet",
        "Kido",
        "Chen",
        "Daniel",
        "Danny",
        "Manu",
        "Manny",
        "Khan",
        "Sub-Zero",
        "Scorpion",
        "Snake",
        "Noob",
        "007",
        "Agent",
        "Secret",
        "Santa",
        "Sacred",
        "Sierra",
        "Buddha",
        "Balina",
        "Meteor",
        "Miracle",
        "Liquid",
        "Lay Low",
        "Porsche",
        "Ferrero",
        "Foster Kid",
        "Fantasy",
        "Mr. Boombastic",
        "Boombastic",
        "Bartender",
        "Bachata",
        "Baraban",
        "Barbaros",
        "Bandit",
        "Bandugan",
        "Lol",
        "Match Maker",
        "Mr. Dog",
        "Mr. Cat",
        "Mafia",
        "Fate",
        "Destiny",
        "Destination Hell",
        "Hell",
        "Hella",
        "Thor",
        "Smasher",
        "Hulk",
        "Thanos",
        "Zebra",
        "ZeeZee",
        "Zaltan",
        "Sultan",
        "King",
        "Mr. King",
        "Nobel",
        "Mr. Nobel",
        "Player",
        "Bulldog ",
        "Bull",
        "Conqueror",
        "Baron",
        "Dark Master",
        "Yo-Yo",
        "Dollar",
        "Bitcoin Miner"
    };

    public string GetRandomName()
    {
        return names[Random.Range(0, names.Length)];
    }
}
