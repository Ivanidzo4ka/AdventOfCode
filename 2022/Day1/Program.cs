StepOne();
StepTwo();
void StepOne(){

    Console.WriteLine(DataPoints().Max());
}
void StepTwo()
{
    Console.WriteLine(DataPoints().OrderByDescending(x=>x).Take(3).Sum());
}

IEnumerable<int> DataPoints(){
   
    var current = 0;
    foreach(var line in File.ReadAllLines("input.txt"))
    {
        if (line==""){
            yield return current;
            current=0;
        }
        else{
            current+=int.Parse(line);
        }
    }
    yield return current;
}

