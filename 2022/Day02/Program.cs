StepOne();
StepTwo();
void StepOne(){
var map = new Dictionary<char, Play>(){
    ['A'] = Play.Rock,
    ['B'] = Play.Paper,
    ['C'] =  Play.Scissors,
    ['X'] = Play.Rock,
    ['Y'] = Play.Paper,
    ['Z'] = Play.Scissors,
};

var score = 0;
foreach(var line in File.ReadAllLines("input.txt"))
{
     var first = map[line[0]];
     var second = map[line[2]];
     score+=Score(first, second); 
     score+=PlayScore(second);
}
Console.WriteLine(score);
}

void StepTwo(){
    var map = new Dictionary<char, Play>(){
    ['A'] = Play.Rock,
    ['B'] = Play.Paper,
    ['C'] =  Play.Scissors,
};

var score = 0;
foreach(var line in File.ReadAllLines("input.txt"))
{
     var first = map[line[0]];
     var second = Decipher(first, line[2]);
     score+=Score(first, second); 
     score+=PlayScore(second);
}
Console.WriteLine(score);
}



int Score(Play first, Play second){
    return first switch{
        Play.Rock=>
            second switch{
                Play.Rock =>3,
                Play.Paper=> 6,
                Play.Scissors => 0,
            },
        Play.Paper=>
            second switch{
                Play.Rock =>0,
                Play.Paper=> 3,
                Play.Scissors => 6,
            },
        Play.Scissors=>
            second switch{
                Play.Rock =>6,
                Play.Paper=> 0,
                Play.Scissors => 3,
            }
    };
}

int PlayScore(Play value){
    return value switch{
        Play.Rock =>1,
        Play.Paper =>2,
        Play.Scissors =>3
    };
}

Play Decipher(Play value, char action)
{
    //DRAW
    return action switch{
        //DRAW
        'Y'=> value,
        // LOSE
        'X'=> value switch {
            Play.Rock => Play.Scissors,
            Play.Paper => Play.Rock,
            Play.Scissors => Play.Paper
        },
        // WIN
        'Z'=>value switch {
        Play.Rock => Play.Paper,
        Play.Paper => Play.Scissors,
        Play.Scissors => Play.Rock}

    };
}


enum Play{
    Rock,
    Paper,
    Scissors
}