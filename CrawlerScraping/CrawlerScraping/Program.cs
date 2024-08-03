using FluentScheduler;
using System;

namespace CrawlerScraping;
class program
{
    static void Main(string[] args)
    {
        JobManager.Initialize(new Configuracao());
        Console.ReadLine();
    }
}