﻿using System;
using System.Collections.Generic;
using System.Linq;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Utilidades.NHibernate;
using NHibernate.Linq;

namespace Graxei.Persistencia.Implementacao.NHibernate
{
    public class LojasNHibernateMySQL : PadraoNHibernateMySQL<Loja>, IRepositorioLojas
    {

        #region Implementação de IRepositorioLojas

        public void Salvar(Loja loja, Usuario usuario)
        {
            base.Salvar(loja);
            /* TODO: checar se a lógica deve permanecer no DAO */
            LojaUsuario lu = null;
            int count = 0;
            if (!UtilidadeEntidades.IsTransiente(loja))
            {
                count =
                    SessaoAtual.Query<LojaUsuario>().Count(
                        p => p.Loja.Nome.Trim().ToLower() == loja.Nome.Trim().ToLower()
                             && p.Usuario.Nome.Trim().ToLower() == usuario.Login.Trim().ToLower());
            }
           if (count == 0)
           {
               lu = new LojaUsuario()
                        {
                            Loja = loja,
                            Usuario = usuario,
                            DataRegistro = DateTime.Now,
                            UsuarioLog = usuario
                        };
               SessaoAtual.Save(lu);
           }
        }

        public void Salvar(Loja loja, IList<Usuario> usuarios)
        {
            foreach (Usuario usuario in usuarios)
            {
                Salvar(loja, usuario);
            }
        }

        public Loja Get(string nome)
        {
            return SessaoAtual.Query<Loja>()
                              .SingleOrDefault<Loja>(loja => loja.Nome.Trim().ToLower() == nome.Trim().ToLower());
        }

        #endregion
    }
}
