using WalLanches.Models;

namespace WalLanches.Repositories.Interfaces
{
    public interface ICategoriaRepositories
    {
        IEnumerable<Categoria> categorias { get; }
    }
}
