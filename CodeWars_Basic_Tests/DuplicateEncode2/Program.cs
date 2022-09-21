using System.Text.RegularExpressions;

if (args.Length == 0) {
    Console.WriteLine("You must specify the source string");
    return;
}
string SourceString = args[0];
Console.WriteLine($"Source string: {SourceString}");

string TargetString = SourceString.ToLower();

foreach (char Symbol in (TargetString.ToCharArray())) {
    string strSymbol = Symbol.ToString();
    if (Regex.IsMatch(Symbol.ToString(), "\\W"))
        strSymbol = "\\" + strSymbol;

    int AmountOfSymbols = (Regex.Matches(TargetString, strSymbol)).Count;
    if (AmountOfSymbols == 1)
        TargetString = TargetString.Replace(Symbol, '(');
    else
        TargetString = TargetString.Replace(Symbol, ')');
}
Console.WriteLine($"Target string: {TargetString}");

