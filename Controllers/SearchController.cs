using doan.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace doan.Controllers
{
    public class SearchController:Controller
    {
        [HttpGet]
        public IActionResult SearchSP(string tukhoa)
        {
            StoreContext context= HttpContext.RequestServices.GetService(typeof(doan.Models.StoreContext)) as StoreContext;
            try
            {
                if (tukhoa == null) return Redirect("/Home/Index");
                else
                {
                    List<Sanpham> listSP = context.sqlSearchSP(tukhoa);
                    ViewData.Model = listSP;
                    List<string> listHA = new List<string>();
                    foreach (var item in listSP)
                    {
                        string str = context.HinhAnhSP(item.MaSp)[0].LinkHinhAnh;
                        listHA.Add(str);
                    }
                    ViewBag.HinhAnhSP = listHA;
                }
            } 
            catch (Exception err)
            {
                return Redirect("/Error404/Page404");
            }
            return View();
        }
    }
}
