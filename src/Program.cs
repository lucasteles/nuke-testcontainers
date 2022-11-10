using DotNet.Testcontainers.Configurations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

using var loggerFactory =
    LoggerFactory.Create(loggingBuilder => loggingBuilder
        .AddSimpleConsole(c =>
        {
            c.IncludeScopes = true;
            c.SingleLine = true;
            // c.ColorBehavior = LoggerColorBehavior.Disabled;
        })
        .SetMinimumLevel(LogLevel.Trace));

var logger = loggerFactory.CreateLogger<Program>();
TestcontainersSettings.Logger = logger;

logger.LogInformation("*********** Begin Test **********");

PostgreSqlTestcontainerConfiguration dbCredentials = new()
{
    Username = "postgres",
    Password = "postgres",
    Database = "integration_test_db"
};
var postgresSql = new TestcontainersBuilder<PostgreSqlTestcontainer>()
     .WithHostname("db")
     .WithDatabase(dbCredentials)
     .Build();

await postgresSql.StartAsync();
await Task.Delay(1000);
// var dockerFileDir = Path.GetFullPath(Directory.GetCurrentDirectory())
//     .Split($"{Path.DirectorySeparatorChar}src")
//     .First();

// await new ImageFromDockerfileBuilder()
//     .WithDockerfileDirectory(dockerFileDir)
//     .WithDeleteIfExists(true)
//     .WithCleanUp(true)
//     .WithName("test_image_name").Build();

logger.LogInformation("*********** End Test **********");
