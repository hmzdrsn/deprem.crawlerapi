using HtmlAgilityPack;
using System.Text;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using Microsoft.Extensions.DependencyInjection;
using deprem.Database.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using deprem.Model.Models;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using System.Globalization;

var url = "http://www.koeri.boun.edu.tr/scripts/lst0.asp";
var xPath = @"//html/body/pre";

ApplicationDbContext context = new();//***

while (true)
{
    var web = new HtmlWeb();
    web.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3";
    web.OverrideEncoding = Encoding.UTF8;
    var doc = web.Load(url);
    HtmlNodeCollection data = doc.DocumentNode.SelectNodes(xPath);
    System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.InvariantCulture;
    Deprem deprem = new Deprem();
    var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid:4326);
    foreach (HtmlNode item in data)
    {
        
        string veri = item.InnerText;
        deprem.tarih =DateTime.Parse(veri.Substring(586, 10).Trim());
        deprem.saat = TimeSpan.Parse(veri.Substring(597, 8).Trim());
        deprem.enlem =Double.Parse(veri.Substring(607, 8).Trim(),culture);
        deprem.boylam =Double.Parse(veri.Substring(617, 7).Trim(),culture);
        deprem.derinlik = Double.Parse(veri.Substring(631, 4).Trim(), culture);
        if(Double.TryParse(veri.Substring(641, 3).Trim(),culture, out Double result))
        {
            deprem.md = result;
        }
        else
        {
            deprem.md = 0.0;
        }
        if (Double.TryParse(veri.Substring(646, 3).Trim(), culture, out Double result2))
        {
            deprem.ml = result2;
        }
        else
        {
            deprem.ml = 0.0;
        }
        if (Double.TryParse(veri.Substring(651, 3).Trim(), culture, out Double result3))
        {
            deprem.mw = result3;
        }
        else
        {
            deprem.mw = 0.0;
        }

        deprem.yer = veri.Substring(657, 50).Trim();
        deprem.cozumNiteliği = veri.Substring(707, 6).Trim();
        if (deprem.cozumNiteliği.Substring(0, 1) == "�");
        {
            deprem.cozumNiteliği = "İlksel";
        }

        deprem.Location = geometryFactory.CreatePoint(new Coordinate(deprem.boylam,deprem.enlem));
        var x = deprem.Location.X;
        var y = deprem.Location.Y;


        CompareandInsert(deprem);
    }
    Thread.Sleep(TimeSpan.FromSeconds(60));//**
}

void Insert(Deprem deprem)
{
    context.Depremler.Add(deprem);
    context.SaveChanges();
    Console.WriteLine("Kayıt Eklendi");
}

void CompareandInsert(Deprem deprem)
{
        var sonkayit =context.Depremler.OrderByDescending(r => r.Id).FirstOrDefault();
        if(sonkayit?.saat == deprem.saat)
        {

        }
        else
        {
            Insert(deprem);
        }
    
}
