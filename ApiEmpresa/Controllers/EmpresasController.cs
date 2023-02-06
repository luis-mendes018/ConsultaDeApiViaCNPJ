using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using ApiEmpresa.Models;

namespace ApiEmpresa.Controllers
{
    public class EmpresasController : Controller
    {
        HttpClient client = new HttpClient();
      
    
        public ActionResult Details(string cnpj)
        {
            string cnpjSemPontos = cnpj.Trim().Replace(".", "");
            string semBarra = cnpjSemPontos.Replace("/", "");
            string semHifen = semBarra.Replace("-", "");

            try
            {
                HttpResponseMessage response = client.GetAsync($"https://receitaws.com.br/v1/cnpj/" + semHifen).Result;

                var informacoesEmpresa = response.Content.ReadAsAsync<InformacoesEmpresa>().Result;


                if (informacoesEmpresa != null)
                {
                    return View(informacoesEmpresa);
                }

                else
                {
                    
                    return HttpNotFound();
                }
            }
            catch
            {
                
                return View("Erro");
            }
                
        }

        public ActionResult ConsultarEmpresa()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ConsultarEmpresa(InformacoesEmpresa informacoesEmpresa)
        {
           

                HttpResponseMessage responseMessage = client.GetAsync("https://receitaws.com.br/v1/cnpj/cnpj").Result;
                if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("Details");
                }
                else
                {


                    return View();
                } 
        }


        
    }
}
