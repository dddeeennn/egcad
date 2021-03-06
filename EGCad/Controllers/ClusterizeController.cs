﻿using System.Web.Mvc;
using EGCad.Common.Model.Clusterize;
using EGCad.Core.Clasterize;

namespace EGCad.Controllers
{
    public class ClusterizeController : Controller
    {
        // GET: Clusterize
        public ActionResult Index()
        {
            return View((ClusterTree)TempData["clusterizedData"] ?? new ClusterTree());
        }
    }
}