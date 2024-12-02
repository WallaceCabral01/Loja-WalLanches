using Microsoft.AspNetCore.Mvc;
using WalLanches.Models;
using WalLanches.ViewModels;

namespace WalLanches.Componentes
{
    public class CarrinhoCompraResumo:ViewComponent
    {
        private readonly CarrinhoCompra _carrinhoCompra;
      public CarrinhoCompraResumo(CarrinhoCompra carrinhoCompra)
        {
            _carrinhoCompra = carrinhoCompra;
        }

        public IViewComponentResult Invoke()
        {
            var itens = _carrinhoCompra.GetCarrinhoCompraItens();
            _carrinhoCompra.CarrinhoCompraItems = itens;

            var carrinhoCompraMV = new CarrinhoDeCompraViewModel
            {
                CarrinhoCompra = _carrinhoCompra,
                CarrinhoCopmpraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()
            };
            return View(carrinhoCompraMV);

        }
    }
}
