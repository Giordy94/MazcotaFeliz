using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MascotaFeliz.App.Dominio;
using Microsoft.EntityFrameworkCore;

namespace MascotaFeliz.App.Persistencia
{
    public class RepositorioPropietario : IRepositorioPropietario
    {

        private readonly AppContext _appContext;

        public RepositorioPropietario(AppContext appContext)
        {
            _appContext = appContext;
        }

        public Propietario AddPropietario(Propietario propietario)
        {
            var propietarioNuevo = _appContext.Propietarios.Add(propietario);
            _appContext.SaveChanges();
            return propietarioNuevo.Entity;
        }

        public Propietario UpdatePropietario(Propietario propietario)
        {
            var propietarioEncontrado = _appContext.Propietarios.FirstOrDefault(d => d.Id == propietario.Id);
            if(propietarioEncontrado != null)
            {
                propietarioEncontrado.Nombres = propietario.Nombres;
                propietarioEncontrado.Apellidos = propietario.Apellidos;
                propietarioEncontrado.Direccion = propietario.Direccion;
                propietarioEncontrado.Telefono = propietario.Telefono;
                propietarioEncontrado.Correo = propietario.Correo;
                _appContext.SaveChanges();
            }
            return propietarioEncontrado;
        }

        public void DeletePropietario(int idPropietario)
        {
            var propietarioEncontrado = _appContext.Propietarios.FirstOrDefault(d => d.Id == idPropietario);
            if (propietarioEncontrado == null)
                return;
            _appContext.Propietarios.Remove(propietarioEncontrado);
            _appContext.SaveChanges();
        }

        public IEnumerable<Propietario> GetAllPropietarios()
        {
            return GetAllPropietarios_();
        }

        public IEnumerable<Propietario> GetPropietarioFiltro(Propietario propietario)
        {
            var propietarios = GetAllPropietarios();
            if (propietarios != null)
            {
                if(!String.IsNullOrEmpty(propietario.Nombres))
                {
                    propietarios = propietarios.Where(s => s.Nombres.Contains(propietario.Nombres));
                }
                if(!String.IsNullOrEmpty(propietario.Apellidos))
                {
                    propietarios = propietarios.Where(s => s.Apellidos.Contains(propietario.Apellidos));
                }
                if(!String.IsNullOrEmpty(propietario.Correo))
                {
                    propietarios = propietarios.Where(s => s.Correo.Contains(propietario.Correo));
                }
            }
            return propietarios;
        }
        
        public IEnumerable<Propietario> GetAllPropietarios_()
        {
            return _appContext.Propietarios;
        }

        public Propietario GetPropietario(int idPropietario)
        {
            return _appContext.Propietarios.FirstOrDefault(d => d.Id == idPropietario);
        }
    }
}