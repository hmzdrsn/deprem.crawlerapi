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

            Deprem deprem = new Deprem();
            deprem = context.Depremler.OrderByDescending(r => r.Id).FirstOrDefault();
            return Ok(deprem); 
        }

        [HttpGet]
        public ActionResult<Deprem> getAll()
        {
            var deprem = context.Depremler.ToList();
            return Ok(deprem);
        }

        [HttpGet]
        public async Task<ActionResult> findBy(DateTime tarih, double boylam, double enlem)
        {
            double distance = 5;
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var location = geometryFactory.CreatePoint(new Coordinate(boylam, enlem));
            var result =  await context.Depremler
                .Where(x => x.Location.Distance(location) < distance && x.tarih> tarih)
                .Select(x => new
                {
                    x.tarih,
                    x.saat,
                    konum = x.Location.ToString(),
                    x.derinlik,
                    x.yer,
                    x.cozumNiteliği,
                })
                .ToListAsync();
            
            return Ok(result);
        }

    }
}

