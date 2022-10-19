using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

using var loggerFactory =
    LoggerFactory.Create(loggingBuilder => loggingBuilder
        .AddSimpleConsole(c =>
        {
            c.IncludeScopes = true;
            c.SingleLine = true;
            c.ColorBehavior = LoggerColorBehavior.Disabled;
        }));

var logger = loggerFactory.CreateLogger<Program>();
TestcontainersSettings.Logger = logger;

logger.LogWarning("Begin Program");

var dockerFileDir = Path.GetFullPath(Directory.GetCurrentDirectory())
    .Split($"{Path.DirectorySeparatorChar}src")
    .First();

await new ImageFromDockerfileBuilder()
    .WithDockerfileDirectory(dockerFileDir)
    .WithDeleteIfExists(true)
    .WithCleanUp(true)
    .WithName("test_image_name").Build();