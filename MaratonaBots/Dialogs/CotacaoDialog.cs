using System;
using System.Linq;
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
            await context.PostAsync($"Desculpe, não entendi sua pergunta.{result.Query}");
        }

        [LuisIntent("Sobre")]
        public async Task Sobre(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Eu sou um BOT e aprendo sempre. Atualmente faço cotações de moedas");
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
            await context.PostAsync("Ninguém tem paciência comigo!");
        }


        [LuisIntent("Cotacao")]
        public async Task Cotacao(IDialogContext context, LuisResult result)
        {
            var moedas = result.Entities?.Select(e => e.Entity);
            await context.PostAsync($"Eu farei a cotação das moedas. {String.Join(",", moedas.ToArray())}");
        }

    }
}