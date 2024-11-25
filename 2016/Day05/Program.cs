
using System.Security.Cryptography;
using System.Text;

PartOne();
PartTwo();
void PartOne()
{
    var input= "uqwqemis";
    var md5 = MD5.Create();
    var password = new StringBuilder();
    long ind=0;
    while(password.Length<8)
    {
        var bytes = Encoding.ASCII.GetBytes(input+ind);
        var res = md5.ComputeHash(bytes);
        if (res[0]==0&&res[1]==0&& res[2]<16)
        {
            password.Append(res[2].ToString("X"));
        }
        ind++;
    }
    Console.WriteLine(password.ToString());
}

void PartTwo()
{
    var input= "uqwqemis";
    var md5 = MD5.Create();
    var password = new char[8];
    long ind=0;
    var count=0;
    while(count<8)
    {
        var bytes = Encoding.ASCII.GetBytes(input+ind);
        var res = md5.ComputeHash(bytes);
        if (res[0]==0&&res[1]==0&& res[2]<=7&& password[res[2]]=='\0')
        {
            password[res[2]] = ((res[3]/16).ToString("X"))[0];
            count++;
        }
        ind++;
    }
    Console.WriteLine(new string(password));
}