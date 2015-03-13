using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EGCad.Common.Extensions;
using EGCad.Common.Model.Data;

namespace EGCad.Controllers
{
    public class TableParametersController : InputBaseController
    {
        // GET: TableParameters
        public ActionResult Index()
        {
            if (!Points.Any())
            {
                Points = new List<ParameterTableEntry>(new[] { new ParameterTableEntry(0, 0, Parameters) });
            }
            else
            {
                foreach (var point in Points)
                {
                    var pointParameters = point.Parameters;
                    var currentParametes = Parameters;
                    var actualParameters = new List<Parameter>();

                    foreach (var parameter in currentParametes)
                    {
                        var pointParam = pointParameters.FirstOrDefault(x => x.Id == parameter.Id);
                        actualParameters.Add(pointParam ?? parameter);
                    }

                    point.Parameters = actualParameters;
                }
            }
            return View(Parameters);
        }

        public JsonResult GetState()
        {
            return Data(0, new { items = Points.ToArray(), XMin = Map != null ? Map.StartT.X : 0, XMax = Map != null ? Map.EndT.X : 0 });
        }

        public JsonResult CreatePoint()
        {
            var points = Points;
            points.Add(new ParameterTableEntry(Points.Count, 0, Parameters));
            Points = points;
            return GetState();
        }

        public JsonResult Swap(int[] ids)
        {
            if (ids == null) return GetState();
           
            var points = Points;

            for (var i = 0; i < ids.Length; i++)
            {
                if (ids[i] == points[i].Id) continue;

                var secondIndex = points.FindIndex(x => x.Id == ids[i]);
                points.Swap(i, secondIndex);
            }

            Points = points;
            return GetState();
        }

        public JsonResult Save(int x, int id, double[] values)
        {
            var points = Points;
            if (Parameters.Any())
            {
                if (Parameters.Count != values.Count())
                    throw new ArgumentException("Number of values not equal parameters count!");
            }

            var point = points.First(p => p.Id == id);
            point.X = x;

            if (values != null)
            {
                var parameters = values.Select((value, i) => new Parameter(Parameters[i].Id, Parameters[i].Name, Parameters[i].Unit, value)).ToList();
                point.Parameters = parameters;
            }

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
    }
}