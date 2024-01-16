var _dic = new Dictionary<(int php, int pmana, int bhp, bool turn, int shield, int poison, int recharge), long>();

PartOne();
PartTwo();

void PartOne()
{
    var ans = 0L;
    var boss = Parse("input.txt");
    _dic= new();
    ans = Play((50, 500), boss, true, 0, 0, 0, false);
    Console.WriteLine(ans);
}
long Play((int hp, int mana) player, (int hp, int damage) boss, bool playerTurn, int shield, int poison, int recharge, bool hard)
{
    var ans = 1000000000L;
    var key = (player.hp, player.mana, boss.hp, playerTurn, shield, poison, recharge);
    if (_dic.ContainsKey(key))
        return _dic[key];

    if (poison > 0)
        boss.hp -= 3;
    if (boss.hp <= 0)
        return 0;

    player.mana += (recharge > 0 ? 101 : 0);
    if (!playerTurn)
    {
        player.hp -= Math.Max(1, boss.damage - (shield > 0 ? 7 : 0));
        if (player.hp <= 0)
            return ans;
        ans = Math.Min(ans, Play(player, boss, !playerTurn, Math.Max(0, shield - 1), Math.Max(0, poison - 1), Math.Max(0, recharge - 1), hard));
        return ans;
    }
    if (hard)
        player.hp--;
    if (player.hp<=0)
        return ans;
    if (player.mana >= 53)
    {
        (int hp, int damage) nextBoss = (boss.hp - 4, boss.damage);
        if (nextBoss.hp < 0)
            return 53;
        ans = Math.Min(ans, 53 + Play((player.hp, player.mana - 53), nextBoss, false, Math.Max(0, shield - 1), Math.Max(0, poison - 1), Math.Max(0, recharge - 1), hard));
    }
    if (player.mana >= 73)
    {
        (int hp, int damage) nextBoss = (boss.hp - 2, boss.damage);
        if (nextBoss.hp < 0)
            return 73;
        ans = Math.Min(ans, 73 + Play((player.hp + 2, player.mana - 73), nextBoss, false, Math.Max(0, shield - 1), Math.Max(0, poison - 1), Math.Max(0, recharge - 1), hard));
    }
    if (player.mana >= 113 && shield <= 1)
    {
        ans = Math.Min(ans, 113 + Play((player.hp, player.mana - 113), boss, false, 6, Math.Max(0, poison - 1), Math.Max(0, recharge - 1), hard));
    }
    if (player.mana >= 173 && poison <= 1)
    {
        ans = Math.Min(ans, 173 + Play((player.hp, player.mana - 173), boss, false, Math.Max(0, shield - 1), 6, Math.Max(0, recharge - 1), hard));
    }
    if (player.mana >= 229 && recharge <= 1)
    {
        ans = Math.Min(ans, 229 + Play((player.hp, player.mana - 229), boss, !playerTurn, Math.Max(0, shield - 1), Math.Max(0, poison - 1),5, hard));
    }
    _dic[key] = ans;
    return ans;
}
void PartTwo()
{
    var ans = 0L;
    var boss = Parse("input.txt");
    _dic = new();
    ans = Play((50, 500), boss, true, 0, 0, 0, true);
    Console.WriteLine(ans);
}

(int hp, int damage) Parse(string fileName)
{
    var lines = File.ReadAllLines(fileName);
    return (int.Parse(lines[0].Split(" ")[2]), int.Parse(lines[1].Split(" ")[1]));

}
