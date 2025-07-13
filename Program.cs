using System.Collections;
using System.ComponentModel.Design;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata;

namespace CSharpFundamentalTwo.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ExecuteStackExample();
            //ExecuteQueueExample();

        }


        static void ExecuteQueueExample()
        {
            var queue = new Queue<string>();
            while (true)
            {
                Console.Write("Please Select Documents To Print ('print' to Print): ");
                var input = Console.ReadLine();
                if (input.Equals("print", StringComparison.OrdinalIgnoreCase))
                {
                    while (queue.Count > 0) { 
                    Console.WriteLine($"Printing docuemts '{queue.Dequeue()}'...");
                        Console.WriteLine($"Queue Count: {queue.Count}");
                }
                }
                else
                    queue.Enqueue(input);
            }
        }
        static void ExecuteStackExample()
        {
            var commmandStack = new Stack<AppendTextCommand>();
            var redoStack = new Stack<AppendTextCommand>();
            var origianlText = "";

            while (true)
            {
                Console.Write("Type Text To Apppend ('exit' To Exit, 'undo' To Undo, 'redo' To Redo): ");
                var input = Console.ReadLine();
                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    break;
                else if (input.Equals("undo", StringComparison.OrdinalIgnoreCase))
                {
                    if (commmandStack.Count > 0)
                    {
                        var command = commmandStack.Pop();
                        origianlText = command.Undo();
                        redoStack.Push(command);

                    }
                }
                else if (input.Equals("redo", StringComparison.OrdinalIgnoreCase))
                {
                    var command = redoStack.Pop();
                    origianlText = command.Redo();
                    commmandStack.Push(command);
                }
                else
                {
                    var command = new AppendTextCommand(origianlText, input);
                    origianlText = command.Excute();
                    commmandStack.Push(command);
                }


            }
        }


        class AppendTextCommand
        {
            private string _originalText;
            private readonly string _textToAppend;
            public AppendTextCommand(string originalText, string textToAppend)
            {
                _originalText = originalText;
                _textToAppend = textToAppend;
            }
            public string Excute()
            {
                _originalText += _textToAppend;
                Console.WriteLine(_originalText);
                return _originalText;
            }
            public string Undo()
            {
                _originalText = _originalText.Substring(0, _originalText.Length - _textToAppend.Length);
                Console.WriteLine(_originalText);
                return _originalText;
            }
            public string Redo()
            {
                _originalText += _textToAppend;
                Console.WriteLine(_originalText);
                return _originalText;
            }
        }
        }

    }



