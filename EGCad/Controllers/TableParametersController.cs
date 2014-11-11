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
            return View(GetParameterTableEntries(Parameters));
        }

        public JsonResult GetState()
        {
            return Data(0, new { items = Points.ToArray() });
        }

        public JsonResult Save(int x, int y, params double[] values)
        {
            var points = Points;

            if (Parameters.Count != values.Count()) throw new ArgumentException("Number of values not equal parameters count!");

            var parameters = values.Select((value, i) => new Parameter(Parameters[i].Id, Parameters[i].Name, Parameters[i].Unit, value)).ToList();

            points.Add(new ParameterTableEntry(points.Count, x, y, parameters));
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

        private ParameterTableEntry GetParameterTableEntries(List<Parameter> parameters)
        {
            return new ParameterTableEntry(0, 0, 0, parameters);
        }
    }
}