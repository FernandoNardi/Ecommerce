using Ecommerce.Models;
using Ecommerce.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

//=============================================
// Autor: Fernando Nardi
//=============================================
namespace Ecommerce.Controllers
{
    public class ClienteController : Controller
    {

        private string _address = string.Format("{0}", "http://localhost:8080/api/clientes");
        private RequestTask _requestTask;

        #region :::: Listagem de clientes ::::

        [HttpGet]
        public ActionResult Index()
        {
            _requestTask = new RequestTask(this._address);
            Resultado resultado = _requestTask.BuscarClientes();
            if (resultado.erro)
            {
                ViewBag.hideerro = string.Empty;
                ViewBag.mensagem = resultado.mensagem;
                return View();
            }
            else
            {
                ViewBag.hideerro = string.Format("{0}", "hide");
                return View(resultado.lista);
            }
        }
        
        #endregion

        #region :::: Cadastro de clientes ::::

        [HttpGet]
        public ActionResult Cadastrar()
        {
            ViewBag.hideerro = string.Format("{0}", "hide");
            return View();
        }
        
        [HttpPost]       
        public ActionResult Cadastrar([Bind(Include = "cpf, nome, email, estadocivil, telefones, logradouro, numero, bairro, cidade, estado")]Cliente cliente)
        {            
            try
            {
                if (ModelState.IsValid)
                {
                    _requestTask = new RequestTask(this._address);
                    cliente.telefones = verificarTelefone(cliente);
                    Resultado resultado = _requestTask.CadastrarCliente(cliente);
                    if (resultado.erro)
                    {                        
                        ViewBag.hideerro = string.Empty;
                        ViewBag.mensagem = resultado.mensagem;
                        return View();
                    }
                    else
                    {
                        ViewBag.hideerro = string.Format("{0}", "hide");
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ViewBag.hideerro = string.Empty;
                    ViewBag.mensagem = string.Format("{0}", "<div class='alert alert-danger' role='alert'>Ocorreu um erro ao cadastrar cliente</div>");
                    return View();
                }                
            }
            catch (Exception exception)
            {
                ViewBag.hideerro = string.Empty;
                ViewBag.mensagem = string.Format("{0}", "<div class='alert alert-danger' role='alert'>Ocorreu um erro ao cadastrar cliente</div>");
                return View();
            }            
        }

        #endregion

        #region :::: Edição de clientes ::::
                
        public ActionResult Editar(string cpf = "")
        {            
            return GetCliente(cpf);
        }

        [HttpPost]
        public ActionResult Editar([Bind(Include = "_id, cpf, nome, email, estadocivil, telefones, logradouro, numero, bairro, cidade, estado")]Cliente cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _requestTask = new RequestTask(string.Format("{0}{1}{2}", this._address, "/", cliente.cpf));
                    cliente.telefones = verificarTelefone(cliente);
                    Resultado resultado = _requestTask.EditarCliente(cliente);
                    if (resultado.erro)
                    {
                        ViewBag.hideerro = string.Empty;
                        ViewBag.mensagem = resultado.mensagem;
                        return View();
                    }
                    else
                    {
                        ViewBag.hideerro = string.Format("{0}", "hide");
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception exception)
            {
                ViewBag.hideerro = string.Empty;
                ViewBag.mensagem = string.Format("{0}", "Ocorreu um erro ao alterar o cliente");
                return View();
            }
            return View(cliente);
        }

        #endregion

        #region :::: Deletar Cliente ::::

        public ActionResult Deletar(string cpf = "")
        {
            return GetCliente(cpf);
        }

        [HttpPost, ActionName("Deletar")]
        public ActionResult DeletarConfirmed(string cpf) {
            try
            {
                _requestTask = new RequestTask(string.Format("{0}{1}{2}", this._address, "/", cpf));
                Resultado resultado = _requestTask.DeleteCliente();
                if (resultado.erro)
                {
                    ViewBag.hideerro = string.Empty;
                    ViewBag.mensagem = resultado.mensagem;
                    return View();
                }
                else
                {
                    ViewBag.hideerro = string.Format("{0}", "hide");
                    return RedirectToAction("Index");
                }
            }
            catch (Exception exception)
            {                
                ViewBag.hideerro = string.Empty;
                ViewBag.mensagem = string.Format("{0}", "Ocorreu um erro excluir o cliente");
                return View();
            }            
        }

        #endregion

        #region :::: Detalhe do cliente ::::

        public ActionResult Detalhe(string cpf = "") {
            return GetCliente(cpf);
        }

        public ActionResult GetCliente(string cpf)
        {
            if (!string.IsNullOrEmpty(cpf))
            {
                _requestTask = new RequestTask(string.Format("{0}{1}{2}", this._address, "/", cpf));
                Resultado resultado = _requestTask.BuscarClientes();
                if (resultado.erro)
                {
                    ViewBag.hideerro = string.Empty;
                    ViewBag.mensagem = resultado.mensagem;
                    return View();
                }
                else
                {
                    if (resultado.lista != null && resultado.lista.Count > 0)
                    {
                        if (resultado.lista[0].telefones.Count == 2)
                        {
                            resultado.lista[0].telefones.Add(new Telefone());
                        }
                        else if (resultado.lista[0].telefones.Count == 1)
                        {
                            resultado.lista[0].telefones.Add(new Telefone());
                            resultado.lista[0].telefones.Add(new Telefone());
                        }
                        else if (resultado.lista[0].telefones.Count == 0)
                        {
                            resultado.lista[0].telefones.Add(new Telefone());
                            resultado.lista[0].telefones.Add(new Telefone());
                            resultado.lista[0].telefones.Add(new Telefone());
                        }
                        ViewBag.hideerro = string.Format("{0}", "hide");
                        return View(resultado.lista[0]);
                    }
                    else
                    {                        
                        ViewBag.hideerro = string.Empty;
                        ViewBag.mensagem = string.Format("{0}", "Cliente não está cadastrado no sistema");
                        return View();
                    }
                }                
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        #endregion

        #region :::: Telefone :::::
        
        public List<Telefone> verificarTelefone(Cliente cliente)
        {
            List<Telefone> telefones = new List<Telefone>();
            if (cliente.telefones != null)
            {
                foreach (Telefone tel in cliente.telefones) {
                    if (!string.IsNullOrEmpty(tel.numero)) 
                    {
                        telefones.Add(tel);
                    }
                }
            }
            return telefones;
        }

        #endregion

    }
}