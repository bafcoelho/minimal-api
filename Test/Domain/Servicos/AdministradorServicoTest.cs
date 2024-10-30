using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using minimal_api.Dominio.Entidades;
using minimal_api.Dominio.Servicos;
using minimal_api.Infraestrutura.Db;

namespace Test.Domain.Servicos
{
    [TestClass]
    public class AdministradorServicoTest
    {
        private DbContexto CriarContextoDeTeste()
        {
            var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var path = Path.GetFullPath(Path.Combine(assemblyPath ?? string.Empty, @"..\..\..\"));
            
            var builder = new ConfigurationBuilder()
                .SetBasePath(path ?? Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            
            var configuration = builder.Build();
            return new DbContexto(configuration);
        }

        [TestMethod]
        public void TestarSalvarAdministrador()
        {
            //arrange (variáveis que vamos usar)
            var context = CriarContextoDeTeste();
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");
            
            var adm = new Administrador();
            adm.Email = "teste@teste.com";
            adm.Senha = "teste";
            adm.Perfil = "Adm";

            
            var administradorServico = new AdministradorServico(context);
            
            //Act - ações que precisam ser feitas
            administradorServico.Incluir(adm);
            
            //Assert - validação
            Assert.AreEqual(1, administradorServico.Todos(1).Count());
        }
        
        [TestMethod]
        public void TestandoBuscaPorId()
        {
            //arrange (variáveis que vamos usar)
            var context = CriarContextoDeTeste();
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");
            
            
            var adm = new Administrador();
            adm.Email = "teste@teste.com";
            adm.Senha = "teste";
            adm.Perfil = "Adm";

            
            var administradorServico = new AdministradorServico(context);
            
            //Act - ações que precisam ser feitas
            administradorServico.Incluir(adm);
            var admnovo = administradorServico.BuscaPorId(1);
            
            //Assert - validação
            Assert.AreEqual(1, admnovo.Id);
        }
    }
}