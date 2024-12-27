using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using SystemCcharpzinho.Infrastructure.Context;

namespace SystemCcharpzinho.Tests
{
    public abstract class BaseTest
    {
        protected readonly AppDbContext _context;

        protected BaseTest()
        {
            // carregar as variáveis de ambiente
            Env.Load("C:\\Users\\Pablo\\Desktop\\api_ccharpzada\\SystemCcharpzinho.API\\.env");

            // é criado um banco de dados em memória para testar as funções
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new AppDbContext(options);
            
            // é criado esse banco de dados em memória para testar as funções
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }
    }
}