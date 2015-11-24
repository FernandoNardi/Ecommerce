using Ecommerce.Models;
using Ecommerce.Request;
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
                return View();
            }
            else
            {
                return View(resultado.lista);
            }
        }
        
        #endregion

        #region :::: Cadastro de clientes ::::

        [HttpGet]
        public ActionResult Cadastrar()
        {            
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
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }                
            }
            catch (Exception exception)
            {
                ViewBag.mensagem = "Ocorreu um erro ao salvar o cliente!";
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
                    _requestTask = new RequestTask(string.Format("{0}{1}{2}", this._address, "/", cliente._id));
                    Resultado resultado = _requestTask.EditarCliente(cliente);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception exception)
            {
                return RedirectToAction("Index");
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
                if (!resultado.erro)
                {
                    //return RedirectToAction("Mensagem", "Cliente", new { mensagem = "Cliente excluido com sucesso" });
                    return RedirectToAction("Index");
                }
                else {
                    //return RedirectToAction("Mensagem", "Cliente", new { mensagem = "Ocorreu um erro excluir o cliente" });
                    return RedirectToAction("Index");
                }
            }
            catch (Exception exception)
            {
                //return RedirectToAction("Mensagem", "Cliente", new { mensagem = "Ocorreu um erro excluir o cliente" });
                return RedirectToAction("Index");
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
                    //return RedirectToAction("Mensagem", "Cliente", new { mensagem = "Ops! Algo deu errado ou o cliente não está cadastrado no sistema" });
                    return RedirectToAction("Index");
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
                        return View(resultado.lista[0]);
                    }
                    else
                    {
                        //return RedirectToAction("Mensagem", "Cliente", new { mensagem = "Cliente não está cadastrado no sistema" });
                        return RedirectToAction("Index");
                    }
                }                
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        #endregion

        #region :::: Mensagem :::::

        public ActionResult Mensagem(string mensagem) {
            ViewBag.mensagem = mensagem;
            return View();
        }

        public List<Telefone> verificarTelefone(Cliente cliente)
        {
            List<Telefone> telefones = new List<Telefone>();
            if (cliente.telefones != null)
            {
                foreach (Telefone tel in cliente.telefones) {
                    if (tel.ddd != null && tel.numero != null) 
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