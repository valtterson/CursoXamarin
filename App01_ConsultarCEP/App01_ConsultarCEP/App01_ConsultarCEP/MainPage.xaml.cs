using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsultarCEP.Servicos.Modelo;
using App01_ConsultarCEP.Servicos;

namespace App01_ConsultarCEP
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

            Botao.Clicked += BuscarCEP;
        }

        private void BuscarCEP(object sender, EventArgs args)
        {
            //-- Logica
            //-- Validações

            string cep = CEP.Text.Trim();

            if (isValidCEP(cep))
            {

                try
                {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);

                    if (end != null)
                    {
                        Resultado.Text = string.Format("Endereço: {2}, {3} {0}, {1} ", end.localidade, end.uf, end.logradouro, end.bairro);
                    }
                    else
                    {
                        DisplayAlert("Erro", "O endereço não foi encontrado para o CEP informado: " + cep, "OK");
                    }

                    
                }catch(Exception e)
                {
                    DisplayAlert("Erro crítico", e.Message, "OK");
                }


            }

        }

        private bool isValidCEP(string cep)
        {
            bool valid = true;
            
            
            if (cep.Length != 8)
            {
                DisplayAlert("Erro", "CEP inválido. O CEP deve conter 8 caracteres.", "OK");

                valid = false;
            }
            

            int NovoCEP = 0;

            if (! int.TryParse(cep, out NovoCEP))
            {
                DisplayAlert("Erro", "CEP inválido. O CEP deve ser composto apenas por números.", "OK");

                valid = false;
            }

            return valid;
        }
    }
}
