using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RpgMvc.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace RpgMvc.Controllers
{
    public class DisputasController : Controller
    {
        public string uriBase = "http://iagomartuci.somee.com/RpgApi/Disputas/";
        //public string uriBase = "http://iagomartuci-etec.somee.com/RpgApi/Disputas/";

        //Programação aqui
        [HttpGet]
        public async Task<ActionResult> IndexAsync()
        {
            try
            {
                if (ValidarLogin() == false)
                {
                    TempData["MensagemErro"] = "Você não está logado";
                    return RedirectToAction("IndexLogin", "Usuarios"); //Método: IndexLogin e Controller: Usuarios ("IndexLogin", "Usuarios")
                }

                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                string uriBuscaPersonagens = "http://iagomartuci.somee.com/RpgApi/Personagens/GetAll";
                //string uriBuscaPersonagens = "http://iagomartuci-etec.somee.com/RpgApi/Personagens/GetAll";
                HttpResponseMessage response = await httpClient.GetAsync(uriBuscaPersonagens);
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<PersonagemViewModel> listaPersonagens = await Task.Run(() =>
                        JsonConvert.DeserializeObject<List<PersonagemViewModel>>(serialized));

                    ViewBag.ListaAtacantes = listaPersonagens;
                    ViewBag.ListaOponentes = listaPersonagens;
                    return View();
                }
                else
                {
                    throw new Exception(serialized);
                }
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<ActionResult> IndexAsync(DisputaViewModel disputa)
        {
            try
            {
                if (ValidarLogin() == false)
                {
                    TempData["MensagemErro"] = "Você não está logado";
                    return RedirectToAction("IndexLogin", "Usuarios"); //Método: IndexLogin e Controller: Usuarios ("IndexLogin", "Usuarios")
                }

                HttpClient httpClient = new HttpClient();
                string uriComplementar = "Arma";

                var content = new StringContent(JsonConvert.SerializeObject(disputa));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await httpClient.PostAsync(uriBase + uriComplementar, content);
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    disputa = await Task.Run(() => JsonConvert.DeserializeObject<DisputaViewModel>(serialized));
                    TempData["Mensagem"] = disputa.Narracao;
                    return RedirectToAction("Index", "Personagens");
                }
                else
                {
                    throw new Exception(serialized);
                }
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<ActionResult> IndexHabilidadesAsync()
        {
            try
            {
                if (ValidarLogin() == false)
                {
                    TempData["MensagemErro"] = "Você não está logado";
                    return RedirectToAction("IndexLogin", "Usuarios"); //Método: IndexLogin e Controller: Usuarios ("IndexLogin", "Usuarios")
                }

                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                string uriBuscaPersonagens = "http://iagomartuci.somee.com/RpgApi/Personagens/GetAll";
                //string uriBuscaPersonagens = "http://iagomartuci-etec.somee.com/RpgApi/Personagens/GetAll";
                HttpResponseMessage response = await httpClient.GetAsync(uriBuscaPersonagens);
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<PersonagemViewModel> listaPersonagens = await Task.Run(() =>
                        JsonConvert.DeserializeObject<List<PersonagemViewModel>>(serialized));

                    ViewBag.ListaAtacantes = listaPersonagens;
                    ViewBag.ListaOponentes = listaPersonagens;
                }
                else
                {
                    throw new Exception(serialized);
                }

                string uriBuscaHabilidades = "http://iagomartuci.somee.com/RpgApi/PersonagemHabilidades/GetHabilidades";
                //string uriBuscaHabilidades = "http://iagomartuci-etec.somee.com/RpgApi/PersonagemHabilidades/GetHabilidades";
                response = await httpClient.GetAsync(uriBuscaHabilidades);
                serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<HabilidadeViewModel> listaHabilidades = await Task.Run(() =>
                        JsonConvert.DeserializeObject<List<HabilidadeViewModel>>(serialized));
                    ViewBag.ListaHabilidades = listaHabilidades;
                }
                else
                {
                    throw new Exception(serialized);
                }

                return View("IndexHabilidades");
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<ActionResult> IndexHabilidadesAsync(DisputaViewModel disputa)
        {
            try
            {
                if (ValidarLogin() == false)
                {
                    TempData["MensagemErro"] = "Você não está logado";
                    return RedirectToAction("IndexLogin", "Usuarios"); //Método: IndexLogin e Controller: Usuarios ("IndexLogin", "Usuarios")
                }

                HttpClient httpClient = new HttpClient();
                string uriComplementar = "Habilidade";
                var content = new StringContent(JsonConvert.SerializeObject(disputa));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await httpClient.PostAsync(uriBase + uriComplementar, content);
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    disputa = await Task.Run(() =>
                    JsonConvert.DeserializeObject<DisputaViewModel>(serialized));
                    TempData["Mensagem"] = disputa.Narracao;
                    return RedirectToAction("Index", "Personagens");
                }
                else
                {
                    throw new Exception(serialized);
                }
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<ActionResult> DisputaGeralAsync()
        {
            try
            {
                if (ValidarLogin() == false)
                {
                    TempData["MensagemErro"] = "Você não está logado";
                    return RedirectToAction("IndexLogin", "Usuarios"); //Método: IndexLogin e Controller: Usuarios ("IndexLogin", "Usuarios")
                }

                HttpClient httpClient = new HttpClient();

                string token = HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                string uriBuscaPersonagens = "http://iagomartuci.somee.com/RpgApi/Personagens/GetAll";
                //string uriBuscaPersonagens = "http://iagomartuci-etec.somee.com/RpgApi/Personagens/GetAll";
                HttpResponseMessage response = await httpClient.GetAsync(uriBuscaPersonagens);

                string serialized = await response.Content.ReadAsStringAsync();

                List<PersonagemViewModel> listaPersonagens = await Task.Run(() =>
                    JsonConvert.DeserializeObject<List<PersonagemViewModel>>(serialized));

                string uriDisputa =  "http://iagomartuci.somee.com/RpgApi/Disputas/DisputaEmGrupo";
                //string uriDisputa = "http://iagomartuci-etec.somee.com/RpgApi/Disputas/DisputaEmGrupo";
                DisputaViewModel disputa = new DisputaViewModel();
                disputa.ListaIdPersonagens = new List<int>();
                disputa.ListaIdPersonagens.AddRange(listaPersonagens.Select(p => p.Id));

                var content = new StringContent(JsonConvert.SerializeObject(disputa));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await httpClient.PostAsync(uriDisputa, content);

                serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    disputa = await Task.Run(() =>
                        JsonConvert.DeserializeObject<DisputaViewModel>(serialized));
                    TempData["Mensagem"] = string.Join("<br/>", disputa.Resultados);
                }
                else
                {
                    throw new Exception(serialized);
                }

                return RedirectToAction("Index", "Personagens");
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Index", "Personagens");
            }
        }

        [HttpGet]
        public async Task<ActionResult> IndexDisputasAsync()
        {
            try
            {
                if (ValidarLogin() == false)
                {
                    TempData["MensagemErro"] = "Você não está logado";
                    return RedirectToAction("IndexLogin", "Usuarios"); //Método: IndexLogin e Controller: Usuarios ("IndexLogin", "Usuarios")
                }

                string uriComplementar = "Listar";

                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await httpClient.GetAsync(uriBase + uriComplementar);
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<DisputaViewModel> lista = await Task.Run(() =>
                        JsonConvert.DeserializeObject<List<DisputaViewModel>>(serialized));
                    return View(lista);
                }
                else
                {
                    throw new Exception(serialized);
                }
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<ActionResult> ApagarDisputasAsync()
        {
            try
            {
                if (ValidarLogin() == false)
                {
                    TempData["MensagemErro"] = "Você não está logado";
                    return RedirectToAction("IndexLogin", "Usuarios"); //Método: IndexLogin e Controller: Usuarios ("IndexLogin", "Usuarios")
                }

                HttpClient httpClient = new HttpClient();

                string token = HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                string uriComplementar = "ApagarDisputas";

                HttpResponseMessage response = await httpClient.DeleteAsync(uriBase + uriComplementar);
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["Mensagem"] = "Disputas apagadas com sucesso!";
                }
                else
                {
                    throw new Exception(serialized);
                }
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
            }
            return RedirectToAction("IndexDisputas", "Disputas");
        }

        private bool ValidarLogin()
        {
            HttpClient httpClient = new HttpClient();
            string token = HttpContext.Session.GetString("SessionTokenUsuario");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            if (token != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}