using FluentScheduler;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CrawlerScraping
{
    public class Rastreador : IJob
    {
        private string _produto = "Console-Playstation-4";
        private decimal _target = 20000;

        public void Execute()
        {
            StringBuilder sb = new StringBuilder();

            using (WebClient client = new WebClient())
            {
                string mercadoLivre = client.DownloadString($"https://lista.mercadolivre.com.br/{_produto}");

                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml( mercadoLivre );

                var container = htmlDoc.DocumentNode.SelectSingleNode("//ol[@class='ui-search-layout ui-search-layout--grid']");
                var nodes = container.SelectNodes("//div[@class='ui-search-result__wrapper']");

                foreach (var node in nodes) 
                {
                    var nodeImg = node.SelectSingleNode("//div[@class='ui-search-result__image']");

                    var regex = new Regex("<a [^>]*href=(?:'(?<href>.*?)')|(?:\"(?<href>.*?)\")");
                    var arr = regex.Matches(nodeImg.InnerText).OfType<Match>().Select(m => m.Groups["href"].Value).ToArray();

                    var urlAfiliado = arr[0] + "&afiliado=1";
                    var nome = arr[2];

                    var nodeBoxPreco = node.SelectSingleNode("//a[@class='ui-search-result__content ui-search-link']");
                    var preco = nodeBoxPreco.SelectSingleNode("//span[@class='prince-tag-fraction']").InnerText;
                    var itemId = nodeBoxPreco.SelectSingleNode("//form[@class='ui-search-bookmark']").Attributes["action"].Value.Split("/")[3];


                    sb.Append($"<a href='{urlAfiliado}'>{nome}  R$ {preco}</a>");
                }
                Console.WriteLine(sb.ToString());
                Console.ReadLine();
            }



        }



    }
}
