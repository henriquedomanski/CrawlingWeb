using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlerScraping
{
    public class Configuracao : Registry
    {
     public Configuracao() 
        {
        Schedule<Rastreador>()
                .NonReentrant()
                .ToRunOnceAt(DateTime.Now.AddSeconds(3))
                .AndEvery(2).Seconds();
        }
    }
}
