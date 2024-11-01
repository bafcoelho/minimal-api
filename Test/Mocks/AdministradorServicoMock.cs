using minimal_api.Dominio.DTOs;
using minimal_api.Dominio.Entidades;
using minimal_api.Dominio.Interfaces;

namespace Test.Mocks;

public class AdministradorServicoMock : IAdministradorServico
{

    private static List<Administrador> _administradores = new List<Administrador>()
    {
        new Administrador()
        {
            Id = 1,
            Email = "adm@teste.com",
            Senha = "teste",
            Perfil = "Adm"
        },
        new Administrador()
        {
            Id = 2,
            Email = "editor@teste.com",
            Senha = "teste",
            Perfil = "Editor"
        }
    };
    public Administrador? Login(LoginDTO loginDTO)
    {
        return _administradores.FirstOrDefault(x => x.Email == loginDTO.Email && x.Senha == loginDTO.Senha);
    }

    public Administrador Incluir(Administrador administrador)
    {
        administrador.Id = _administradores.Count + 1;
        _administradores.Add(administrador);

        return administrador;
    }

    public List<Administrador> Todos(int? pagina)
    {
        return _administradores;
    }

    public Administrador? BuscaPorId(int id)
    {
        return _administradores.FirstOrDefault(x => x.Id == id);
    }
}