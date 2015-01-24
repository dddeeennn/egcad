using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EGCad.Models.InputData;

namespace EGCad.Controllers
{
    public class TableParametersController : InputBaseController
    {
        // GET: TableParameters
        public ActionResult Index()
        {
            if (!Points.Any())
            {
                Points = new List<ParameterTableEntry>(new[] {new ParameterTableEntry(0, 0, 0, Parameters)});
            }
            else
            {
                foreach (var parameter in Parameters)
                {
                    if (Points[0].Parameters.All(x => x.Id != parameter.Id))
                    {
                        var points = Points;
                        points.ForEach(p=>p.Parameters.Add(parameter));
                        Points=points;
                    }
                }
            }
            return View(Parameters);
        }

        public JsonResult GetState()
        {
            return Data(0, new { items = Points.ToArray() });
        }

        public JsonResult CreatePoint()
        {
            var points = Points;
            points.Add(new ParameterTableEntry(Points.Last().Id+1, 0, 0, Parameters));
            Points = points;
            return GetState();
        }

        public JsonResult Save(int x, int y, int id, double[] values)
        {
            var points = Points;

            if (Parameters.Count != values.Count()) throw new ArgumentException("Number of values not equal parameters count!");

            var parameters = values.Select((value, i) => new Parameter(Parameters[i].Id, Parameters[i].Name, Parameters[i].Unit, value)).ToList();

            var point = points.First(p => p.Id == id);
            point.Parameters = parameters;
            point.X = x;
            point.Y = y;
            Points = points;

            return GetState();
        }

        public JsonResult Remove(int id)
        {
            var points = Points;

            points.Remove(points.FirstOrDefault(x => x.Id == id));
            points.ForEach(x => x.Id = points.IndexOf(x));

            Points = points;

            return GetState();
        }

        private List<ParameterTableEntry> GetParameterTableEntries(List<Parameter> parameters)
        {
            return new List<ParameterTableEntry>(new[] { new ParameterTableEntry(0, 0, 0, parameters) });
        }
    }
}