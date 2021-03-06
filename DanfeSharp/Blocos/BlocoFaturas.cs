﻿using org.pdfclown.documents.contents.composition;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanfeSharp
{
    class BlocoFaturas : BlocoDanfe
    {
        public readonly float RetanguloH = Utils.Mm2Pu(7.8F);

        public const int Colunas = 7;

        public BlocoFaturas(DanfeDocumento danfeMaker) : base(danfeMaker)
        {
            float nLinhas = (float)Math.Ceiling((float)danfeMaker.Model.Duplicatas.Count / (float)Colunas);
            Size = new System.Drawing.SizeF(danfeMaker.InnerRect.Width, danfeMaker.CabecalhoBlocoAltura + nLinhas*RetanguloH + DanfeDocumento.LineWidth);
            Initialize();
            //_PossuiBordaTopo = false;
        }

        protected override void CriarCampos()
        {

        }

        protected override void PosicionarCampos()
        {

        }

        protected override void ToXObjectInternal(PrimitiveComposer comp)
        {

            var duplicatas = Danfe.Model.Duplicatas;

            BlockComposer bComp = new BlockComposer(comp);

            RectangleF r1 = InternalRectangle;
            r1.Height = RetanguloH;

            RectangleF[] colunas = r1.SplitRectangle(Colunas);
            int colunaIndex = 0;           

           
            for (int i = 0; i < duplicatas.Count; i++)
            {

                if (i > 0 && i % Colunas == 0)
                {
                    colunaIndex = 0;

                    for (int i2 = 0; i2 < Colunas; i2++)
                    {
                        colunas[i2].Y += RetanguloH;
                    }
                }

                var duplicata = duplicatas[i];
                RectangleF pRet = colunas[colunaIndex].GetPaddedRectangleMm(1);
                comp.SetFont(Danfe.Font, 6);
                bComp.SafeBegin(pRet, XAlignmentEnum.Left, YAlignmentEnum.Middle);
                bComp.ShowText("Número:\nVenc.:\nValor:\n");
                bComp.End();
               

                comp.SetFont(Danfe.FontBold, 6);

                bComp.SafeBegin(pRet, XAlignmentEnum.Right, YAlignmentEnum.Middle);
                bComp.ShowText(String.Format("{0}\n{1}\n{2}\n", duplicata.Numero, duplicata.Vecimento.Formatar(), duplicata.Valor.Formatar()));
                bComp.End();

                comp.SafeDrawRectangle(colunas[colunaIndex]);

                colunaIndex++;         
            }

            comp.Stroke();
        }

        public override string Cabecalho
        {
            get
            {
                return "Faturas";
            }
        }


    }
}
