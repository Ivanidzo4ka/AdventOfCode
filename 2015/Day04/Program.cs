using System.Security.Cryptography;
PartOne();
PartTwo();
void PartOne()
{
    var ans = 0;

    var lines = File.ReadAllLines("input.txt");
    var num = 1L;
    using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
    {
        //var p = "abcdef609043";
        //var tt =md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes(p));
        while (true)
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(lines[0]+num.ToString());
            var hash = md5.ComputeHash(inputBytes);
            if (hash[0] == 0 && hash[1] == 0 && hash[2]==0)
                break;
            num++;
        }
    }
    Console.WriteLine(num);
}


void PartTwo()
{


}

