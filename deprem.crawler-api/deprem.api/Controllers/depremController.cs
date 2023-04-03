using Microsoft.AspNetCore.Mvc;
using System.Text;
using HtmlAgilityPack;
using System.Data.SqlClient;
using deprem.Model.Models;
using System.Runtime.Intrinsics.Arm;
using deprem.Database.Data;
using Microsoft.IdentityModel.Tokens;
//using deprem.Database.Migrations;
using Microsoft.EntityFrameworkCore;
using System.Xml;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using NuGet.Protocol;

namespace deprem.api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    
    public class depremController : Controller
    {

        ApplicationDbContext context = new();
        
        [HttpGet]
        public ActionResult<Deprem> getLast()
        {
            var deprem = context.Depremler
        .OrderByDescending(r => r.Id)
        .Select(x => new {
            tarih =x.tarih.ToString("yyyy-MM-dd"),
            x.saat,
            x.enlem,
            x.boylam,
            x.derinlik,
            x.yer,
            x.cozumNiteliği
        })
        .FirstOrDefault();
            return Ok(deprem);
        }


        [HttpGet("{tarih}/{enlem}/{boylam}")]
        public async Task<ActionResult> findBy(DateTime tarih, double enlem, double boylam)
        {
            double distance = 500;
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var location = geometryFactory.CreatePoint(new Coordinate(boylam, enlem));
            var result =  await context.Depremler
                .Where(x => x.Location.Distance(location) < distance && x.tarih> tarih)
                .Select(x => new
                {
                    tarih = x.tarih.ToString("yyyy-MM-dd"),
                    x.saat,
                    x.enlem,
                    x.boylam,
                    x.derinlik,
                    x.yer,
                    x.cozumNiteliği,
                })
                .ToListAsync();
            
            return Ok(result);
        }

    }
}

