namespace MassTransit.DbTransport.Tests.SqlServer;

using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using SqlTransport.PostgreSql;
using SqlTransport.SqlServer;


[TestFixture]
public class PortAddress_Specs
{
    [Test]
    public async Task Should_include_the_port()
    {
        await using var provider = new ServiceCollection()
            .AddMassTransitTestHarness(x =>
            {
                x.AddOptions<SqlTransportOptions>().Configure(options =>
                {
                    options.Host = "localhost";
                    options.Database = "masstransit_transport_tests";
                    options.Port = 8675;
                    options.Schema = "transport";
                    options.Role = "transport";
                    options.Username = "unit_tests";
                    options.Password = "H4rd2Gu3ss!";
                    options.AdminUsername = "sa";
                    options.AdminPassword = "Password12!";
                });
            })
            .BuildServiceProvider(true);

        var connection = SqlServerSqlTransportConnection.GetDatabaseConnection(provider.GetRequiredService<IOptions<SqlTransportOptions>>().Value);

        Assert.That(connection.Connection.ConnectionString.Contains("Data Source=localhost,8675"));
    }

    [Test]
    public async Task Should_include_the_port_for_postgres()
    {
        await using var provider = new ServiceCollection()
            .AddMassTransitTestHarness(x =>
            {
                x.AddOptions<SqlTransportOptions>().Configure(options =>
                {
                    options.Host = "localhost";
                    options.Port = 5544;
                    options.Database = "masstransit_transport_tests";
                    options.Schema = "transport";
                    options.Role = "transport";
                    options.Username = "unit_tests";
                    options.Password = "H4rd2Gu3ss!";
                    options.AdminUsername = "sa";
                    options.AdminPassword = "Password12!";
                });
            })
            .BuildServiceProvider(true);

        var connection = PostgresSqlTransportConnection.GetDatabaseConnection(provider.GetRequiredService<IOptions<SqlTransportOptions>>().Value);

        Assert.That(connection.Connection.ConnectionString.Contains("Port=5544"));
    }

    [Test]
    public async Task Should_not_include_the_port()
    {
        await using var provider = new ServiceCollection()
            .AddMassTransitTestHarness(x =>
            {
                x.AddOptions<SqlTransportOptions>().Configure(options =>
                {
                    options.Host = "localhost";
                    options.Database = "masstransit_transport_tests";
                    options.Schema = "transport";
                    options.Role = "transport";
                    options.Username = "unit_tests";
                    options.Password = "H4rd2Gu3ss!";
                    options.AdminUsername = "sa";
                    options.AdminPassword = "Password12!";
                });
            })
            .BuildServiceProvider(true);

        var connection = SqlServerSqlTransportConnection.GetDatabaseConnection(provider.GetRequiredService<IOptions<SqlTransportOptions>>().Value);

        Assert.That(connection.Connection.ConnectionString.Contains("Data Source=localhost;"));
    }

    [Test]
    public async Task Should_not_include_the_port_for_postgres()
    {
        await using var provider = new ServiceCollection()
            .AddMassTransitTestHarness(x =>
            {
                x.AddOptions<SqlTransportOptions>().Configure(options =>
                {
                    options.Host = "localhost";
                    options.Database = "masstransit_transport_tests";
                    options.Schema = "transport";
                    options.Role = "transport";
                    options.Username = "unit_tests";
                    options.Password = "H4rd2Gu3ss!";
                    options.AdminUsername = "sa";
                    options.AdminPassword = "Password12!";
                });
            })
            .BuildServiceProvider(true);

        var connection = PostgresSqlTransportConnection.GetDatabaseConnection(provider.GetRequiredService<IOptions<SqlTransportOptions>>().Value);

        Assert.That(connection.Connection.ConnectionString.Contains("Port="), Is.False);
    }
}
