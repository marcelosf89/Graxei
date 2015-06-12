using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Routing;
using Graxei.Apresentacao.Extension;
using Graxei.Transversais.ContratosDeDados.TinyTypes;
using Graxei.Apresentacao.Extension.PaginacaoChain.LinkBuilderStrategy;

namespace Graxei.Apresentacao.Extension.PaginacaoChain
{
    public abstract class AbstractPaginacao : IPaginacaoChain
    {
        public AbstractPaginacao(TotalElementosLista totalElementosLista,
            PaginaAtualLista elementoAtualLista, int quantidadeMaximaLinksPaginacaoPorVez, ILinkBuilderStrategy linkBuilderStrategy)
        {
            _totalElementosLista = totalElementosLista;
            _elementoAtualLista = elementoAtualLista;
            _quantidadeMaximaLinksPaginacaoPorVez = quantidadeMaximaLinksPaginacaoPorVez;
            _totalPaginas = 1;
            if (_totalElementosLista.Valor / _quantidadeMaximaLinksPaginacaoPorVez > 1)
            {
                _totalPaginas = _totalElementosLista.Valor / 10;
            }
            _linkBuilderStrategy = linkBuilderStrategy;
        }
        
        public abstract bool RegraAtende();

        public abstract long GetPrimeiraPaginaGrupoAtual();

        public abstract long GetUltimaPaginaGrupoAtual();

        public abstract long GetElementoParaSubstituir();
        
        public MvcHtmlString Get()
        {
            bool tratadoPeloElementoDaCadeia = RegraAtende();
            if (tratadoPeloElementoDaCadeia)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("<div class=\"btn-group\">");

                List<string> links = _linkBuilderStrategy.Build(GetPrimeiraPaginaGrupoAtual(), GetUltimaPaginaGrupoAtual());
                links = _linkBuilderStrategy.SubstituirElementoAtual(GetElementoParaSubstituir());
                for (int i = 0; i < links.Count; i++)
                {
                    stringBuilder.Append(links[i]);
                }
                stringBuilder.Append("</div>"); 
                return MvcHtmlString.Create(stringBuilder.ToString());
            }
            if (ProximoElemento != null)
            {
                return ProximoElemento.Get();
            }

            return new MvcHtmlString("<div></div>");
        }

        private void SubstituirElementoAtual(List<string> links)
        {
            long elementoParaSubstituir = GetElementoParaSubstituir();
            
        }

        public IPaginacaoChain ProximoElemento { get; private set; }

        public void SetProximoElemento(IPaginacaoChain paginacaoChain)
        {
            ProximoElemento = paginacaoChain;
        }

        public ILinkBuilderStrategy LinkBuilderStrategy
        {
            get
            {
                return _linkBuilderStrategy;
            }
        }

        protected TotalElementosLista _totalElementosLista;
        protected PaginaAtualLista _elementoAtualLista;
        protected int _quantidadeMaximaLinksPaginacaoPorVez;
        protected long _totalPaginas;
        protected ILinkBuilderStrategy _linkBuilderStrategy;
    }
}