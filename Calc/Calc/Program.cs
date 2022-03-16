using System;

class MyCalc
{
    static void Main()
    {
        string str;
        double res = 0;

        while (true)
        {
            Console.WriteLine("Для выхода введите '...' ");
            Console.WriteLine("Введите выражение");
            str = Console.ReadLine();
            if (str == "...") break;
            if (str == "") continue;
            
            string[] operators = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            bool firstCorrect = double.TryParse(operators[0], out double a);
            bool secondCorrect = double.TryParse(operators[2], out double b);
            bool show = true;


            if (firstCorrect && secondCorrect)
            {
                if (operators[1] == "+")
                {
                    res = a + b;
                }
                else if (operators[1] == "-")
                {
                    res = a - b;
                }
                else if (operators[1] == "*")
                {
                    res = a * b;
                }
                else if (operators[1] == "/")
                {
                    if (b == 0)
                    {
                        Console.WriteLine("Второе число равно 0. Но на 0 делить нельзя");
                        show = false;
                    }
                    else
                    {
                        res = a / b;
                    }
                }
                else if (operators[1] == "%")
                {
                    res = a % b;
                }
                else if (operators[1] == "^")
                {
                    res = Math.Pow(a, b);
                }
                else
                {
                    Console.WriteLine($"Не могу определить знак {operators[1]}");
                }
                if (show) Console.WriteLine("Ответ: " + res);

            }
            else
            {
                Console.WriteLine("Не удалось распознать число, попробуйте еще раз.");
            }
        }
        Console.WriteLine("Спасибо что воспользовались калькулятором");
    }
}
