// See https://aka.ms/new-console-template for more information
using DataAccessLibrary;

Console.WriteLine("Hello, World!");

DataController stackController = new DataController("foo", Table.Stacks);
stackController.InsertStack();