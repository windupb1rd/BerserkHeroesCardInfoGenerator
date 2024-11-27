using Core.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.UseCases
{
    internal class CreateCardsUseCase
    {
        private readonly IWebApiClient _webApiClient;

        public CreateCardsUseCase(IWebApiClient webApiClient)
        {
            _webApiClient = webApiClient;
        }


    }
}
