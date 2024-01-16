var _weapon = new (int cost, int damage)[5]
{
    (8,4),
    (10,5),
    (25,6),
    (40,7),
    (74,8)
};
var _armor = new (int cost, int armor)[6]
{
    (0,0),
    (13,1),
    (31,2),
    (53,3),
    (75,4),
    (102,5)
};
var _rings = new (int cost, int damage,int armor)[8]
{
    (0,0,0),
    (0,0,0),
    (25,1,0),
    (50,2,0),
    (100,3,0),
    (20,0,1),
    (40,0,2),
    (80,0,3),
};

PartOne();
PartTwo();

void PartOne()
{
    var ans = 0L;
    var boss = Parse("input.txt");
    ans = EquipWeaponMin((100,0,0), boss, 0);
    Console.WriteLine(ans);
}

long EquipWeaponMin((int hp, int damage, int armor)player, (int hp, int damage, int armor) boss, long cost)
{
    var ans = long.MaxValue;
    for (int i=0; i<_weapon.Length; i++)
        ans = Math.Min(ans, EquipArmorMin((player.hp, player.damage+_weapon[i].damage, player.armor), boss, cost+_weapon[i].cost));
    return ans;
}

long EquipArmorMin((int hp, int damage, int armor)player, (int hp, int damage, int armor) boss, long cost)
{
    var ans = long.MaxValue;
    for (int i=0; i<_armor.Length; i++)
        ans = Math.Min(ans, PickRingMin((player.hp, player.damage, player.armor+_armor[i].armor), boss, cost+_armor[i].cost));
    return ans;
}
long PickRingMin((int hp, int damage, int armor)player, (int hp, int damage, int armor) boss, long cost)
{
    var ans = long.MaxValue;
    for (int i=0; i<_rings.Length; i++)
        for (int j=i+1; j<_rings.Length; j++)
        {
            if (Win((player.hp, player.damage+_rings[i].damage+_rings[j].damage, player.armor+_rings[i].armor+_rings[j].armor), boss))
                ans = Math.Min(ans, cost+_rings[i].cost+_rings[j].cost);
        }
    return ans;
}
long EquipWeaponMax((int hp, int damage, int armor)player, (int hp, int damage, int armor) boss, long cost)
{
    var ans = long.MinValue;
    for (int i=0; i<_weapon.Length; i++)
        ans = Math.Max(ans, EquipArmorMax((player.hp, player.damage+_weapon[i].damage, player.armor), boss, cost+_weapon[i].cost));
    return ans;
}

long EquipArmorMax((int hp, int damage, int armor)player, (int hp, int damage, int armor) boss, long cost)
{
    var ans = long.MinValue;
    for (int i=0; i<_armor.Length; i++)
        ans = Math.Max(ans, PickRingMax((player.hp, player.damage, player.armor+_armor[i].armor), boss, cost+_armor[i].cost));
    return ans;
}
long PickRingMax((int hp, int damage, int armor)player, (int hp, int damage, int armor) boss, long cost)
{
    var ans = long.MinValue;
    for (int i=0; i<_rings.Length; i++)
        for (int j=i+1; j<_rings.Length; j++)
        {
            if (!Win((player.hp, player.damage+_rings[i].damage+_rings[j].damage, player.armor+_rings[i].armor+_rings[j].armor), boss))
                ans = Math.Max(ans, cost+_rings[i].cost+_rings[j].cost);
        }
    return ans;
}
void PartTwo()
{
    var ans = 0L;
    var boss = Parse("input.txt");
    ans = EquipWeaponMax((100,0,0), boss, 0);
    Console.WriteLine(ans);
}


bool Win((int hp, int damage, int armor) player, (int hp, int damage, int armor) boss)
{
    return (100.0/Math.Max(1, player.damage-boss.armor))<=(100.0/Math.Max(1, boss.damage-player.armor));
}

(int hp, int damage, int armor) Parse(string fileName)
{
    var lines = File.ReadAllLines(fileName);
    return (int.Parse(lines[0].Split(" ")[2]), int.Parse(lines[1].Split(" ")[1]), int.Parse(lines[2].Split(" ")[1]));
}
