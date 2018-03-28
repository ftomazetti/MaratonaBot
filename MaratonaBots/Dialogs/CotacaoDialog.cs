using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;

namespace MaratonaBots.Dialogs
{
    [Serializable]
    [LuisModel("c7273c08-6c43-4107-bac3-64b48a484e3e", "6c3165e4a3b24e81829b131e4c7e5c8a")]
    public class CotacaoDialog : LuisDialog<object>
    {
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Desculpe, não entendi sua pergunta: {result.Query}");
        }

        [LuisIntent("Sobre")]
        public async Task Sobre(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Eu sou um BOT ainda sem nome. Atualmente não faço grandes coisas");
        }

        [LuisIntent("Saudacao")]
        public async Task Saudacao(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Olá tudo bem!");
        }
        // Frederico - mudança teste
        [LuisIntent("Desvio")]
        public async Task Desvio(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Que isso! Seja educado!");
        }

        [LuisIntent("Cotacao")]
        public async Task Cotacao(IDialogContext context, LuisResult result)
        {
            var moedas = result.Entities?.Select(e => e.Entity);
            Thread.Sleep(2000);
            await context.PostAsync($"Eu farei a cotação das moedas. {String.Join(",", moedas.ToArray())}");
        }

        [LuisIntent("identificacao")]
        public async Task identificacao(IDialogContext context, LuisResult result)
        {
            var cidadao = result.Entities?.Select(e => e.Entity);
            Thread.Sleep(2000);
            await context.PostAsync($"Você é:  {String.Join(",", cidadao.ToArray())}");
        }

        [LuisIntent("geracao")]
        public async Task geracao(IDialogContext context, LuisResult result)
        {

            var contexto = context;
            var tipo = result.Entities?.Select(e => e.Entity);
            var resultado = String.Join(",", tipo.ToArray());

            var numregistro = Extrair_numero(resultado);
            var tiporegistro = Extrair_tipo(resultado);
            var email = Extrair_email(resultado);

            Thread.Sleep(2000);
            await context.PostAsync($"Recebi a string: {String.Join(",", tipo.ToArray())}");
            await context.PostAsync($"Vamos gerar: {tiporegistro} {numregistro}");
        }

        private string Extrair_numero(string conteudo)
        {
            string[] partes = conteudo.Split(',');
            var retorno = "";
            foreach (var parte in partes)
            {
                if (parte.Length == 6)
                {
                    retorno = parte;
                    break;
                }
            }
            return retorno;
        }

        private string Extrair_tipo(string conteudo)
        {
            var retorno = "VAZIO ";
                if (conteudo.ToLower().Contains("pa"))
                    retorno = " PA ";

            if (conteudo.ToLower().Contains("ficha"))
                retorno = " FICHA ";

            return retorno;
        }

        private string Extrair_email(string conteudo)
        {
            string[] partes = conteudo.Split(',');
            var retorno = "";
            foreach (var parte in partes)
            {
                if (parte.Contains("@"))
                {
                    retorno = parte;
                    break;
                }
            }
            return retorno;
        }


    }
}