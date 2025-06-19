// See https://aka.ms/new-console-template for more information
using CalculatorNS;

Console.WriteLine("Program.cs");
Calculator calculator = new Calculator();

Console.WriteLine("Enter first number:");
double firstNumber = Convert.ToDouble(Console.ReadLine());
Console.WriteLine("Enter second number:");
double secondNumber = Convert.ToDouble(Console.ReadLine());
Console.WriteLine("Choose an operation: +, -, *, /");
string operation = Console.ReadLine();
double result = 0;

switch (operation)
{
    case "+":
        result = calculator.Add(firstNumber, secondNumber);
        break;
    case "-":
        result = calculator.Subtract(firstNumber, secondNumber);
        break;
    case "*":
        result = calculator.Multiply(firstNumber, secondNumber);
        break;
    case "/":
        try
        {
            result = calculator.Divide(firstNumber, secondNumber);
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
        break;
    default:
        Console.WriteLine("Invalid operation.");
        return;
}