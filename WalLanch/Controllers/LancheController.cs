using WalLanches.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WalLanches.Models;
using WalLanches.ViewModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;



namespace WalLanches.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository _lancheRepository;
        public LancheController(ILancheRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }

        public IActionResult List(string categoria)
        {

            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;
            if (string.IsNullOrEmpty(categoria))
            {
                lanches = _lancheRepository.Lanches.OrderBy(l => l.LancheId);
                categoriaAtual = "Todos os lanches";
            }
            else
            {
                //  if (string.Equals("Normal", categoria, StringComparison.OrdinalIgnoreCase))
                // {
                //    lanches = _lancheRepository.Lanches
                //   .Where(l => l.Categoria.CategoriaNome.Equals("Normal"))
                //       .OrderBy(l => l.Nome);
                //  }
                //  else
                //  {
                //    lanches = _lancheRepository.Lanches
                //       .Where(l => l.Categoria.CategoriaNome.Equals("Natural"))
                //     .OrderBy(l => l.Nome);
                //}

                lanches = _lancheRepository.Lanches
                    .Where(l => l.Categoria.CategoriaNome.Equals(categoria))
                    .OrderBy(c => c.Nome);


                categoriaAtual = categoria;
            }

            var LancheListViewModel = new LancheListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual

            };

            return View(LancheListViewModel);
        }
    }
}
