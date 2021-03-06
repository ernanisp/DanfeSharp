﻿using org.pdfclown.documents.contents.composition;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanfeSharp
{
    public class BlocoCalculoImposto : BlocoDanfe
    {
        #region CamposLinhas1

        public DanfeCampo BaseCalculoIcms { get; private set; }
        public DanfeCampo ValorIcms { get; private set; }
        public DanfeCampo BaseCalculoIcmsSt { get; private set; }
        public DanfeCampo ValorCalculoIcmsSt { get; private set; }
        public DanfeCampo ValorProdutos { get; private set; }

        #endregion


        #region CamposLinhas2

        public DanfeCampo ValorFrete { get; private set; }
        public DanfeCampo ValorSeguro { get; private set; }
        public DanfeCampo Desconto { get; private set; }
        public DanfeCampo OutrasDespesas { get; private set; }
        public DanfeCampo ValorIpi { get; private set; }
        public DanfeCampo ValorNota { get; private set; }

        #endregion

        
        public BlocoCalculoImposto(DanfeDocumento danfeMaker)
            : base(danfeMaker)
        {
            Size = new SizeF(Danfe.InnerRect.Width, danfeMaker.CabecalhoBlocoAltura + 2*danfeMaker.CampoAltura + DanfeDocumento.LineWidth);
            Initialize();

        }

        protected override DanfeCampo CriarCampo(string cabecalho, string corpo, XAlignmentEnum corpoAlinhamentoX = XAlignmentEnum.Right)
        {
            return base.CriarCampo(cabecalho, corpo, corpoAlinhamentoX);
        }

        protected override void CriarCampos()
        {
            BaseCalculoIcms = CriarCampo("BASE DE CÁLCULO DO ICMS", Danfe.Model.BaseCalculoIcms.Formatar());
            ValorIcms = CriarCampo("VALOR DO ICMS", Danfe.Model.ValorIcms.Formatar());
            BaseCalculoIcmsSt = CriarCampo("BASE DE CÁLC. ICMS S.T.", Danfe.Model.BaseCalculoIcmsSt.Formatar());
            ValorCalculoIcmsSt = CriarCampo("VALOR DO ICMS SUBSTITUIÇÃO", Danfe.Model.ValorIcmsSt.Formatar());
            ValorProdutos = CriarCampo("VALOR TOTAL DOS PRODUTOS", Danfe.Model.ValorTotalProdutos.Formatar());


            ValorFrete = CriarCampo("Valor do Frete", Danfe.Model.ValorFrete.Formatar());
            ValorSeguro = CriarCampo("VALOR DO SEGURO", Danfe.Model.ValorSeguro.Formatar());
            Desconto = CriarCampo("Desconto", Danfe.Model.Desconto.Formatar());
            OutrasDespesas = CriarCampo("OUTRAS DESPESAS", Danfe.Model.OutrasDespesas.Formatar());
            ValorIpi = CriarCampo("VALOR DO IPI", Danfe.Model.ValorIpi.Formatar());
            ValorNota = CriarCampo("VALOR TOTAL DA NOTA", Danfe.Model.ValorTotalNota.Formatar(), RectangleF.Empty, XAlignmentEnum.Right, 10, true);
        }

        protected override void PosicionarCampos()
        {
            RectangleF linha = new RectangleF(InternalRectangle.Left, InternalRectangle.Top, InternalRectangle.Width, Danfe.CampoAltura);
            PosicionarLadoLado(linha, BaseCalculoIcms, ValorIcms, BaseCalculoIcmsSt, ValorCalculoIcmsSt, ValorProdutos);

            linha.Y = linha.Bottom;
            PosicionarLadoLado(linha, new float[] { 0, 0, 0, 0, 0, ValorProdutos.Retangulo.Width}, ValorFrete, ValorSeguro, Desconto, OutrasDespesas, ValorIpi, ValorNota);
        }       
                  

        public override string Cabecalho
        {
            get
            {
                return "CÁLCULO DO IMPOSTO";
            }
        }
    }
}
